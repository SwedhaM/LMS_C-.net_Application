using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;  
using System.Configuration;  
using System.Data;    
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using LibraryManagementSystem.Filters;

namespace LibraryManagementSystem.Controllers;

// [AllowAnonymous]
public class LoginController:Controller{
    private readonly UserDetailDbContext db;
    public IConfiguration Configuration { get; }
    private readonly SignupDbContext _context;

    private readonly  SignupDbContext _con;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<LoginController> _logger;
        public LoginController(SignupDbContext context1,IConfiguration configuration,ILogger<LoginController> logger ,UserDetailDbContext context, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            db = context;
            _environment = environment;
            _logger = logger;

            _context = context1;
        }

[LoggingActionFilter]
[HttpGet]
    public async Task<IActionResult> AdminProfile()
    {
        var email = HttpContext.Session.GetString("EmailID");
        SignupViewModel u = await _context.Signup_Detail.Where(x => x.Email == email).FirstOrDefaultAsync();
        return View(u);
    }
public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("LoginPage", "Home");
    }
 
    
         
          public IActionResult Index()
    {
       
        return View();
    }

     [HttpGet]
    //  [TypeFilter(typeof(AuthorizationFilter))]
    public IActionResult AdminLogin()
    {

        ClaimsPrincipal claimUser = HttpContext.User;
        if (claimUser.Identity.IsAuthenticated)
        {
            return RedirectToAction("AdminHomePage", "Admin");
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AdminLogin(string EmailID, string Password, Login modelLogin){
        var user = await _context.Signup_Detail.FirstOrDefaultAsync(u => u.Email == EmailID && u.Password == Password);
        if (user != null){
            //if(modelLogin.EmailID==EmailID && modelLogin.Password==Password){
            HttpContext.Session.SetString("EmailID", EmailID);
            List<Claim> claims = new List<Claim>(){
                                new Claim(ClaimTypes.NameIdentifier,EmailID)
                            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties(){
                AllowRefresh = true,
                IsPersistent = modelLogin.KeepLoggedIn
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), properties);
            _logger.LogInformation("{EmailID} logged in successfully.", EmailID);
            return RedirectToAction("AdminHomePage", "Admin");
        }
        ViewData["LoginSuccess"] = false;
        return View();
    }
    

    public IActionResult AdminRegister(Register register)
    {
           
                bool IsInserted= false;
                if(ModelState.IsValid){
             
                    IsInserted=Database.AdminRegister(register);
                    if(IsInserted){
                        ViewBag.Message="Inserted successfully";
                        return View("AdminLogin");
                    }
                    else{
                        TempData["Error"]="Not inserted";
                    }
                }
                return View();
                  
          //HttpContext.Session.SetString("EmailID ", register.EmailID);
          
       }
              [HttpGet]
public IActionResult UserLogin()
{
    // Check if the "RememberMe" cookie is present
    if (Request.Cookies.ContainsKey("RememberMe"))
    {
        string email = Request.Cookies["RememberMe"];
        // Pre-fill the email field in the login form
        var login = new Login { EmailID = email };
        return View(login);
    }

    return View();
}

[HttpPost]
public IActionResult UserLogin(Login login, bool rememberMe)
{
    string result = Database.UserLogin(login);
    Console.WriteLine(result);
    if (result == "success")
    {
        HttpContext.Session.SetString("EmailID", login.EmailID);

        if (rememberMe)
        {
            // Set the "RememberMe" cookie with the email value
            Response.Cookies.Append("RememberMe", login.EmailID, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(30),
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });
        }

        return RedirectToAction("UserHomePage", "User");
    }

    return RedirectToAction("UserLogin", "Login");
}

public IActionResult UserLogOut()
{
    return RedirectToAction("UserLogin", "Login");
}



    //    [HttpGet]
    // public IActionResult UserLogin()
    // {
       
    //     return View();
    // }
    
    // [HttpPost]
    //  public IActionResult UserLogin(Login login)
    // {
    //    string result= Database.UserLogin(login);
    //    Console.WriteLine(result);
    //    if(result=="success")
    //    {
    //       HttpContext.Session.SetString("EmailID", login.EmailID);
    //       return RedirectToAction("UserHomePage","User");
    //    }
      
    //    return RedirectToAction("UserLogin","Login");
        
    // }
    [HttpGet]
    public IActionResult UserProfile(UserDetail userDetail)
    {
        var email = HttpContext.Session.GetString("EmailID");
          var u = from s in db.UserDetails
                     where s.EmailID == email
                     select s;
        return View(u);
    }

    [HttpGet]

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)

        {



            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;


            var email = HttpContext.Session.GetString("EmailID");
          var li = from s in db.UserDetails
                     where s.EmailID == email
                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.Name.Contains(searchString)
                                       || s.EmailID.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.Name);
                    break;
                default:
                    li = li.OrderBy(s => s.RegisterNo);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<UserDetail>.CreateAsync(li.AsNoTracking(), pageNumber ?? 1, pageSize));
            //  return View(await li.AsNoTracking().ToListAsync());
        }
}

        