using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces{
  public interface IUserRepository
  {
      Task<bool> IsEmailAlreadyExistsAsync(string EmailID);
      }
}
