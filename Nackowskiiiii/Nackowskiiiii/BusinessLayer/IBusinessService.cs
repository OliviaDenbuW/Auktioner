using Nackowskiiiii.Models;
using Nackowskiiiii.Models.AuctionViewModels;
using Nackowskiiiii.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nackowskiiiii.BusinessLayer
{
    public interface IBusinessService
    {
        #region Admin
        HttpResponseMessage CreateNewAuction(AuctionModel newAuction);

        CreateAuctionViewModel GetCreateViewModel(TestAuctionViewModel input);

        AuctionModel MakeAuctionApiReady(CreateAuctionViewModel viewModel);

        //AuctionViewModel ConvertViewModel(CreateAuctionViewModel viewModel);

        HttpResponseMessage UpdateAuction(AuctionModel currentAuction);

        UpdateAuctionViewModel GetUpdateViewModel(TestAuctionViewModel viewModel);

        AuctionModel MakeAuctionApiReady(UpdateAuctionViewModel viewModel);

        //UpdateAuctionViewModel ConvertViewModel(AuctionViewModel viewModel);

        TestAuctionViewModel TestConvertViewModel(AuctionViewModel input);

        AuctionViewModel ConvertViewModel(UpdateAuctionViewModel input);

        HttpResponseMessage DeleteAuction(int id);

        //void AddNewAdmin(AdminViewModel newAdmin);

        //bool UsernameIsUnique(string newUsername);
        #endregion

        List<AuctionViewModel> GetAllAuctions();

        List<AuctionViewModel> CreateAuctionList(IEnumerable<AuctionModel> auctions);

        AuctionModel MakeAuctionApiReady(AuctionViewModel viewModel);

        AuctionViewModel GetAuctionById(int id);

        List<AuctionViewModel> GetAuctionSearchResult(string searchInput);

        List<AuctionViewModel> GetAllOpenAuctions();

        bool GetAuctionIsOpen(int auctionId);

        #region Bid
        HttpResponseMessage MakeBid(BidModel bid);

        BidModel MakeBidApiReady(BidViewModel viewModel, string userName);

        List<BidViewModel> GetAllBidViewModelsForCurrentAuction(int currentAuctionId);

        List<BidViewModel> CreateReturnBidList(IEnumerable<BidModel> bidListDb);

        bool GetAuctionHasBids(int auctionId);

        List<decimal> GetAllPriceBidsForAuction(int auctionId);

        decimal GetHighestBidForAuction(int auctionId);

        bool GetCurrentBidIsValid(decimal currentBid, int auctionId);
        #endregion
    }
}
