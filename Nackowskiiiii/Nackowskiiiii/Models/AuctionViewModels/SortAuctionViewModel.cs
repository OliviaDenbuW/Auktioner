using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskiiiii.Models.AuctionViewModels
{
    public class SortAuctionViewModel
    {
        public SortAuctionViewModel()
        {
            SortTypes = new List<SelectListItem>();
        }

        //public int AuctionId { get; set; }

        //public DateTime EndDateString { get; set; }

        //public decimal StartPrice { get; set; }

        public List<SelectListItem> SortTypes { get; set; }
    }
}
