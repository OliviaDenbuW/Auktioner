using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nackowskiiiii.BusinessLayer;
using Nackowskiiiii.Models;
using Nackowskiiiii.Models.AuctionViewModels;
using Nackowskiiiii.Services.Identity;

namespace Nackowskiiiii.Controllers
{
    public class AuctionController : Controller
    {
        private IBusinessService _businessService;
        private IUserService _userService;

        public AuctionController(IBusinessService businessService,
                                 IUserService userService)
        {
            _businessService = businessService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SortAuctionByEndDate()
        {
            SortAuctionViewModel viewModel = new SortAuctionViewModel();

            viewModel.SortTypes.Add(new SelectListItem { Text = "End date", Value = "1" });
            viewModel.SortTypes.Add(new SelectListItem { Text = "Start price", Value = "2" });

            return View(viewModel);
        }

        public IActionResult SortAuctionByEndDate(string input)
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchForAuctionWithParam(string title)
        {
            return View();
        }

        public IActionResult ViewAuctionDetails(int auctionId)
        {
            GeneralAuctionViewModel currentAuction = _businessService.GetAuctionById(auctionId);
            TestAuctionViewModel testViewModel = _businessService.TestConvertViewModel(currentAuction);

            testViewModel.GeneralAuctionViewModel.AuctionIsOpen = _businessService.GetAuctionIsOpen(auctionId);
            testViewModel.GeneralAuctionViewModel.HighestBidForAuction = _businessService.GetHighestBidForAuction(auctionId);

            //currentAuction.HighestBidForAuction = _businessService.GetHighestBidForAuction(auctionId);

            return View(testViewModel);
        }

        [HttpPost]
        public IActionResult MakeBidOnAuction(BidViewModel viewModel)
        {
            bool currentBidIsValid = _businessService.GetCurrentBidIsValid(viewModel.Bid, viewModel.AuctionId);
            
            if (currentBidIsValid == true)
            {
                string currentUserName = _userService.GetCurrentUserName();

                BidModel bid = _businessService.MakeBidApiReady(viewModel, currentUserName);

                HttpResponseMessage response = _businessService.MakeBid(bid);

                return RedirectToAction("ViewAuctionDetails", "Auction", new { auctionId = viewModel.AuctionId, message = "Bid has successfully been made" });
            }
            else
            {
                return RedirectToAction("ViewAuctionDetails", "Auction", new { auctionId = viewModel.AuctionId, message = "Bid is too low" });
            }
        }
    }
}