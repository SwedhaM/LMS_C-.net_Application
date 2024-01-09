using System;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using LibraryManagementSystem.Models;
using System.Data;
namespace LibraryManagementSystem.Models{
    public class Database{
        static SqlConnection sqlconnection=new SqlConnection("Server=DESKTOP-SBSQNFC\\SQLEXPRESS;Database=LibraryManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true");

        static public string UserLogin(Login login)
        {
                
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("VerifyUser",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailID",login.EmailID);
                command.Parameters.AddWithValue("@Password", login.Password);
                int Count=Convert.ToInt32(command.ExecuteScalar());
                sqlconnection.Close();
                if(Count==1)
                {
                  return "success";         
                }
                
                  return "fails";      
        }

         static public bool AdminRegister(Register register)
        {
                
                
                SqlCommand command=new SqlCommand("AdminRegister",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName",register.FirstName);
                command.Parameters.AddWithValue("@LastName",register.LastName);
                command.Parameters.AddWithValue("@EmailID",register.EmailID);
                command.Parameters.AddWithValue("@Password",register.Password);
                command.Parameters.AddWithValue("@ConfirmPassword", register.ConfirmPassword);
                sqlconnection.Open();
                int Count=Convert.ToInt32(command.ExecuteNonQuery());
                sqlconnection.Close();
                if (Count != 0) {
                  return true;         
                }
                
                  return false;       
        }
         
             /* public string Details(UserDetail userDetail){
              sqlconnection.Open();  
            string str = "select * from Login where UserName='" + HttpContext.Session.SetString("EmailID", userDetail.EmailID) + "'";  
            SqlCommand command = new SqlCommand(str, sqlconnection);  
            SqlDataAdapter da = new SqlDataAdapter(command);  
            DataSet ds = new DataSet();  
            da.Fill(ds);
            return "success";
            } */
        


        static public string AdminLogin(Login login)
        {
                
                sqlconnection.Open();
                SqlCommand command=new SqlCommand("VerifyAdmin",sqlconnection); 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailID",login.EmailID);
                command.Parameters.AddWithValue("@Password", login.Password);
                int Count=Convert.ToInt32(command.ExecuteScalar());
                sqlconnection.Close();
                if(Count==1)
                {
                  return "success";         
                }
                
                  return "fails";      
        }
        
    }
}