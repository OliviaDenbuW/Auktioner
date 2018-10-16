using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskiiiii.Models.AuctionViewModels
{
    public class CreateAuctionViewModel
    {
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
        [Range(0.0, Double.MaxValue, ErrorMessage = "Start price must be over zero")]
        public decimal StartPrice { get; set; }

        public string CreatedBy { get; set; }

        public bool AuctionIsOpen { get; set; }
    }
}
