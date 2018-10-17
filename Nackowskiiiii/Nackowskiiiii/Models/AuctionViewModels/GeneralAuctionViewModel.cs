using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskiiiii.Models.AuctionViewModels
{
    public class GeneralAuctionViewModel
    {
        public GeneralAuctionViewModel()
        {
            Bids = new List<BidViewModel>();
        }

        public CreateAuctionViewModel CreateViewModel { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Title is mandatory")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is mandatory")]
        public string Description { get; set; }

        [Display(Name = "Start date")]
        [Required(ErrorMessage = "Start date is mandatory")]
        public string StartDateString { get; set; }

        [Display(Name = "End date")]
        [Required(ErrorMessage = "End date is mandatory")]
        public string EndDateString { get; set; }

        public string GroupCode { get; set; }

        [Display(Name = "Start price")]
        [Required(ErrorMessage = "Start price is mandatory")]
        public decimal StartPrice { get; set; }

        public string CreatedBy { get; set; }

        public bool AuctionIsOpen { get; set; }

        public decimal HighestBidForAuction { get; set; }

        public BidViewModel NewBid { get; set; }

        public List<BidViewModel> Bids { get; set; }

        //public List<SelectListItem> SearchFilter { get; set; }
    }
}
