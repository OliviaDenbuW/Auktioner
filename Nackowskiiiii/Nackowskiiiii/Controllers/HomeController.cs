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
            List<TestAuctionViewModel> openAuctions = _businessService.TestGetAllOpenAuctions();

            return View(openAuctions);
        }

        [HttpPost]
        public IActionResult SearchForAuction(string searchInput)
        {
            List<AuctionViewModel> searchResult = _businessService.GetAuctionSearchResult(searchInput);

            return View("SearchResult", searchResult);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
