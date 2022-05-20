using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using KosovoTeam.Models;

namespace KosovoTeam.Data.Services
{
    public interface IOrdersService
    {

        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
