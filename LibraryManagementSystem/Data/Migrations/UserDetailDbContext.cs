using LibraryManagementSystem.Models;  
using Microsoft.EntityFrameworkCore;  
  
namespace LibraryManagementSystem.Data  
{  
    public class UserDetailDbContext : DbContext  
    {  
        public UserDetailDbContext(DbContextOptions<UserDetailDbContext> options) :  
            base(options)  
        {  
  
        }  
        public DbSet<UserDetail> UserDetails { get; set; }  
    }  
}  