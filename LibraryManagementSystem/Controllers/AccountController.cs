using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net;
using System.Data.SqlClient;
using System.Net.Mail;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

public class AccountController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration configuration;

    public AccountController(IHttpClientFactory httpClientFactory,IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5285/");
        this.configuration = configuration;
    }

    [HttpGet]
    public IActionResult Signup()
    {
        return View();
    }


        [HttpPost]
public async Task<IActionResult> Signup(SignupViewModel signupViewModel)
{
    var signupResponse = await _httpClient.PostAsJsonAsync("/signup", signupViewModel);

    if (signupResponse.IsSuccessStatusCode)
    {
        // Signup was successful, redirect to the login page
        return RedirectToAction("AdminLogin", "Login");
    }
    else
    {
        // Signup failed, display an error message to the user
        var errorMessage = await signupResponse.Content.ReadAsStringAsync();

        if (signupResponse.StatusCode == HttpStatusCode.Conflict)
        {
            TempData["ErrorMessage"] = "A user with the same email address already exists.";
        }
        else if (signupViewModel.Password != signupViewModel.Confirmpassword)
        {
            TempData["ErrorMessage"] = "The password and confirm password must match.";
        }
        else
        {
            TempData["ErrorMessage"] = errorMessage;
        }

        return View(signupViewModel);
    }
}
        // GET: Signup
        public ActionResult Index()
        {
            return View(new SignupViewModel());
        }

        // POST: Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                string password = GetPassword(model.Email);
                if (!string.IsNullOrEmpty(password))
                {
                    SendEmail(model.Email, password);
                    ViewBag.Message = "Password sent to your email address.";
                }
                else
                {
                    ViewBag.ErrorMessage = "Email not found.";
                }
            }

            return View(model);
        }

        private string GetPassword(string email)
        {
            string password = null;

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Password FROM Signup_Detail WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            password = reader["Password"].ToString();
                        }
                    }
                }
            }

            return password;
        }

       private void SendEmail(string email, string password)
{
    try
    {
        string fromEmail = configuration.GetSection("AppSettings")["Email"];
        string fromPassword = configuration.GetSection("AppSettings")["Password"];

        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(fromEmail);
        mailMessage.To.Add(email);
        mailMessage.Subject = "Your Password";
        mailMessage.Body = $"Your password is: {password}";

        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        smtpClient.EnableSsl = true;
        smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
        smtpClient.Send(mailMessage);
    }
    catch (Exception ex)
    {
        // Handle the exception here
        ViewBag.ErrorMessage = "An error occurred while sending the email: " + ex.Message;
    }
}

    }