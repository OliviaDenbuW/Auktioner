using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nackowskiiiii.BusinessLayer;
using Nackowskiiiii.Models.AuctionViewModels;

namespace Nackowskiiiii.Controllers
{
    public class AuctionController : Controller
    {
        private IRegularBusinessService _regularBusinessService;

        public AuctionController(IRegularBusinessService regularBusinessService)
        {
            _regularBusinessService = regularBusinessService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewAuctionDetails(int auctionId)
        {
            AuctionViewModel currentAuction = _regularBusinessService.GetAuctionById(auctionId);

            return View(currentAuction);
        }

        public IActionResult MakeBidOnAuction(decimal bid)
        {
            //Gör logik för att kolla att budet är över det högsta

            return View();
        }
    }
}