using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly EcommerceContext _context;
        public CategoriesController(EcommerceContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories?.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category model, IFormFile File)
        {
            if (File != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                string filePathImage = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Category", imageName);
                using var stream = System.IO.File.Create(filePathImage);
                {
                    await File.CopyToAsync(stream);
                }
                model.CatPhoto = imageName;
            }
            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var categories = _context.Categories?.Find(id);
            return View(categories);

        }

        [HttpPost]
        public IActionResult Edit(Category model, IFormFile File)
        {
            if (File != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                string filePathImage = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Category", imageName);
                using var stream = System.IO.File.Create(filePathImage);
                {
                    File.CopyTo(stream);
                }
                model.CatPhoto = imageName;
            }
            else
            {
                model.CatPhoto = model.CatPhoto;
            }
            _context.Update(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var cat = _context.Categories?.Find(id);
                if (cat != null)
                {
                    _context.Categories?.Remove(cat);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                { }
            }
                return View();

            
        }
    }
}
