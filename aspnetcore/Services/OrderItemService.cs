using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aspnetcore.Models;
using System;

namespace aspnetcore.Services
{
    public class OrderItemService : IOrderItemService
    {
        private StoreContext _context;
        public OrderItemService(StoreContext context)
        {
            _context = context;
        }

        #region Orders
        public async Task<IEnumerable<OrderItem>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.ToArrayAsync();

            return orders;
        }
        public async Task<OrderItem> GetOrderAsync(int OrderID)
        {
            var order = await _context.Orders.SingleAsync(t => t.ID == OrderID);

            return order;
        }
        public async Task<IEnumerable<OrderItem>> GetOrdersFor(int userID)
        {
            var orders = await _context.Orders.Where(u => u.UserID == userID).ToArrayAsync();
            
            return orders;
        }
        public async Task<int> CreateOrder(OrderItem order)
        {      
            int result = 0;
            try 
            {
                await _context.Orders.AddAsync(order);
                result = await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                System.Console.Write(e.ToString());
            }
            return result;
        }
        public async Task<int> UpdateOrder(OrderItem order)
        {
            var orderToUpdate = await _context.Orders.SingleOrDefaultAsync(s => s.ID == order.ID);
            _context.Entry(orderToUpdate).State = EntityState.Modified;
            orderToUpdate.Name = order.Name;
            orderToUpdate.StreetAddress = order.StreetAddress;
            orderToUpdate.City = order.City;
            orderToUpdate.State = order.State;
            orderToUpdate.ZipCode = order.ZipCode;
            orderToUpdate.TrackingID = order.TrackingID;
            orderToUpdate.UserID = order.UserID;  
          
            return await _context.SaveChangesAsync();
        }
        #endregion

        #region Users
        public async Task<IEnumerable<UserItem>> GetAllUsersAsync()
        {   
            return await _context.Users.ToArrayAsync();  
        }
        public async Task<int> CreateUser(UserItem user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}
