using Nackowskiiiii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nackowskiiiii.DataLayer
{
    public interface IAuctionRepository
    {
        HttpResponseMessage CreateNewAuction(AuctionModel newAuction);

        IEnumerable<AuctionModel> GetAllAuctions();

        HttpResponseMessage UpdateAuction(AuctionModel currentAuction);

        HttpResponseMessage DeleteAuction(int id);

        HttpResponseMessage MakeBid(BidModel bid);

        IEnumerable<BidModel> GetBidsForCurrentAuction(int auctionId);
    }
}
