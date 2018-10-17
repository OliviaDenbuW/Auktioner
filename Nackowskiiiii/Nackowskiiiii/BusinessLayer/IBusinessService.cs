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
        //Klar
        HttpResponseMessage CreateNewAuction(AuctionModel newAuction);

        //TODO (fixa så AuctionIsOpen får värde i GetCreateViewModel (nu i controller i createAuctionMethod)
        CreateAuctionViewModel GetCreateViewModel(TestAuctionViewModel input);

        //Klar
        AuctionModel MakeAuctionApiReady(CreateAuctionViewModel viewModel);

        HttpResponseMessage UpdateAuction(AuctionModel currentAuction);

        UpdateAuctionViewModel GetUpdateViewModel(TestAuctionViewModel viewModel);

        AuctionModel MakeAuctionApiReady(UpdateAuctionViewModel viewModel);

        //UpdateAuctionViewModel ConvertViewModel(AuctionViewModel viewModel);

        //Test
        TestAuctionViewModel TestConvertViewModel(GeneralAuctionViewModel input);

        //TODO kanske ta bort om ovan funkar
        TestAuctionViewModel TestConvertViewModel(AuctionViewModel input);

        AuctionViewModel ConvertViewModel(UpdateAuctionViewModel input);

        HttpResponseMessage DeleteAuction(int id);

        //void AddNewAdmin(AdminViewModel newAdmin);

        //bool UsernameIsUnique(string newUsername);
        #endregion

        //TODO ta bort om ovan fungerar
        List<AuctionViewModel> GetAllAuctions();

        //Test
        IEnumerable<TestAuctionViewModel> TestCreateAuctionListIEnumerable(IEnumerable<AuctionModel> auctions);

        //TODO ta bort om ovan fungerar
        List<AuctionViewModel> CreateAuctionList(IEnumerable<AuctionModel> auctions);

        AuctionModel MakeAuctionApiReady(AuctionViewModel viewModel);

        //Test
        GeneralAuctionViewModel CreateGeneralAuctionViewModel(AuctionModel model);

        //TODO kanske ta bort om ovan funkar
        AuctionViewModel CreateAuctionViewModel(AuctionModel model);

        //test
        GeneralAuctionViewModel TestGetAuctionById(int id);

        //TODO kanske ta bort om ovan funkar
        AuctionViewModel GetAuctionById(int id);

        List<AuctionViewModel> GetAuctionSearchResult(string searchInput);

        //Test
        List<TestAuctionViewModel> TestGetAllOpenAuctions();

        //Test
        bool TestGetAuctionIsOpen(int auctionId);

        //TODO kanske ta bort om ovan funkar
        //bool GetAuctionIsOpen(int auctionId);

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
