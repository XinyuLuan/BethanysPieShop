using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}
        // Access data
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        // because I want to return a list of pie.
        //There will be a rendering file in /Views/Pie/ called list.cshtml(razor page)
        // corresponding to this function.
        public ViewResult List()
        {
            // for one specific type:
            //ViewBag.CurrentCategory = "Cheese cakes";

            // By using ViewModel:
            PieListViewModel _pieListViewModel = new PieListViewModel();
            _pieListViewModel.Pies = _pieRepository.AllPies;
            _pieListViewModel.CurrentCategory = "Cheese cakes";
            return View(_pieListViewModel);
        }
    }
}

