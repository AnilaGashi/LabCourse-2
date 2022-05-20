using KosovoTeam.Data.Base;
using KosovoTeam.Data.ViewModels;
using KosovoTeam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KosovoTeam.Data.Services
{
    public interface IProductsService : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<NewProductDropdownsVM> GetNewProductDropdownsValues();
        Task UpdateProductAsync(NewProductVM data);
        Task AddNewProductAsync(NewProductVM product);
    }
}
