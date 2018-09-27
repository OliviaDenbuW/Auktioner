using Nackowskiiiii.DataLayer;
using Nackowskiiiii.Models;
using Nackowskiiiii.Models.AuctionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nackowskiiiii.BusinessLayer
{
    public class RegularBusinessService : IRegularBusinessService
    {
        private IAuctionRepository _repository;
        private string _apiKey;
        private string _admin;

        public RegularBusinessService(IAuctionRepository repository)
        {
            _repository = repository;
            _apiKey = "1080";
            _admin = "Admin";
        }

        public List<AuctionViewModel> GetAllAuctionsDb()
        {
            IEnumerable<AuctionModel> allAuctionsDb = _repository.GetAllAuctionsDb();

            List<AuctionViewModel> auctionList = MaterializingAuctionList(allAuctionsDb);

            return auctionList;
        }

        public AuctionModel MapAuctionViewModelToAuctionModel(AuctionViewModel viewModel)
        {
            AuctionModel model = new AuctionModel
            {
                //AuktionID = viewModel.Id.ToString(),
                Titel = viewModel.Title,
                Beskrivning = viewModel.Description,
                StartDatum = "2018-07-25T00:00:00",
                SlutDatum = "2018-09-02T00:00:00",
                Gruppkod = _apiKey,
                Utropspris = viewModel.StartPrice.ToString(),
                SkapadAv = _admin
            };

            return model;
        }

        public List<AuctionViewModel> MaterializingAuctionList(IEnumerable<AuctionModel> auctionListDb)
        {
            List<AuctionViewModel> auctionList = auctionListDb
                .Select(auctionDb => new AuctionViewModel()
                {
                    Id = Int32.Parse(auctionDb.AuktionID),
                    Title = auctionDb.Titel,
                    Description = auctionDb.Beskrivning,
                    StartDateString = auctionDb.StartDatum,
                    EndDateString = auctionDb.SlutDatum,
                    GroupCode = _apiKey,
                    StartPrice = auctionDb.Utropspris,
                    CreatedBy = _admin
                }).ToList();

            return auctionList;
        }

        public AuctionViewModel GetAuctionById(int id)
        {
            IEnumerable<AuctionModel> allAuctionsDb = _repository.GetAllAuctionsDb();

            AuctionModel model = allAuctionsDb.SingleOrDefault(auction => auction.AuktionID == id.ToString());

            AuctionViewModel viewModel = MapAuctionModelToActionViewModel(model);

            return viewModel;
        }

        public AuctionViewModel MapAuctionModelToActionViewModel(AuctionModel model)
        {
            AuctionViewModel viewModel = new AuctionViewModel
            {
                Id = Int32.Parse(model.AuktionID),
                Title = model.Titel,
                Description = model.Beskrivning,
                StartDateString = model.StartDatum,
                EndDateString = model.SlutDatum,
                GroupCode = _apiKey,
                StartPrice = model.Utropspris,
                CreatedBy = _admin
            };

            return viewModel;
        }

        public List<AuctionViewModel> GetAuctionSearchResult(string searchInput)
        {
            IEnumerable<AuctionModel> allAuctionsDb = _repository.GetAllAuctionsDb();
            IEnumerable<AuctionModel> searchResultDb = allAuctionsDb.Where(auction => auction.Titel.ToLower().Contains(searchInput.ToLower())||
                                                                                      auction.Beskrivning.ToLower().Contains(searchInput.ToLower()));

            List<AuctionViewModel> searchResult = MaterializingAuctionList(searchResultDb);

            return searchResult;
        }
    }
}
