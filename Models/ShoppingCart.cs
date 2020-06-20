using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class ShoppingCart
    {
        // get dbContext
        private readonly AppDbContext _appDbContext;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        private ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        /* 
        The IServiceProvider is going to give us access in this method 
        to the services collection. The services collection managed in the dependency injection
        */
        // this function will invoked when the server sent a request
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            // open a session for a new user/client
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            // set a database context for database
            var context = services.GetService<AppDbContext>();
            // check if there is a shopping cart with cartid, if not, I'll create a new one with Guid.
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            // set cartId to session
            session.SetString("CartId", cartId);
            // return a new shopping cart for the user;
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Pie pie, int amount)
        {
            //check if the shopping cart item exist
            var shoppingCartItem = _appDbContext.ShoppingCartItems.SingleOrDefault(
                 s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);
            // if not, we will create a new one for the user, and add it in shopping cart 
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = amount
                };

                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            // if it exists, increment the amount
            else
            {
                shoppingCartItem.Amount+= amount;
            }
            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            // see if the shopping cart exists in the class, if so, return. 
            // if not, find that in the database.
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Pie)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }
    }
}
