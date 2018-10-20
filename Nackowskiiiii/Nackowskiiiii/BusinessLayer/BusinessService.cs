using Microsoft.AspNetCore.Identity;
using Nackowskiiiii.Data;
using Nackowskiiiii.DataLayer;
using Nackowskiiiii.Models;
using Nackowskiiiii.Models.AuctionViewModels;
using Nackowskiiiii.Models.UserViewModels;
using Nackowskiiiii.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nackowskiiiii.BusinessLayer
{
    public class BusinessService : IBusinessService
    {
        private IUserService _userService;
        private IAuctionRepository _auctionRepository;
        private IUserRepository _userRepository;
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        private string _apiKey;
        private string _admin;

        public BusinessService(IUserService userService,
                               IAuctionRepository auctionRepository,
                               IUserRepository userRepository,
                               ApplicationDbContext context,
                               UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _auctionRepository = auctionRepository;
            _userRepository = userRepository;
            _context = context;
            _userManager = userManager;
            _apiKey = "1080";
            _admin = "Admin";
        }

        #region Create
        //Klar
        public HttpResponseMessage CreateNewAuction(AuctionModel newAuction)
        {
            return _auctionRepository.CreateNewAuction(newAuction);
        }

        //TODO (fixa så AuctionIsOpen får värde i GetCreateViewModel (nu i controller i createAuctionMethod)
        public CreateAuctionViewModel GetCreateViewModel(TestAuctionViewModel input)
        {
            CreateAuctionViewModel viewModel = new CreateAuctionViewModel
            {
                Title = input.CreateAuctionViewModel.Title,
                Description = input.CreateAuctionViewModel.Description,
                StartDateString = input.CreateAuctionViewModel.StartDateString,
                EndDateString = input.CreateAuctionViewModel.EndDateString,
                GroupCode = _apiKey,
                StartPrice = input.CreateAuctionViewModel.StartPrice,
                CreatedBy = _userService.GetCurrentUserName(),
            };

            return viewModel;
        }

        //Klar
        public AuctionModel MakeAuctionApiReady(CreateAuctionViewModel viewModel)
        {
            AuctionModel model = new AuctionModel
            {
                AuktionID = viewModel.Id.ToString(),
                Titel = viewModel.Title,
                Beskrivning = viewModel.Description,
                StartDatum = viewModel.StartDateString,
                SlutDatum = viewModel.EndDateString,
                Gruppkod = _apiKey,
                Utropspris = viewModel.StartPrice.ToString(),
                SkapadAv = viewModel.CreatedBy
            };

            return model;
        }
        #endregion

        #region update
        public HttpResponseMessage UpdateAuction(AuctionModel model)
        {
            return _auctionRepository.UpdateAuction(model);
        }

        public UpdateAuctionViewModel GetUpdateViewModel(TestAuctionViewModel currentAuction)
        {
            UpdateAuctionViewModel viewModel = new UpdateAuctionViewModel
            {
                Id = currentAuction.UpdateAuctionViewModel.Id,
                Title = currentAuction.UpdateAuctionViewModel.Title,
                Description = currentAuction.UpdateAuctionViewModel.Description,
                StartDateString = currentAuction.UpdateAuctionViewModel.StartDateString,
                EndDateString = currentAuction.UpdateAuctionViewModel.EndDateString,
                GroupCode = _apiKey,
                StartPrice = currentAuction.UpdateAuctionViewModel.StartPrice,
                CreatedBy = _userService.GetCurrentUserName(),
            };

            return viewModel;
        }

        public AuctionViewModel ConvertViewModel(UpdateAuctionViewModel input)
        {
            AuctionViewModel viewModel = new AuctionViewModel
            {
                Id = input.Id,
                Title = input.Title,
                Description = input.Description,
                StartDateString = input.StartDateString,
                EndDateString = input.EndDateString,
                GroupCode = _apiKey,
                StartPrice = input.StartPrice,
                CreatedBy = _admin,
            };

            return viewModel;
        }

        public AuctionModel MakeAuctionApiReady(UpdateAuctionViewModel viewModel)
        {
            AuctionModel model = new AuctionModel
            {
                AuktionID = viewModel.Id.ToString(),
                Titel = viewModel.Title,
                Beskrivning = viewModel.Description,
                StartDatum = viewModel.StartDateString,
                SlutDatum = viewModel.EndDateString,
                Gruppkod = _apiKey,
                Utropspris = viewModel.StartPrice.ToString(),
                SkapadAv = viewModel.CreatedBy
            };

            return model;
        }
        #endregion

        #region delete
        public HttpResponseMessage DeleteAuction(int id)
        {
            return _auctionRepository.DeleteAuction(id);
        }
        #endregion


        //Klar
        public TestAuctionViewModel TestConvertViewModel(GeneralAuctionViewModel currentAuction)
        {
            TestAuctionViewModel viewModel = new TestAuctionViewModel
            {
                GeneralAuctionViewModel = new GeneralAuctionViewModel
                {
                    Id = currentAuction.Id,
                    Title = currentAuction.Title,
                    Description = currentAuction.Description,
                    StartDateString = currentAuction.StartDateString,
                    EndDateString = currentAuction.EndDateString,
                    GroupCode = _apiKey,
                    StartPrice = currentAuction.StartPrice,
                    CreatedBy = currentAuction.CreatedBy,
                    Bids = GetAllBidViewModelsForCurrentAuction(currentAuction.Id)
                }
            };

            return viewModel;
        }

        //Test
        public IEnumerable<TestAuctionViewModel> TestGetAllAuctions()
        {
            IEnumerable<AuctionModel> allAuctions = _auctionRepository.GetAllAuctions();
            IEnumerable<TestAuctionViewModel> viewModelList = TestCreateAuctionListIEnumerable(allAuctions);

            return viewModelList;
        }

        //Test
        public IEnumerable<TestAuctionViewModel> TestCreateAuctionListIEnumerable(IEnumerable<AuctionModel> auctions)
        {
            var viewModelList =
            auctions.Select(x => new TestAuctionViewModel
            {
                GeneralAuctionViewModel = new GeneralAuctionViewModel
                {
                    Id = Int32.Parse(x.AuktionID),
                    Title = x.Titel,
                    Description = x.Beskrivning,
                    StartDateString = x.StartDatum,
                    EndDateString = x.SlutDatum,
                    GroupCode = _apiKey,
                    StartPrice = decimal.Parse(x.Utropspris),
                    CreatedBy = _admin
                        //AuctionIsOpen = TestGetAuctionIsOpen(Int32.Parse(auctionDb.AuktionID))
                }
            }).OrderBy(auction => auction.GeneralAuctionViewModel.Title);

            return viewModelList;
        }

        //TODO Ta bort om ovan fungerar
        public List<AuctionViewModel> CreateAuctionList(IEnumerable<AuctionModel> auctions)
        {
            List<AuctionViewModel> viewModelList = auctions
                .Select(auctionDb => new AuctionViewModel()
                {
                    Id = Int32.Parse(auctionDb.AuktionID),
                    Title = auctionDb.Titel,
                    Description = auctionDb.Beskrivning,
                    StartDateString = auctionDb.StartDatum,
                    EndDateString = auctionDb.SlutDatum,
                    GroupCode = _apiKey,
                    StartPrice = decimal.Parse(auctionDb.Utropspris),
                    CreatedBy = _admin
                    //AuctionIsOpen = TestGetAuctionIsOpen(Int32.Parse(auctionDb.AuktionID))
                }).OrderBy(x => x.Title).ToList();

            return viewModelList;
        }

        public AuctionModel MakeAuctionApiReady(AuctionViewModel viewModel)
        {
            AuctionModel model = new AuctionModel
            {
                AuktionID = viewModel.Id.ToString(),
                Titel = viewModel.Title,
                Beskrivning = viewModel.Description,
                StartDatum = viewModel.StartDateString,
                SlutDatum = viewModel.EndDateString,
                Gruppkod = _apiKey,
                Utropspris = viewModel.StartPrice.ToString(),
                SkapadAv = _admin
            };

            return model;
        }

        //Test
        public GeneralAuctionViewModel CreateGeneralAuctionViewModel(AuctionModel model)
        {
            GeneralAuctionViewModel viewModel = new GeneralAuctionViewModel
            {
                Id = Int32.Parse(model.AuktionID),
                Title = model.Titel,
                Description = model.Beskrivning,
                StartDateString = model.StartDatum,
                EndDateString = model.SlutDatum,
                GroupCode = _apiKey,
                StartPrice = decimal.Parse(model.Utropspris),
                CreatedBy = model.SkapadAv/*_admin*/,
                //AuctionIsOpen = TestGetAuctionIsOpen(Int32.Parse(model.AuktionID)),
                Bids = GetAllBidViewModelsForCurrentAuction(Int32.Parse(model.AuktionID))
            };

            return viewModel;
        }

        //TODO: Kanske ta bort om ovan funkar
        public AuctionViewModel CreateAuctionViewModel(AuctionModel model)
        {
            AuctionViewModel viewModel = new AuctionViewModel
            {
                Id = Int32.Parse(model.AuktionID),
                Title = model.Titel,
                Description = model.Beskrivning,
                StartDateString = model.StartDatum,
                EndDateString = model.SlutDatum,
                GroupCode = _apiKey,
                StartPrice = decimal.Parse(model.Utropspris),
                CreatedBy = _admin,
                Bids = GetAllBidViewModelsForCurrentAuction(Int32.Parse(model.AuktionID))
                //AuctionIsOpen = TestGetAuctionIsOpen(Int32.Parse(model.AuktionID))
            };

            return viewModel;
        }

        //TODO KOlla vart den ska ligga och om den används
        public decimal GetMinValidBid(int auctionId)
        {
            decimal minValidBid;
            bool auctionHasBids = GetAuctionHasBids(auctionId);

            if (auctionHasBids == true)
            {
                decimal currentHighestBid = GetHighestBidForAuction(auctionId);
                minValidBid = currentHighestBid + 1;
            }
            else
            {
                AuctionViewModel viewModel = GetAuctionById(auctionId);
                decimal startPrice = viewModel.StartPrice;
                minValidBid = startPrice + 1;
            }

            return minValidBid;
        }

        //Test
        public GeneralAuctionViewModel TestGetAuctionById(int id)
        {
            IEnumerable<AuctionModel> allAuctions = _auctionRepository.GetAllAuctions();
            AuctionModel model = allAuctions.SingleOrDefault(x => x.AuktionID == id.ToString());
            GeneralAuctionViewModel viewModel = CreateGeneralAuctionViewModel(model);

            return viewModel;
        }

        //TODO Kanske ta bort den här sen om ovan funkar
        public AuctionViewModel GetAuctionById(int id)
        {
            IEnumerable<AuctionModel> allAuctions = _auctionRepository.GetAllAuctions();
            AuctionModel model = allAuctions.SingleOrDefault(x => x.AuktionID == id.ToString());
            AuctionViewModel viewModel = CreateAuctionViewModel(model);

            return viewModel;
        }

        //TEst CreateSearchList
        //public IEnumerable<TestAuctionViewModel> SetSearchAuctionViewModelList(IEnumerable<TestAuctionViewModel> auctions)
        //{
        //    IEnumerable<TestAuctionViewModel> viewModelList =
        //        auctions.Select(x => new TestAuctionViewModel
        //        {
        //            SearchAuctionViewModel = new SearchAuctionViewModel
        //            {
        //                Title = x.GeneralAuctionViewModel.Title,
        //                Description = x.GeneralAuctionViewModel.Description
        //            }
        //        }).OrderBy(auction => auction.SearchAuctionViewModel.Title);

        //    return viewModelList;
        //}

        //Test
        public IEnumerable<TestAuctionViewModel> TEstGetAuctionSearchResult(string searchInput)
        {
            IEnumerable<AuctionModel> allAuctionsDb = _auctionRepository.GetAllAuctions();
            IEnumerable<TestAuctionViewModel> allAuctions = TestCreateAuctionListIEnumerable(allAuctionsDb);

            //IEnumerable<TestAuctionViewModel> searchResultTest = SetSearchAuctionViewModelList(allAuctions);

            if (searchInput != null)
            {
                IEnumerable<TestAuctionViewModel> searchResult = allAuctions.Where(
                                                           auction => auction.GeneralAuctionViewModel.Title.ToLower().Contains(searchInput.ToLower()) ||
                                                           auction.GeneralAuctionViewModel.Description.ToLower().Contains(searchInput.ToLower())).ToList();

                //IEnumerable<TestAuctionViewModel> searchResult = searchResultTest.Where(
                //                                           auction => auction.SearchAuctionViewModel.Title.ToLower().Contains(searchInput.ToLower()) ||
                //                                           auction.SearchAuctionViewModel.Description.ToLower().Contains(searchInput.ToLower())).ToList();

                return searchResult;
            }

            return allAuctions;
        }

        //TODO Ta bort om ovan funkar
        public List<AuctionViewModel> GetAuctionSearchResult(string searchInput)
        {
            IEnumerable<AuctionModel> allAuctionsDb = _auctionRepository.GetAllAuctions();
            
            List<AuctionViewModel> searchResult = new List<AuctionViewModel>();

            if (searchInput != null)
            {
                IEnumerable<AuctionModel> searchResultDb = allAuctionsDb.Where(
                                                            auction => auction.Titel.ToLower().Contains(searchInput.ToLower()) ||
                                                            auction.Beskrivning.ToLower().Contains(searchInput.ToLower()));

                searchResult = CreateAuctionList(searchResultDb);
            }
            else
            {
                searchResult = CreateAuctionList(allAuctionsDb);
            }
                

            return searchResult;
        }

        //Klar
        public IEnumerable<TestAuctionViewModel> GetOpenAuctions()
        {
            IEnumerable<AuctionModel> allAuctionsDb = _auctionRepository.GetAllAuctions();
            IEnumerable<TestAuctionViewModel> allAuctions = TestCreateAuctionListIEnumerable(allAuctionsDb);

            IEnumerable<TestAuctionViewModel> openAuctions = allAuctions.
                                                           Where(auction => auction.GeneralAuctionViewModel.AuctionIsOpen == false).ToList();

            return openAuctions;
        }

        //Test
        public bool TestGetAuctionIsOpen(int auctionId)
        {
            GeneralAuctionViewModel testCurrentAuction = TestGetAuctionById(auctionId);
            //AuctionViewModel currentAuction = GetAuctionById(auctionId);
            String todaysDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            String endDateAuction = testCurrentAuction.EndDateString;

            DateTime todayConverted = DateTime.Parse(todaysDate);
            DateTime endDateConverted = DateTime.Parse(endDateAuction);

            if (endDateConverted > todayConverted)
            {
                //currentAuction.AuctionIsOpen = true;
                return true;
            }
            else
            {
                //currentAuction.AuctionIsOpen = false;
                return false;
            }
        }
        #region Bid
        public HttpResponseMessage MakeBid(BidModel currentBid)
        {
            return _auctionRepository.MakeBid(currentBid);
        }

        public BidModel MakeBidApiReady(BidViewModel viewModel, string userName = "Pappa alex")
        {
            BidModel model = new BidModel
            {
                Summa = viewModel.Bid.ToString(),
                AuktionID = viewModel.AuctionId.ToString(),
                Budgivare = userName
            };

            return model;
        }

        public List<BidViewModel> GetAllBidViewModelsForCurrentAuction(int currentAuctionId)
        {
            IEnumerable<BidModel> allBidsDb = _auctionRepository.GetBidsForCurrentAuction(currentAuctionId);
            List<BidViewModel> bidList = CreateReturnBidList(allBidsDb).OrderByDescending(x => x.Bid).ToList();
            
            return bidList;
        }

        public List<BidViewModel> CreateReturnBidList(IEnumerable<BidModel> bidListDb)
        {
            List<BidViewModel> bidList = bidListDb
                .Select(bidDb => new BidViewModel()
                {
                    Id =  int.Parse(bidDb.AuktionID),
                    Bid = decimal.Parse(bidDb.Summa),
                    AuctionId = int.Parse(bidDb.AuktionID),
                    Bidder = bidDb.Budgivare
                }).ToList();

            return bidList;
        }

        public bool GetAuctionHasBids(int auctionId)
        {
            List<BidViewModel> bidList = GetAllBidViewModelsForCurrentAuction(auctionId);

            if (bidList.Any())
            {
                return true;
            }

            return false;
        }

        public List<decimal> GetAllPriceBidsForAuction(int auctionId)
        {
            List<BidViewModel> bidList = GetAllBidViewModelsForCurrentAuction(auctionId);

            List<decimal> bidPriceList = new List<decimal>();

            foreach (var bidViewModel in bidList)
            {
                bidPriceList.Add(bidViewModel.Bid);
            }

            return bidPriceList;
        }

        public decimal GetHighestBidForAuction(int auctionId)
        {
            List<decimal> bidPriceList = new List<decimal>();
            bidPriceList = GetAllPriceBidsForAuction(auctionId);

            decimal highestBid;

            if (bidPriceList.Count() != 0)
            {
                highestBid = bidPriceList.Max();
            }
            else
            {
                highestBid = 0;
            }

            return highestBid;
        }

        public bool GetCurrentBidIsValid(decimal newBid, int auctionId)
        {
            AuctionViewModel viewModel = GetAuctionById(auctionId);

            decimal currentHighestBid = GetHighestBidForAuction(auctionId);
            decimal startPrice = viewModel.StartPrice;

            if ((newBid > currentHighestBid) && (newBid > startPrice))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}

