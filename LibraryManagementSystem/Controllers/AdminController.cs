


using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Controllers;
[Authorize]

public class AdminController : Controller
{
    private readonly UserDetailDbContext db;
    private readonly BorrowBookDbContext DB;
    private readonly IssueBookDbContext db1;
    private readonly BookDbContext dB;
        private readonly IWebHostEnvironment _environment;
        public AdminController(IssueBookDbContext context2,BookDbContext context3,BorrowBookDbContext context1,UserDetailDbContext context, IWebHostEnvironment environment)
        {
            dB=context3;
            db = context;
            db1 = context2;
            DB= context1;
            _environment = environment;
        }
    
[HttpPost]
 public async Task<IActionResult> IssueBook(int BookID)
 {       
     BorrowBook borrowBook = await DB.BorrowBooks.FirstOrDefaultAsync(b => b.BookID == BookID);

     // Retrieve the book based on the bookID value
    

     // Create a new BorrowedBook object
     IssueBook issue = new IssueBook
     {
         BookID = borrowBook.BookID,
         BookName = borrowBook.BookName,
         BookEdition = borrowBook.BookEdition,
         Name = borrowBook.Name,
         EmailID = borrowBook.EmailID,
         BorrowDate = borrowBook.Date,
         IssueDate = DateTime.Now,
         DueDate = DateTime.Now.AddDays(10),
         Issued = "Yes"
     };

     // Add the new IssueBook object to the database and save changes
    db1.IssueBooks.Add(issue);
     await db1.SaveChangesAsync();

     // // Remove the book from the BorrowBooks table
    DB.BorrowBooks.Remove(borrowBook);
    await DB.SaveChangesAsync();

     // Redirect to the AdminHomePage action
    return RedirectToAction("AdminHomePage");
 }




    public IActionResult Index()
    {
        return View();
    }
    public IActionResult LoginPage()
    {
        return View();
    }
    public IActionResult AdminHomePage()
    {
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }
    [HttpGet]

        public async Task<IActionResult> BorrowBookDetails(string sortOrder, string searchString, string currentFilter, int? pageNumber)

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


            var li = from s in DB.BorrowBooks
                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.BookName.Contains(searchString)
                                       || s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.BookName);
                    break;
                case "Date":
                    li = li.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    li = li.OrderByDescending(s => s.Date);
                    break;
                default:
                    li = li.OrderBy(s => s.BookID);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<BorrowBook>.CreateAsync(li.AsNoTracking(), pageNumber ?? 1, pageSize));
            //  return View(await li.AsNoTracking().ToListAsync());
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
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }




}
