using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Nompilo_PHC_Website.Repository;
using System.Net.Mail;
using System.Text;
using Nompilo_PHC_Website.Data;
using Nompilo_PHC_Website.ViewModels;

namespace Nompilo_PHC_Website.Controllers
{
    public class UserController : Controller
    {
        
        private readonly SignInManager<DataGeeksUser> _signInManager;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserStore<DataGeeksUser> _userStore;
        private readonly IUserEmailStore<DataGeeksUser> _emailStore;
        private readonly UserManager<DataGeeksUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _dbContext;

        public UserController(
            SignInManager<DataGeeksUser> signInManager,
            IRoleRepository roleRepository,
            IUserStore<DataGeeksUser> userStore,
            UserManager<DataGeeksUser> userManager,
            IUserRepository userRepository,
            IEmailSender emailSender,
            ApplicationDbContext dbContext
            )
        {
            
            _roleRepository = roleRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _emailStore = GetEmailStore();
            _userStore = userStore;
            _emailSender = emailSender;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _userRepository.GetUsers());
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetUser(id);
            var roles = await _roleRepository.GetRoles();
            if (user == null) return NotFound();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
            var rolesItems = roles.Select(r => new SelectListItem(r.Name, r.Id, userRoles.Any(s => s.Contains(r.Name)))).ToList();

            var res = new UserViewModel
            {

                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Roles = rolesItems,
            };
            return View(res);
        }

        [HttpGet]

        private async Task<UserViewModel> LoadView(string id)
        {
            var user = await _userRepository.GetUser(id);
            var roles = await _roleRepository.GetRoles();
            if (user == null) return new UserViewModel();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
            var rolesItems = roles.Select(r => new SelectListItem(r.Name, r.Id, userRoles.Any(s => s.Contains(r.Name)))).ToList();

            return new UserViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Id = user.Id,
                Roles = rolesItems,
            };

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var rolesCount = model.Roles.Count(s => s.Selected);

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Surname) | string.IsNullOrEmpty(model.Email) || rolesCount < 1)
            {
                if (rolesCount < 1)
                {
                    ViewBag.Message = "Please select at least one role!";
                }
                return View(model);
            }
            var user = await _userRepository.GetUser(model.Id);
            if (user == null) return NotFound();


            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                var assignedRole = userRoles.FirstOrDefault(s => s == role.Text);
                if (role.Selected && assignedRole == null)
                    await _signInManager.UserManager.AddToRoleAsync(user, role.Text);
                else if (assignedRole != null && !role.Selected)
                    await _signInManager.UserManager.RemoveFromRoleAsync(user, role.Text);
            }

            var updateUser = await _userRepository.UpdateUser(user);

            return RedirectToAction("Index", "User", new { area = "" });
        }

        private async Task<UserViewModel> LoadView()
        {
            var roles = await _roleRepository.GetRoles();
            var roleItems = roles.Select(r => new SelectListItem(r.Name, r.Id, false)).ToList();

            return new UserViewModel
            {
                Roles = roleItems,
            };
        }


        [HttpGet]
        public async Task<IActionResult> AddUser()
        {

            return View(await LoadView());
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            var rolesCount = model.Roles.Count(s => s.Selected);

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Surname) | string.IsNullOrEmpty(model.Email) || rolesCount < 1)
            {
                if (rolesCount < 1)
                {
                    ViewBag.Message = "Please select at least one role!";
                }
                return View(model);
            }
            var user = CreateUser();

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;

            string returnUrl = Url.Content("~/");

            await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
            //await _emailStore.SetEmailAsync(user, model.Email,CancellationToken.None);
            var result = await _userManager.CreateAsync(user, "Datageeks@01");
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/User/ConfirmEmail",
                pageHandler: null,
                values: new { /*area = "Identity", */UserId = userId, token = code, returnUrl = returnUrl },
                protocol: Request.Scheme);

          
            if (result.Succeeded)
            {
                foreach (var role in model.Roles)
                {
                    if (role.Selected)
                    {
                        await _signInManager.UserManager.AddToRoleAsync(user, role.Text);
                    }
                }
                return RedirectToAction("Index", "User", new { area = "" });
            }

            return View(model);
        }
        
       
        [HttpGet]

        public IActionResult Succeeded()
        {
            if (TempData["Succeeded"] != null)
            {
                ViewBag.success = TempData["Succeeded"];
                TempData.Clear();
            }
            return View();
        }
        [HttpGet]

        public IActionResult AccessDenied()
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
                TempData.Clear();
            }
            return View();
        }

        [HttpGet]

        public IActionResult Error()
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
                TempData.Clear();
            }
            return View();
        }
        
        private DataGeeksUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<DataGeeksUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(DataGeeksUser)}'. " +
                    $"Ensure that '{nameof(DataGeeksUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<DataGeeksUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<DataGeeksUser>)_userStore;
        }
    }
}
