using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskiiiii.Models.AuctionViewModels
{
    public class AuctionViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string StartDateString { get; set; }

        public DateTime StartDate { get; set; }

        public string EndDateString { get; set; }

        public DateTime EndDate { get; set; }

        public string GroupCode { get; set; }

        public string StartPrice { get; set; }

        public string CreatedBy { get; set; }
    }
}
