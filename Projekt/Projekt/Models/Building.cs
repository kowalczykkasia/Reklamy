using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models
{
    public class Building
    {
        public Building()
        {
            CampaignsFrom = new HashSet<Campaign>();
            CampaignsTo = new HashSet<Campaign>();

        }
        public int IdBuilding { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public double Height { get; set; }

        public virtual ICollection<Campaign> CampaignsFrom { get; set; }
        public virtual ICollection<Campaign> CampaignsTo { get; set; }
    }
}
