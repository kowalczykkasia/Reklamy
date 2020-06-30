
using AdvertApi.DTOs;
using AdvertApi.DTOs.Requests;
using AdvertApi.Models;
using System;
using System.Collections.Generic;

namespace AdvertApi.Services
{
    public interface IClientDal
    {
        public CreatedClientResponse CreateClient(CreateClientRequest client);
        public bool CheckPassword(LoginRequest loginRequest);
        public bool GrandToken(string indexNumber, string token);

        public string CheckToken_returnLogin(string token);

        public IEnumerable<Campaign> GetAllInformation();

        public double AddCampaign(NewCampaignRequest campaign);

    }
}
