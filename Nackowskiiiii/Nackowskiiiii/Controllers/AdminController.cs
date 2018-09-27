using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nackowskiiiii.BusinessLayer;
using Nackowskiiiii.Models;
using Nackowskiiiii.Models.AuctionViewModels;

namespace Nackowskiiiii.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IRegularBusinessService _regularBusinessService;
        private IAdminBusinessService _adminBusinessService;
        
        public AdminController(IRegularBusinessService regularBusinessService,
                               IAdminBusinessService adminBusinessService)
        {
            _regularBusinessService = regularBusinessService;
            _adminBusinessService = adminBusinessService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateNewAuction()
        {
            return View();
        }

        [HttpPost]
        public HttpResponseMessage CreateNewAuction(AuctionViewModel viewModel)
        {
            AuctionModel model = _regularBusinessService.MapAuctionViewModelToAuctionModel(viewModel);

            HttpResponseMessage response = _adminBusinessService.CreateNewAuction(model);

            return response;
        }

        public IActionResult UpdateAuction(int id)
        {
            AuctionViewModel currentAuction = _regularBusinessService.GetAuctionById(id);

            return View(currentAuction);
        }

        [HttpPut]
        public HttpResponseMessage UpdateAuction(AuctionViewModel viewModel)
        {
            AuctionModel model = _regularBusinessService.MapAuctionViewModelToAuctionModel(viewModel);

            HttpResponseMessage response = _adminBusinessService.UpdateAuction(model);

            return response;
        }

        //public HttpResponseMessage UpdateAuction(int id)
        //{
        //    HttpResponseMessage response = _adminBusinessService.UpdateAuction(id);

        //    return response;
        //}

        public HttpResponseMessage DeleteAuction(int id)
        {
            HttpResponseMessage response = _adminBusinessService.DeleteAuction(id);

            return response;
        }
    }
}