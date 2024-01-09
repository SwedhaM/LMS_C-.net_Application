using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data.Repositories{
  public class UserRepository : IUserRepository
  {
      private readonly UserDetailDbContext db;

      public UserRepository(UserDetailDbContext context)
      {
          db = context;
      }

      public async Task<bool> IsEmailAlreadyExistsAsync(string EmailID)
      {
          return await Task.Run(() => db.UserDetails.Any(x => x.EmailID == EmailID));
      }
  }
}
