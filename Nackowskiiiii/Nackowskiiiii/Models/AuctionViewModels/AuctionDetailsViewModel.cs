using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskiiiii.Models.AuctionViewModels
{
    public class AuctionDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string EndDateString { get; set; }

        public decimal StartPrice { get; set; }

        public string CreatedBy { get; set; }

        public bool AuctionIsOpen { get; set; }
    }
}
