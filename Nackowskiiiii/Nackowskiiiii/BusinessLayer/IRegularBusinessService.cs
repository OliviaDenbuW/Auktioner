using Nackowskiiiii.Models;
using Nackowskiiiii.Models.AuctionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nackowskiiiii.BusinessLayer
{
    public interface IRegularBusinessService
    {
        //HttpResponseMessage CreateNewAuction(AuctionModel newAuction);

        //HttpResponseMessage DeleteAuction(int id);

        List<AuctionViewModel> GetAllAuctionsDb();

        AuctionModel MapAuctionViewModelToAuctionModel(AuctionViewModel viewModel);

        List<AuctionViewModel> MaterializingAuctionList(IEnumerable<AuctionModel> auctionListDb);

        AuctionViewModel GetAuctionById(int id);

        List<AuctionViewModel> GetAuctionSearchResult(string searchInput);
    }
}
