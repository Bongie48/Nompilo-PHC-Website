using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nompilo_PHC_Website.Data;

namespace Nompilo_PHC_Website.Repository
{
    public interface IRoleRepository
    {
        Task<List<IdentityRole>> GetRoles();

    }
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<IdentityRole>> GetRoles()
        {
            return _dbContext.Roles.Where(s => s.Name != "Patient" && s.Name != "Receptionist").ToListAsync();
        }
    }
}
