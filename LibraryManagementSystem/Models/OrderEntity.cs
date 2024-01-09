using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class OrderEntity
    {
        [Key]
        public int id{get;set;}

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email{get; set;}
        [Required]
        public string Mobile{get; set;}
        [Required]
        public   double Total_amt{get; set;}

        [NotMapped]

         public string? TransactionId{get; set;}

        [Column("Order_Id")]

         public string? OrderId{get; set;}

         public string PaymentStatus{get; set;}
}
}