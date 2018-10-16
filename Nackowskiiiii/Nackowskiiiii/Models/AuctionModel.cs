using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Nackowskiiiii.Models
{
    [DataContract]
    public class AuctionModel
    {
        [DataMember]
        public string AuktionID { get; set; }

        [DataMember]
        public string Titel { get; set; }

        [DataMember]
        public string Beskrivning { get; set; }

        [DataMember]
        public string StartDatum { get; set; }

        [DataMember]
        public string SlutDatum { get; set; }

        [DataMember]
        public string Gruppkod { get; set; }

        [DataMember]
        public string Utropspris { get; set; }

        [DataMember]
        public string SkapadAv { get; set; }
    }
}
