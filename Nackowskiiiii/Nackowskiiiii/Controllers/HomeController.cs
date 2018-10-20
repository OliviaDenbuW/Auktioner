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
        private IBusinessService _businessService;

        public HomeController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        public IActionResult Index()
        {
            IEnumerable<TestAuctionViewModel> openAuctions = _businessService.GetOpenAuctions();

            return View(openAuctions);
        }

        [HttpPost]
        public IActionResult SearchForAuction(string searchInput)
        {
            //List<AuctionViewModel> searchResult = _businessService.GetAuctionSearchResult(searchInput);
            IEnumerable<TestAuctionViewModel> searchResult = _businessService.TEstGetAuctionSearchResult(searchInput);

            return View("SearchResult", searchResult);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
