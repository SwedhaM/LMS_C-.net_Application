using LibraryManagementSystem.Models;  
using Microsoft.EntityFrameworkCore;  
  
namespace LibraryManagementSystem.Data  
{  
    public class OrderDbContext : DbContext  
    {  
        public OrderDbContext(DbContextOptions<OrderDbContext> options) :  
            base(options)  
        {  
  
        }  
        public DbSet<OrderEntity> orderEntities { get; set; }  
    }  
}  