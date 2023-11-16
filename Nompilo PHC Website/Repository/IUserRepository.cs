using Microsoft.EntityFrameworkCore;
using Nompilo_PHC_Website.Data;

namespace Nompilo_PHC_Website.Repository
{
    public interface IUserRepository
    {
        Task<List<DataGeeksUser>> GetUsers();
        Task<DataGeeksUser> GetUser(string id);
        Task<DataGeeksUser> UpdateUser(DataGeeksUser User);
    }

    //show potential fixes>>>>implement interfaces
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DataGeeksUser> GetUser(string id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<List<DataGeeksUser>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<DataGeeksUser> UpdateUser(DataGeeksUser User)
        {
            _dbContext.Update(User);
            await _dbContext.SaveChangesAsync();
            return User;
        }
    }
}
