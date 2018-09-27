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

        IEnumerable<AuctionModel> GetAllAuctionsDb();

        HttpResponseMessage UpdateAuction(AuctionModel currentAuction);

        HttpResponseMessage DeleteAuction(int id);
    }
}
