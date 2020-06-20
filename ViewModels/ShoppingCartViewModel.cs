using BethanysPieShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.ViewModels
{
    public class ShoppingCartViewModel
    {
        //it's not a constructure
        /*
        private readonly ShoppingCart _shoppingCart;
        private readonly decimal _shoppingCartTotal;
        public ShoppingCartViewModel(ShoppingCart shoppingCart, decimal shoppingCartTotal)
        {
            _shoppingCart = shoppingCart;
            _shoppingCartTotal = shoppingCartTotal;
        }
        */
        public ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
