
using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly EcommerceContext _context;

        public int CatId { get; private set; }

        public ProductsController(EcommerceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.Category)
                .ToList();
            return View(products);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Product model, IFormFile File)
        {
            if (File != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                string filePathImage = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Product", imageName);
                using (var stream = System.IO.File.Create(filePathImage))
                {
                    await File.CopyToAsync(stream);
                }
                model.ProImage = imageName;
            }
            _context.Add(model);
            await _context.SaveChangesAsync();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CatId", "CatName");
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View(product);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, Product model, IFormFile File)
        {
            if (File != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                string filePathImage = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Product", imageName);
                using (var stream = System.IO.File.Create(filePathImage))
                {
                    await File.CopyToAsync(stream);
                }
                model.ProImage = imageName;
            }
            else
            {
                model.ProImage = model.ProImage;
            }
            _context.Update(model);
            await _context.SaveChangesAsync();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CatId", "CatName");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                var product = _context.Products.Find(id.Value);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                }
                else
                {
                    // Product not found, return error or redirect to error page
                }
            }
            else
            {
                // ID is null, return error or redirect to error page
            }

            return RedirectToAction(nameof(Index));
        }







    }
}