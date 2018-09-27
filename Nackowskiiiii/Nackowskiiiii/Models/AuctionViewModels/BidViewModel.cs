using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskiiiii.Models.AuctionViewModels
{
    public class BidViewModel
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int AuctionId { get; set; }

        public string Bidder { get; set; }
    }
}
