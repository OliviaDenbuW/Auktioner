using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskiiiii.Models.AuctionViewModels
{
    public class TestAuctionViewModel
    {
        public AuctionDetailsViewModel AuctionDetilsViewModel { get; set; }

        public CreateAuctionViewModel CreateAuctionViewModel { get; set; }

        public UpdateAuctionViewModel UpdateAuctionViewModel { get; set; }

        public SortAuctionViewModel SortAuctionViewModel { get; set; }
    }
}
