using LibraryManagementSystem.Models;  
using Microsoft.EntityFrameworkCore;  
  
namespace LibraryManagementSystem.Data  
{  
    public class BookDbContext : DbContext  
    {  
        public BookDbContext(DbContextOptions<BookDbContext> options) :  
            base(options)  
        {  
  
        }  
        public DbSet<Book> Books { get; set; }  
    }  
}  