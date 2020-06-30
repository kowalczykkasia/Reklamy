using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models
{
    public class Campaign
    {
        public Campaign()
        {
            Banners = new HashSet<Banner>();
        }
        public int IdCampaign { get; set; }
        public int IdClient { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double PricePerSquareMeter { get; set; }
        public int FromIdBuilding { get; set; }
        public int ToIdBuilding { get; set; }

        public virtual Client Client { get; set; }

        public virtual Building BuildingFrom { get; set; }
        public virtual Building BuildingTo { get; set; }

        public virtual ICollection<Banner> Banners { get; set; }
    }
}
