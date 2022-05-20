using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using KosovoTeam.Data.Cart;
using KosovoTeam.Data.Services;
using KosovoTeam.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KosovoTeam.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IProductsService productsService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _productsService = productsService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            
            return View(response);
        }
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var userRoleIsAdmin = User.IsInRole("Admin");
            if (!userRoleIsAdmin)
            {
                var item = await _productsService.GetProductByIdAsync(id);

                if (item != null)
                {
                    _shoppingCart.AddItemToCart(item);
                }
                return RedirectToAction(nameof(ShoppingCart));

            }
            return NotFound();
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _productsService.GetProductByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            if (items.Count > 0)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string userEmailAddress = User.FindFirstValue(ClaimTypes.Email); 

                await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
                await _shoppingCart.ClearShoppingCartAsync();

                return View("OrderCompleted");

            }
            return View("Error");
        }
    }
}
