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
    public class AdminBusinessService : IAdminBusinessService
    {
        private IAuctionRepository _repository;
        private IRegularBusinessService _auctionBusinessService;
        private string _apiKey;
        private string _admin;

        public AdminBusinessService(IAuctionRepository repository,
                                    IRegularBusinessService auctionBusinessService)
        {
            _repository = repository;
            _apiKey = "1080";
            _admin = "Admin";
        }

        public HttpResponseMessage CreateNewAuction(AuctionModel newAuction)
        {
            return _repository.CreateNewAuction(newAuction);
        }

        public HttpResponseMessage UpdateAuction(AuctionModel model)
        {
            return _repository.UpdateAuction(model);
        }

        public HttpResponseMessage DeleteAuction(int id)
        {
            return _repository.DeleteAuction(id);
        }
    }
}
