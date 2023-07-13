using ecommerce.Areas.Admin.Controllers;
using ecommerce.Data;
using ecommerce.Models;
using ecommerce.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        //private readonly EcommerceContext _logger;
        private readonly EcommerceContext _context;



        public HomeController(ILogger<HomeController> logger, EcommerceContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<List<Product>> GetPage(IQueryable<Product> result, int pagenumber)
        {
            const int Pagesize = 2;
            decimal rowCount = await _context.Products.CountAsync();
            var pagecount = Math.Ceiling(rowCount / Pagesize);
            if (pagenumber > pagecount)
            {
                pagenumber = 1;
            }
            pagenumber = pagenumber <= 0 ? 1 : pagenumber;
            int skipCount = (pagenumber - 1) * Pagesize;
            var pageData = await result
                .Skip(skipCount)
                .Take(Pagesize)
                .ToListAsync();

            ViewBag.CurrentPage = pagenumber;
            ViewBag.PageCount = pagecount;


            return pageData;
        }


        public IActionResult Index()
        {
            var model = new IndexVM()
            {
                Categories = _context.Categories.ToList(),
                Products = _context.Products.ToList()
            };
            return View(model);
        }

        private object IndexVM()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Product(int page = 1)
        {
            var products = _context.Products;
            var model = await GetPage(products, page);
            return View(model);
        }

        public IActionResult ProductCategory(int id )
        {
            var products = _context.Products
                .Where(c=> c.CatId==id)
                .ToList();
            return View(products);
        }

        [HttpPost]

        public IActionResult SearchProduct(string NamePro)
        {
            var products = _context.Products
                .Where(p => p.ProName == NamePro)
                .ToList();
            return View(products);
        }


        public IActionResult ProductDetails(int? id)
        {
            var product = _context.Products.Include(x => x.Category)
                .FirstOrDefault(p => p.ProId == id);
            return View(product);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Contact model)
        {
            if (ModelState.IsValid)
            {
                var contact = new Contact
                {
                    Name = model.Name,
                    Email = model.Email,
                    Subject = model.Subject,
                    Message = model.Message,

                };

                _context.Add(contact);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        //public IActionResult Contact(Contact model)
        //{
        // if (ModelState.IsValid)
        // {
        // Handle contact form submission here
        // ...
        //    return RedirectToAction("Index");
        // }
        // else
        // {
        //     return View(model);
        // }
        // }
        //[HttpPost]
        //public IActionResult Contact(Contact model)
        //{
        // if (ModelState.IsValid)
        //{
        //   _context.Add(model); 
        //   _context.SaveChanges(); 
        //   return RedirectToAction("Index");   
        //  }
        //  return View(model);
        // }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current.Id ?? HttpContext.TraceIdentifier });
        }
    }
}