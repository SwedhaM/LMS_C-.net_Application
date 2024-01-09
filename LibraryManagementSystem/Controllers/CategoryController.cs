using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using System.Data.SqlClient;
using LibraryManagementSystem.Data;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;

public class CategoryController : Controller
{
    private readonly BookDbContext db;
    public CategoryController(BookDbContext context, IWebHostEnvironment environment)
        {
            db = context;
        }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult MainIndex()
    {
        return View();
    }
        
        
        [HttpGet]

        public async Task<IActionResult> Horror(string sortOrder, string searchString, string currentFilter, int? pageNumber)

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


            var li = from s in db.Books
                     where s.Category == "Horror"
                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.BookName.Contains(searchString)
                                       || s.AuthorName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.BookName);
                    break;
                case "Date":
                    li = li.OrderBy(s => s.AddedOn);
                    break;
                case "date_desc":
                    li = li.OrderByDescending(s => s.AddedOn);
                    break;
                default:
                    li = li.OrderBy(s => s.BookID);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Book>.CreateAsync(li.AsNoTracking(), pageNumber ?? 1, pageSize));
            //  return View(await li.AsNoTracking().ToListAsync());
        }
        [HttpGet]

        public async Task<IActionResult> Fantasy(string sortOrder, string searchString, string currentFilter, int? pageNumber)

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


            var li = from s in db.Books
                     where s.Category == "Fantasy"
                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.BookName.Contains(searchString)
                                       || s.AuthorName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.BookName);
                    break;
                case "Date":
                    li = li.OrderBy(s => s.AddedOn);
                    break;
                case "date_desc":
                    li = li.OrderByDescending(s => s.AddedOn);
                    break;
                default:
                    li = li.OrderBy(s => s.BookID);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Book>.CreateAsync(li.AsNoTracking(), pageNumber ?? 1, pageSize));
            //  return View(await li.AsNoTracking().ToListAsync());
        }
        [HttpGet]

        public async Task<IActionResult> ScienceFiction(string sortOrder, string searchString, string currentFilter, int? pageNumber)

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


            var li = from s in db.Books
                     where s.Category == "ScienceFiction"
                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.BookName.Contains(searchString)
                                       || s.AuthorName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.BookName);
                    break;
                case "Date":
                    li = li.OrderBy(s => s.AddedOn);
                    break;
                case "date_desc":
                    li = li.OrderByDescending(s => s.AddedOn);
                    break;
                default:
                    li = li.OrderBy(s => s.BookID);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Book>.CreateAsync(li.AsNoTracking(), pageNumber ?? 1, pageSize));
            //  return View(await li.AsNoTracking().ToListAsync());
        }
        [HttpGet]

        public async Task<IActionResult> Photography(string sortOrder, string searchString, string currentFilter, int? pageNumber)

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


            var li = from s in db.Books
                     where s.Category == "Photography"
                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.BookName.Contains(searchString)
                                       || s.AuthorName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.BookName);
                    break;
                case "Date":
                    li = li.OrderBy(s => s.AddedOn);
                    break;
                case "date_desc":
                    li = li.OrderByDescending(s => s.AddedOn);
                    break;
                default:
                    li = li.OrderBy(s => s.BookID);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Book>.CreateAsync(li.AsNoTracking(), pageNumber ?? 1, pageSize));
            //  return View(await li.AsNoTracking().ToListAsync());
        }
        [HttpGet]

        public async Task<IActionResult> Environment(string sortOrder, string searchString, string currentFilter, int? pageNumber)

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


            var li = from s in db.Books
                     where s.Category == "Environment"
                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.BookName.Contains(searchString)
                                       || s.AuthorName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.BookName);
                    break;
                case "Date":
                    li = li.OrderBy(s => s.AddedOn);
                    break;
                case "date_desc":
                    li = li.OrderByDescending(s => s.AddedOn);
                    break;
                default:
                    li = li.OrderBy(s => s.BookID);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Book>.CreateAsync(li.AsNoTracking(), pageNumber ?? 1, pageSize));
            //  return View(await li.AsNoTracking().ToListAsync());
        }
        [HttpGet]

        public async Task<IActionResult> Programming(string sortOrder, string searchString, string currentFilter, int? pageNumber)

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


            var li = from s in db.Books
                     where s.Category == "Programming"
                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                li = li.Where(s => s.BookName.Contains(searchString)
                                       || s.AuthorName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.BookName);
                    break;
                case "Date":
                    li = li.OrderBy(s => s.AddedOn);
                    break;
                case "date_desc":
                    li = li.OrderByDescending(s => s.AddedOn);
                    break;
                default:
                    li = li.OrderBy(s => s.BookID);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Book>.CreateAsync(li.AsNoTracking(), pageNumber ?? 1, pageSize));
            //  return View(await li.AsNoTracking().ToListAsync());
        }
}
