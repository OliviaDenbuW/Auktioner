using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskiiiii.Models.AuctionViewModels
{
    public class TestAuctionViewModel
    {
        public GeneralAuctionViewModel GeneralAuctionViewModel { get; set; }

        public CreateAuctionViewModel CreateAuctionViewModel { get; set; }

        public UpdateAuctionViewModel UpdateAuctionViewModel { get; set; }

        public SortAuctionViewModel SortAuctionViewModel { get; set; }

        public SearchAuctionViewModel SearchAuctionViewModel { get; set; }
    }
}
