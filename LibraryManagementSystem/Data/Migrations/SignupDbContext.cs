using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;  
  
namespace LibraryManagementSystem.Models
{  
    public class SignupDbContext : DbContext  
    {  
        public SignupDbContext(DbContextOptions<SignupDbContext> options) :  
            base(options)  
        {  
  
        }  
        public DbSet<SignupViewModel> Signup_Detail{ get; set; }
        
    }  
}