using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nackowskiiiii.BusinessLayer;
using Nackowskiiiii.Models;
using Nackowskiiiii.Models.AuctionViewModels;

namespace Nackowskiiiii.Controllers
{
    public class HomeController : Controller
    {
        private IRegularBusinessService _businessService;

        public HomeController(IRegularBusinessService businessService)
        {
            _businessService = businessService;
        }

        public IActionResult Index()
        {
            //Korrigera till att det bara är de som är aktuella och inte alla
            //List<AuctionViewModel> auctions = _businessService.GetSelectedAuctions();

            List<AuctionViewModel> auctions = _businessService.GetAllAuctionsDb();

            return View(auctions);
        }

        [HttpPost]
        public IActionResult SearchForAuction(string searchInput)
        {            
            List<AuctionViewModel> searchResult = _businessService.GetAuctionSearchResult(searchInput);

            return View("SearchResult", searchResult);
        }

        public void SortAuctionByStartPrice()
        {
            
        }

        public void SortAuctionByEndDate()
        {

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
