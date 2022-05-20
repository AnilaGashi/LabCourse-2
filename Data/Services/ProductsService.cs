using KosovoTeam.Data.Base;
using KosovoTeam.Data.ViewModels;
using KosovoTeam.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KosovoTeam.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        private readonly ApplicationDbContext _context;
        public ProductsService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                TrailerURL = data.TrailerURL,
                TeamId = data.TeamId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                CategoryId = data.CategoryId,
                StaffId = data.StaffId,
                
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var playerId in data.PlayerIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    ProductId = newProduct.Id,
                    PlayerId = playerId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails = await _context.Products
                .Include(c => c.Team)
                .Include(p => p.Staff)
                .Include(e => e.Category)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Player)
                .FirstOrDefaultAsync(n => n.Id == id);

            return productDetails;
        }

        public async Task<NewProductDropdownsVM> GetNewProductDropdownsValues()
        {
            var response = new NewProductDropdownsVM()
            {
                Players = await _context.Players.OrderBy(n => n.FullName).ToListAsync(),
                Teams = await _context.Teams.OrderBy(n => n.Name).ToListAsync(),
                Staffs = await _context.Staffs.OrderBy(n => n.FullName).ToListAsync(),
                Categories = await _context.Category.OrderBy(n => n.ProductCategory).ToListAsync()

            };

            return response;
        }

        public async Task UpdateProductAsync(NewProductVM data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbProduct != null)
            {
                dbProduct.Name = data.Name;
                dbProduct.Description = data.Description;
                dbProduct.Price = data.Price;
                dbProduct.ImageURL = data.ImageURL;
                dbProduct.TrailerURL = data.TrailerURL;
                dbProduct.TeamId = data.TeamId;
                dbProduct.StartDate = data.StartDate;
                dbProduct.EndDate = data.EndDate;
                dbProduct.CategoryId = data.CategoryId;
                dbProduct.StaffId = data.StaffId;
                await _context.SaveChangesAsync();

            }

            //Remove existing actors
            var existingPlayersDb = _context.Actors_Movies.Where(n => n.ProductId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingPlayersDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var playerId in data.PlayerIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    ProductId = data.Id,
                    PlayerId = playerId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    
    }
  }
