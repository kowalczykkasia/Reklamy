using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models
{
    public class Client
    {
        public Client()
        {
            Campaigns = new HashSet<Campaign>();
        }

        public int IdClient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }

        public string salt { get; set; }

        public string RefreshToken { get; set; }

        public string Password { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }

    }
}
