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
        CreateAuctionViewModel SetCreateViewModel(TestAuctionViewModel input);

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

        HttpResponseMessage DeleteAuction(int id);

        //Klar TODO: Kolla TestCreateAuctionListEnum
        IEnumerable<TestAuctionViewModel> TestGetAllAuctions();

        //Klar TODO Fixa så att AuctionIsOpen sätts här
        IEnumerable<TestAuctionViewModel> SetGeneralAuctionViewModelList(IEnumerable<AuctionModel> auctions);

        //TODO Ta bort om jag sen märker att den inte behövdes
        AuctionModel MakeAuctionApiReady(GeneralAuctionViewModel viewModel);
        
        //Klar
        GeneralAuctionViewModel CreateGeneralAuctionViewModel(AuctionModel model);

        //test
        GeneralAuctionViewModel GetAuctionById(int id);

        //TODO Välj ha kvar OM någon förklarar hur man kan skicka två olika modeller in i samma partial (Index och searchResult för _display)
        //IEnumerable<TestAuctionViewModel> SetSearchAuctionViewModelList(IEnumerable<TestAuctionViewModel> auctions);

        //Klar
        IEnumerable<TestAuctionViewModel> GetSearchResult(string searchInput);

        //Klar
        IEnumerable<TestAuctionViewModel> GetOpenAuctions();

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
