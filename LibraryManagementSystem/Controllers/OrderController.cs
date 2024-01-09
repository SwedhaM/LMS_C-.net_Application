using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Controllers{
    public class OrderController : Controller{
        private readonly OrderDbContext db5;
        private readonly IssueBookDbContext db1;
        public OrderController(OrderDbContext context3,IssueBookDbContext context){
            db5=context3;
            db1=context;
        }
        [BindProperty]
         public OrderEntity _orders {get; set;}
         public IActionResult Index(){
            return View();
        }
        public IActionResult InitiateOrder(OrderEntity orders){

            string key = "rzp_test_y3d9ISew6fCzvG";
            string secret = "CVfBShvnIZ4ZFMyQNebKZ6au";


            Random _random=new Random();
            string TransactionId=_random.Next(0,10000).ToString();
             

           Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount",Convert.ToDecimal(_orders.Total_amt )*100); 
            input.Add("currency", "INR");
            input.Add("receipt", "TransactionId");

            RazorpayClient client = new RazorpayClient(key, secret);
            Razorpay.Api.Order order = client.Order.Create(input);

            ViewBag.Orderid= order["id"].ToString();
            orders.OrderId = order["id"].ToString();
            orders.TransactionId = "";
            orders.PaymentStatus = "Pending";
            db5.orderEntities.Add(orders);
            db5.SaveChanges();

            return View("Payment",_orders);
        }
//         public IActionResult Payment(int BookID,string razorpay_payment_id,string razorpay_order_id){

//      Dictionary<string, string> attributes = new Dictionary<string, string>();  

//      attributes.Add("razorpay_payment_id",razorpay_payment_id);
//      attributes.Add("razorpay_order_id",razorpay_order_id);

//     OrderEntity ord = new OrderEntity();
//     ord.TransactionId=razorpay_payment_id;
//     ord.OrderId=razorpay_order_id;
//     ord.PaymentStatus="Paid";
    

//     if (razorpay_payment_id !=null)
//     {
//         ord.TransactionId=razorpay_payment_id;
//         ord.OrderId=razorpay_order_id;
//         ord.PaymentStatus="Paid";
//     }
//     db5.orderEntities.Update(ord);
//     db5.SaveChanges();

//     return View("Paymentsuccess",ord);
// }
public IActionResult Payment(int BookID,string razorpay_payment_id,string razorpay_order_id){
    Dictionary<string, string> attributes = new Dictionary<string, string>();  
    attributes.Add("razorpay_payment_id",razorpay_payment_id);
    attributes.Add("razorpay_order_id",razorpay_order_id);

    OrderEntity ord = db5.orderEntities.FirstOrDefault(o => o.OrderId == razorpay_order_id);
    IssueBook issueBook = db1.IssueBooks.FirstOrDefault(ib => ib.EmailID == ord.Email && ib.Penalty == ord.Total_amt);
    if (ord != null)
    {
        ord.TransactionId = razorpay_payment_id;
        ord.OrderId=razorpay_order_id;
        ord.PaymentStatus = "Paid";
        db5.orderEntities.Update(ord);
        db5.SaveChanges();


        if (ord.PaymentStatus == "Paid" && issueBook != null) {
            issueBook.PaymentStatus = "Paid";
            issueBook.OrderId = ord.OrderId;
            
            // Update the issue book record in the database
            db1.IssueBooks.Update(issueBook);
            db1.SaveChangesAsync();
        }
    }

// Update the penalty status in the issue book record if the order has been paid
    return View("Paymentsuccess",ord);
    // else
    // {
    //     // handle order not found error
    //     return View("PaymentError");
    // }
}

    }
}