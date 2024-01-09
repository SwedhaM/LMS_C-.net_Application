using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models;

public class IssueBook
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string EmailID { get; set; }

    [Required]
    public int BookID { get; set; }

    [Required]
    public string BookName { get; set; }

    [Required]
    public int BookEdition { get; set; }
    [Required]
    public DateTime BorrowDate {get;set;}

    [Required]
    public DateTime IssueDate { get; set; }

    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public string Issued { get; set; }
    
    public DateTime ReturnDate { get; set; }
    public double? Penalty {get;set;}
    public string? PaymentStatus{get;set;}
    [NotMapped]

         public string? TransactionId{get; set;}

        [Column("OrderId")]

         public string? OrderId{get; set;}

}