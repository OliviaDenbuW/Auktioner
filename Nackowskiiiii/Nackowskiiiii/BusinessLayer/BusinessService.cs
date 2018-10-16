﻿using Microsoft.AspNetCore.Identity;
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

        #region Admin
        public HttpResponseMessage CreateNewAuction(AuctionModel newAuction)
        {
            return _auctionRepository.CreateNewAuction(newAuction);
        }

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
        //Jämför med den nedanför (ta kanske bort den här)
        //public AuctionViewModel ConvertViewModel(CreateAuctionViewModel input)
        //{
        //    AuctionViewModel viewModel = new AuctionViewModel
        //    {
        //        Title = input.Title,
        //        Description = input.Description,
        //        StartDateString = input.StartDateString,
        //        EndDateString = input.EndDateString,
        //        GroupCode = _apiKey,
        //        StartPrice = input.StartPrice,
        //        CreatedBy = _userService.GetCurrentUserName()/*_admin*/,
        //    };

        //    return viewModel;
        //}

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

        //public UpdateAuctionViewModel ConvertViewModel(AuctionViewModel userInput)
        //{
        //    UpdateAuctionViewModel viewModel = new UpdateAuctionViewModel
        //    {
        //        Id = userInput.Id,
        //        Title = userInput.Title,
        //        Description = userInput.Description,
        //        StartDateString = userInput.StartDateString,
        //        EndDateString = userInput.EndDateString,
        //        GroupCode = _apiKey,
        //        StartPrice = userInput.StartPrice,
        //        CreatedBy = _userService.GetCurrentUserName(),
        //    };

        //    return viewModel;
        //}

        public TestAuctionViewModel TestConvertViewModel(AuctionViewModel input)
        {
            TestAuctionViewModel viewModel = new TestAuctionViewModel
            {
                UpdateAuctionViewModel = new UpdateAuctionViewModel
                {
                    Id = input.Id,
                    Title = input.Title,
                    Description = input.Description,
                    StartDateString = input.StartDateString,
                    EndDateString = input.EndDateString,
                    GroupCode = _apiKey,
                    StartPrice = input.StartPrice,
                    CreatedBy = _admin
                }
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

        public HttpResponseMessage DeleteAuction(int id)
        {
            return _auctionRepository.DeleteAuction(id);
        }
        #endregion

        public AuctionViewModel GetAuctionById(int id)
        {
            IEnumerable<AuctionModel> allAuctions = _auctionRepository.GetAllAuctions();
            AuctionModel model = allAuctions.SingleOrDefault(x => x.AuktionID == id.ToString());
            AuctionViewModel viewModel = CreateAuctionViewModel(model);

            return viewModel;
        }

        public List<AuctionViewModel> GetAllAuctions()
        {
            IEnumerable<AuctionModel> allAuctions = _auctionRepository.GetAllAuctions();
            List<AuctionViewModel> viewModelList = CreateAuctionList(allAuctions);

            return viewModelList;
        }

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
                    //AuctionIsOpen = GetAuctionIsOpen(Int32.Parse(auctionDb.AuktionID))
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
                //AuctionIsOpen = GetAuctionIsOpen(Int32.Parse(model.AuktionID))
            };

            return viewModel;
        }

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

        public List<AuctionViewModel> GetAllOpenAuctions()
        {
            IEnumerable<AuctionModel> allAuctions = _auctionRepository.GetAllAuctions();

            List<AuctionViewModel> allAuctionsTemp = CreateAuctionList(allAuctions);

            List<AuctionViewModel> openAuctions = new List<AuctionViewModel>();

            //bool auctionIsOpen;

            foreach (var auction in allAuctionsTemp)
            {
                bool auctionIsOpen = GetAuctionIsOpen(auction.Id);

                if (auctionIsOpen == true)
                {
                    auction.AuctionIsOpen = true;
                    openAuctions.Add(auction);
                }
                else
                {
                    auction.AuctionIsOpen = false;
                }
            }

            return openAuctions;
        }

        public bool GetAuctionIsOpen(int auctionId)
        {
            AuctionViewModel currentAuction = GetAuctionById(auctionId);
            String todaysDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            String endDateAuction = currentAuction.EndDateString;

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
