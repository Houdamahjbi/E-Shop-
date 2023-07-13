using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class CartsController : Controller
    {
        private readonly EcommerceContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartsController(EcommerceContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _context.ShoppingCarts.Include(p => p.Product).Where(u => u.UserId == user.Id).ToList();

            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(ShoppingCart model, int qty)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProId == model.ProId);

            var user = await _userManager.GetUserAsync(User);

            var cart = new ShoppingCart
            {
                UserId = user.Id,
                ProId = (int)product.ProId,
                Qty = qty,
            };

            var shopcart = _context.ShoppingCarts.FirstOrDefault(u => u.UserId == user.Id && u.ProId == model.ProId);

            if (qty <= 0)
            {
                qty = 1;
            }
            if (shopcart == null)
                _context.ShoppingCarts.Add(cart);
            else
                shopcart.Qty += model.Qty;

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int id)
        {


            var user = await _userManager.GetUserAsync(User);


            var shopcart = _context.ShoppingCarts.FirstOrDefault(u => u.UserId == user.Id && u.CartId == id);

            if (shopcart != null)
                _context.ShoppingCarts.Remove(shopcart);


            _context.SaveChanges();

            return RedirectToAction(nameof(Cart));
        }
    }
}
