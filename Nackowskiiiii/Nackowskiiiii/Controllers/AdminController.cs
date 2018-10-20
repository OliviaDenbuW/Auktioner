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
        //TODO Kolla om man blir directad till Getten av Create??
        public IActionResult CreateNewAuction(TestAuctionViewModel newAuction)
        {
            if (ModelState.IsValid)
            {
                CreateAuctionViewModel viewModel = _businessService.GetCreateViewModel(newAuction);
                //viewModel.AuctionIsOpen = _businessService.TestGetAuctionIsOpen(viewModel.Id);

                AuctionModel model = _businessService.MakeAuctionApiReady(viewModel);

                HttpResponseMessage response = _businessService.CreateNewAuction(model);

                if (response.IsSuccessStatusCode == true)
                {
                    return RedirectToAction("Index", "Home", new { message = "Auction has successfully been created" });
                }

                //TODO Kolla om man blir directad till Getten av Create??
                return RedirectToAction("CreateNewAuction", "Admin", new { message = "Auction failed to be created" });
            }

            return View(newAuction);
        }

        public IActionResult UpdateAuction(int id)
        {
            GeneralAuctionViewModel currentAuction = _businessService.TestGetAuctionById(id);
            TestAuctionViewModel testViewModel = _businessService.TestConvertViewModel(currentAuction);

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