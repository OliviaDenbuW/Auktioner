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
        #region Create
        //Klar
        HttpResponseMessage CreateNewAuction(AuctionModel newAuction);

        //TODO (fixa så AuctionIsOpen får värde i GetCreateViewModel (nu i controller i createAuctionMethod)
        CreateAuctionViewModel GetCreateViewModel(TestAuctionViewModel input);

        //Klar
        AuctionModel MakeAuctionApiReady(CreateAuctionViewModel viewModel);
        #endregion

        #region Update
        HttpResponseMessage UpdateAuction(AuctionModel currentAuction);

        UpdateAuctionViewModel GetUpdateViewModel(TestAuctionViewModel viewModel);

        AuctionModel MakeAuctionApiReady(UpdateAuctionViewModel viewModel);
        #endregion

        #region General
        //Klar
        TestAuctionViewModel TestConvertViewModel(GeneralAuctionViewModel input);
        #endregion

        AuctionViewModel ConvertViewModel(UpdateAuctionViewModel input);

        HttpResponseMessage DeleteAuction(int id);

        //void AddNewAdmin(AdminViewModel newAdmin);

        //bool UsernameIsUnique(string newUsername);
        

        //Klar TODO: KOlla TestCreateAuctionListEnum
        IEnumerable<TestAuctionViewModel> TestGetAllAuctions();

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

        //TODO Välj ha kvar OM någon förklarar hur man kan skicka två olika modeller in i samma partial (Index och searchResult för _display)
        //IEnumerable<TestAuctionViewModel> SetSearchAuctionViewModelList(IEnumerable<TestAuctionViewModel> auctions);

        //Test
        IEnumerable<TestAuctionViewModel> TEstGetAuctionSearchResult(string searchInput);

        //TODO Ta bort om ovan funkar
        List<AuctionViewModel> GetAuctionSearchResult(string searchInput);

        //Klar
        IEnumerable<TestAuctionViewModel> GetOpenAuctions();

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
