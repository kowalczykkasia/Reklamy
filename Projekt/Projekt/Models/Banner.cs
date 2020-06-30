using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models
{
    public class Banner
    {
        public int IdAdvertisement { get; set; }
        public int Name { get; set; }
        public double Price { get; set; }
        public int IdCampaign { get; set; }
        public double Area { get; set; }

        public virtual Campaign Campaign { get; set; }
    }
}
