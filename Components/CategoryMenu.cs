using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepositor;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepositor = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepositor.AllCategories.OrderBy(c => c.CategoryName);
            return View(categories);
        }
    }
}
