using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Components
{
    public class ShoppingCartSummary: ViewComponent
        // it is like a small version of controller
    {
        private readonly ShoppingCart _shoppingCart;
        
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        //the parameter is the shopping cart we create in Startup.cs
        {
            _shoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            // now the return is IViewComponentResult, not ViewResult or RedirectToActionResult any more
            return View(shoppingCartViewModel);
        }
    }
}
