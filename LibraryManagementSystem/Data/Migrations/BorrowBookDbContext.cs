using LibraryManagementSystem.Models;  
using Microsoft.EntityFrameworkCore;  
  
namespace LibraryManagementSystem.Data  
{  
    public class BorrowBookDbContext : DbContext  
    {  
        public BorrowBookDbContext(DbContextOptions<BorrowBookDbContext> options) :  
            base(options)  
        {  
  
        }  
        public DbSet<BorrowBook> BorrowBooks { get; set; }  
    }  
}  