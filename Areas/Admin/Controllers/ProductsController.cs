using KosovoTeam.Data;
using KosovoTeam.Data.Services;
using KosovoTeam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KosovoTeam.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;
        private readonly ApplicationDbContext _context;

        public ProductsController(IProductsService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = new AllProducts();
            var allProducts = _service.GetAllAsyncProduct();
            List<Product> temp = new List<Product>();
            products.Products = allProducts;
            var product = allProducts.OrderByDescending(x => x.Id).ToList();
            for (int i = 0; i <= 4; i++)
            {
                temp.Add(product[i]);
            }
            products.FiveProducts = temp;
            return View(products);
        }

        public IActionResult Filter(string searchString)
        {
            var allProducts = _service.GetAllAsyncProduct();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allProducts.Where(n => n.Name.ToLower().Contains(searchString) || n.Description.ToLower().Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", allProducts);
        }

        //GET: Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _service.GetProductByIdAsync(id);
            return View(productDetail);
        }

        //GET: Movies/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var productDropdownsData = await _service.GetNewProductDropdownsValues();

            ViewBag.Teams = new SelectList(productDropdownsData.Teams, "Id", "Name");
            ViewBag.Staffs = new SelectList(productDropdownsData.Staffs, "Id", "FullName");
            ViewBag.Players = new SelectList(productDropdownsData.Players, "Id", "FullName");
            ViewBag.Categories = new SelectList(productDropdownsData.Categories, "CategoryId", "ProductCategory");

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(NewProductVM product)
        {
            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();

                ViewBag.Teams= new SelectList(productDropdownsData.Teams, "Id", "Name");
                ViewBag.Staffs = new SelectList(productDropdownsData.Staffs, "Id", "FullName");
                ViewBag.Players = new SelectList(productDropdownsData.Players, "Id", "FullName");
                ViewBag.Categories = new SelectList(productDropdownsData.Categories, "CategoryId", "ProductCategory");

                return View(product);
            }

            await _service.AddNewProductAsync(product);
            TempData["save"] = "A new Product  has been Created!";
            return RedirectToAction(nameof(Index));
        }

        //GET: Movies/Edit/1
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _service.GetProductByIdAsync(id);
            if (productDetails == null) return View("NotFound");

            var response = new NewProductVM()
            {
                Id = productDetails.Id,
                Name = productDetails.Name,
                Description = productDetails.Description,
                Price = productDetails.Price,
                StartDate = productDetails.StartDate,
                EndDate = productDetails.EndDate,
                ImageURL = productDetails.ImageURL,
                TrailerURL = productDetails.TrailerURL,
                CategoryId = productDetails.CategoryId,
                TeamId = productDetails.TeamId,
                StaffId = productDetails.StaffId,
                PlayerIds = productDetails.Actors_Movies.Select(n => n.PlayerId).ToList(),
            };

            var productDropdownsData = await _service.GetNewProductDropdownsValues();
            ViewBag.Teams = new SelectList(productDropdownsData.Teams, "Id", "Name");
            ViewBag.Staffs = new SelectList(productDropdownsData.Staffs, "Id", "FullName");
            ViewBag.Players = new SelectList(productDropdownsData.Players, "Id", "FullName");
            ViewBag.Categories = new SelectList(productDropdownsData.Categories, "CategoryId", "ProductCategory");

            return View(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductVM product)
        {
            if (id != product.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();
                ViewBag.Teams = new SelectList(productDropdownsData.Teams, "Id", "Name");
                ViewBag.Staffs = new SelectList(productDropdownsData.Staffs, "Id", "FullName");
                ViewBag.Players = new SelectList(productDropdownsData.Players, "Id", "FullName");
                ViewBag.Categories = new SelectList(productDropdownsData.Categories, "CategoryId", "ProductCategory");

                return View(product);
            }

            await _service.UpdateProductAsync(product);
            TempData["edit"] = "Product has been updated!";
            return RedirectToAction(nameof(Index));
        }

        // GET: movie/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product= await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: movie/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product= await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            TempData["delete"] = "The Product has been deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}