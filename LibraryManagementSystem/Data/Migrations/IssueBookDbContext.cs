using LibraryManagementSystem.Models;  
using Microsoft.EntityFrameworkCore;  
  
namespace LibraryManagementSystem.Data  
{  
    public class IssueBookDbContext : DbContext  
    {  
        public IssueBookDbContext(DbContextOptions<IssueBookDbContext> options) :  
            base(options)  
        {  
  
        }  
        public DbSet<IssueBook> IssueBooks { get; set; }  
    }  
}  