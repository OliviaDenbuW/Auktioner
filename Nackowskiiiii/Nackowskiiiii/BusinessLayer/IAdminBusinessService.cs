using Nackowskiiiii.Models;
using Nackowskiiiii.Models.AuctionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nackowskiiiii.BusinessLayer
{
    public interface IAdminBusinessService
    {
        HttpResponseMessage CreateNewAuction(AuctionModel newAuction);

        HttpResponseMessage UpdateAuction(AuctionModel currentAuction);

        HttpResponseMessage DeleteAuction(int id);
    }
}
