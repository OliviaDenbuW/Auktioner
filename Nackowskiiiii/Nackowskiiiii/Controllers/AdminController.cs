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
using Nackowskiiiii.Models.UserViewModels;
using Nackowskiiiii.Services.Identity;

namespace Nackowskiiiii.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IBusinessService _businessService;
        private IUserService _userService;

        public AdminController(IBusinessService businessService,
                                IUserService userService)
        {
            _businessService = businessService;
            _userService = userService;
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
        [ValidateAntiForgeryToken]
        public IActionResult CreateNewAuction(TestAuctionViewModel newAuction)
        {
            if (ModelState.IsValid)
            {
                CreateAuctionViewModel viewModel = _businessService.GetCreateViewModel(newAuction);

                AuctionModel model = _businessService.MakeAuctionApiReady(viewModel);

                HttpResponseMessage response = _businessService.CreateNewAuction(model);

                if (response.IsSuccessStatusCode == true)
                {
                    return RedirectToAction("Index", "Home", new { message = "Auction has successfully been created" });
                }

                return RedirectToAction("CreateNewAuction", "Admin", new { auctionId = viewModel.Id, message = "Auction failed to be created" });
            }

            return View(newAuction);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateNewAuction(TestAuctionViewModel input)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AuctionViewModel viewModel = _businessService.ConvertViewModel(input.CreateAuctionViewModel);

        //        AuctionModel model = _businessService.MakeAuctionApiReady(viewModel);

        //        HttpResponseMessage response = _businessService.CreateNewAuction(model);

        //        if (response.IsSuccessStatusCode == true)
        //        {
        //            return RedirectToAction("Index", "Home", new { message = "Auction has successfully been created" });
        //        }

        //        return RedirectToAction("CreateNewAuction", "Admin", new { auctionId = viewModel.Id, message = "Auction failed to be created" });
        //    }

        //    return View(input);
        //}

        public IActionResult UpdateAuction(int id)
        {
            AuctionViewModel currentAuction = _businessService.GetAuctionById(id);
            TestAuctionViewModel testViewModel = _businessService.TestConvertViewModel(currentAuction);
            //UpdateAuctionViewModel viewModel = _businessService.ConvertViewModel(currentAuction);

            return View(testViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAuction(TestAuctionViewModel currentAuction)
        {
            if (ModelState.IsValid)
            {
                UpdateAuctionViewModel viewModel = _businessService.GetUpdateViewModel(currentAuction);

                AuctionModel model = _businessService.MakeAuctionApiReady(viewModel);

                HttpResponseMessage response = _businessService.UpdateAuction(model);

                if (response.IsSuccessStatusCode == true)
                {
                    return RedirectToAction("ViewAuctionDetails", "Auction", new { auctionId = viewModel.Id, message = "Auction was successfully updated" });
                }

                return RedirectToAction("ViewAuctionDetails", "Auction", new { auctionId = viewModel.Id, message = "Auction has not been updated" });
            }

            return View(currentAuction);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult UpdateAuction(TestAuctionViewModel input)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AuctionViewModel viewModel = _businessService.ConvertViewModel(input.UpdateAuctionViewModel);

        //        AuctionModel model = _businessService.MakeAuctionApiReady(viewModel);

        //        HttpResponseMessage response = _businessService.UpdateAuction(model);

        //        if (response.IsSuccessStatusCode == true)
        //        {
        //            return RedirectToAction("ViewAuctionDetails", "Auction", new { auctionId = viewModel.Id, message = "Auction was successfully updated" });
        //        }

        //        return RedirectToAction("ViewAuctionDetails", "Auction", new { auctionId = viewModel.Id, message = "Auction has not been updated" });
        //    }

        //    return View(input);
        //}

        public IActionResult DeleteAuction(int auctionId)
        {
            bool auctionHasBid = _businessService.GetAuctionHasBids(auctionId);

            if (auctionHasBid == false)
            {
                HttpResponseMessage response = _businessService.DeleteAuction(auctionId);

                return RedirectToAction("Index", "Home", new { message = "Auction has successfully been deleted" });
            }
            else
            {
                return RedirectToAction("ViewAuctionDetails", "Auction", new { auctionId = auctionId, message = "Auction with bids cannot be deleted" });
            }
        }

        public IActionResult AddNewAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewAdmin(AdminViewModel newAdmin)
        {
            if (ModelState.IsValid)
            {
                string result = _userService.AddNewAdmin(newAdmin);

                if (result == "Succeeded")
                {
                    return RedirectToAction("Index", "Home", new { message = "New admin has successfully been added" });
                }

                return RedirectToAction("AddNewAdmin", "Admin", new { newAdmin = newAdmin, message = "New admin has not been added" });

            }

            return View(newAdmin);
        }
    }
}