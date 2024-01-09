using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class UserDetailController : Controller
    {
        private readonly UserDetailDbContext db;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _environment;
        public UserDetailController(UserDetailDbContext context, IWebHostEnvironment environment, IUserRepository userRepository)
        {
            db = context;
            _environment = environment;
            _userRepository = userRepository;
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


            var li = from s in db.UserDetails
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

        [HttpGet]
        public IActionResult Create()
        {

            UserDetail u = new UserDetail();

            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDetail ui, IFormFile file)
        {
        if (ModelState.IsValid)
        {
            if (await db.UserDetails.AnyAsync(x => x.EmailID == ui.EmailID))
            {
                ModelState.AddModelError("EmailID", "Email ID already exists");
            }

            // Check if register number already exists
            if (await db.UserDetails.AnyAsync(x => x.RegisterNo == ui.RegisterNo))
            {
                ModelState.AddModelError("RegisterNo", "Register number already exists");
            }
            if (await db.UserDetails.AnyAsync(x => x.PhoneNumber == ui.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "PhoneNumber already exists");
            }
            if (!ModelState.IsValid)
            {
                return View(ui);
            }

        // If there are any validation errors, return the view with the errors
        // if (!ModelState.IsValid)
        // {
        //     return View(ui);
        // }
            UserDetail u = new UserDetail();
            if (file == null || file.Length == 0)
            {
                u.ProfilePicture = "NoImage.png";
            }
            else
            {
                string filename = System.Guid.NewGuid().ToString() + ".png";
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "images", filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                u.ProfilePicture = filename;

            }
            u.RegisterNo = ui.RegisterNo;
            u.Name = ui.Name;
            u.EmailID = ui.EmailID;
            u.Password = ui.Password;
            u.Gender = ui.Gender;
            u.Age = ui.Age;
            u.Address = ui.Address;
            u.PhoneNumber = ui.PhoneNumber;
            
            db.UserDetails.Add(u);
            await db.SaveChangesAsync();

        var senderEmail = new MailAddress("murugaiyanlatha6@gmail.com", "Swedha M");
        var receiverEmail = new MailAddress(ui.EmailID, ui.Name);
        var password = "mmnuuajpcizuambd";
        var subject = "Welcome to the Library";
        var body = "Dear " + ui.Name + ",\n\nGreetings From LMS! \n\nHope you are doing well.\n\nWelcome to the Digital Library Management System. We have created an account for you on our platform to provide you with the necessary learning resources. Find the login credentials below. \n\nEmailID:" + ui.EmailID + "\n\nPassword:" + ui.Password +"\n\nThank you.";
        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(senderEmail.Address, password)
        };
        using (var message = new MailMessage(senderEmail, receiverEmail)
        {
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message);
        }

            return RedirectToAction("Index");

        }
        return View(ui);
    }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)

        {

            if (id == null)

            {

                return NotFound();

            }



            UserDetail u = await db.UserDetails.Where(x => x.ID == id).FirstOrDefaultAsync();





            if (u == null)

            {

                return NotFound();

            }



            return View(u);

        }







        [HttpPost]

        public async Task<IActionResult> Edit(int? id, UserDetail ui, IFormFile file)

        {

            if (id == null)

            {

                return NotFound();

            }



            UserDetail u = await db.UserDetails.Where(x => x.ID == id).FirstOrDefaultAsync();



            if (u == null)

            {

                return NotFound();

            }





            if (file != null || file.Length != 0)

            {



                string filename = System.Guid.NewGuid().ToString() + " .png";

                var path = Path.Combine(

                            Directory.GetCurrentDirectory(), "wwwroot", "images", filename);



                using (var stream = new FileStream(path, FileMode.Create))

                {

                    await file.CopyToAsync(stream);

                }


                if (db.UserDetails.Any(x => x.EmailID == ui.EmailID && x.ID != id) || 
                    db.UserDetails.Any(x => x.RegisterNo == ui.RegisterNo && x.ID != id) ||
                    db.UserDetails.Any(x => x.PhoneNumber == ui.PhoneNumber && x.ID != id))
                {
                    // A user with the same email or registration number already exists in the database
                    ModelState.AddModelError(string.Empty, "A user with the same email or registration number already exists.");
                    return View(ui);
                }
                u.ProfilePicture = filename;



            }
            u.RegisterNo = ui.RegisterNo;
            u.Name = ui.Name;
            u.EmailID = ui.EmailID;
            u.Password = ui.Password;
            u.Gender = ui.Gender;
            u.Age = ui.Age;
            u.Address = ui.Address;
            u.PhoneNumber = ui.PhoneNumber;
            await db.SaveChangesAsync();



        var senderEmail = new MailAddress("murugaiyanlatha6@gmail.com", "Swedha M");
        var receiverEmail = new MailAddress(ui.EmailID, ui.Name);
        var password = "zxbkpznmcqrxnebn";
        var subject = "Details Updation";
        var body = "Dear " + ui.Name + ",\n\nGreetings From LMS! \n\nHope you are doing well.\n\nWelcome to the Digital Library Management System. We have created an account for you on our platform to provide you with the necessary learning resources. Find the login credentials below. \n\nEmailID:" + ui.EmailID + "\n\nPassword:" + ui.Password +"\n\nThank you.";
        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(senderEmail.Address, password)
        };
        using (var message = new MailMessage(senderEmail, receiverEmail)
        {
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message);
        }


            return RedirectToAction("Index");







        }



        [HttpGet]


        public async Task<IActionResult> Details(int? id)


        {

            if (id == null)

            {

                return NotFound();

            }



            UserDetail u = await db.UserDetails.Where(x => x.ID == id).FirstOrDefaultAsync();





            if (u == null)

            {

                return NotFound();

            }



            return View(u);

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.UserDetails == null)
            {
                return NotFound();
            }

            var UserDetails = await db.UserDetails
                .FirstOrDefaultAsync(m => m.ID == id);
            if (UserDetails == null)
            {
                return NotFound();
            }

            return View(UserDetails);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.UserDetails == null)
            {
                return Problem("Entity set 'UserDetailDbContext.UserDetail'  is null.");
            }
            var UserDetails = await db.UserDetails.FindAsync(id);
            if (UserDetails != null)
            {
                db.UserDetails.Remove(UserDetails);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool BookExists(int id)
        {
            return (db.UserDetails?.Any(e => e.ID == id)).GetValueOrDefault();
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        
    }
}