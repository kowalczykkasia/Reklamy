using AdvertApi.DTOs;
using AdvertApi.DTOs.Requests;
using AdvertApi.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdvertApi.Services
{
    public class EFsqlServerDbDal : IClientDal
    {
        private readonly CodeFirstContext db;

        public EFsqlServerDbDal(CodeFirstContext context)
        {
            db = context;
        }

        public CreatedClientResponse CreateClient(CreateClientRequest client)
        { 
            var generatedSalt = CreateSalt();
            var pass = Create(client.Password, generatedSalt);

            int idClient = db.Client.Max(d => d.IdClient) + 1;

            var clt = new Client
            {
                IdClient = idClient,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Phone = client.Phone,
                Login = client.Login,
                Password = pass,
                salt = generatedSalt,
                RefreshToken = ""
            };

            var clientToReturn = new CreatedClientResponse
            {
                IdClient = idClient,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Phone = client.Phone,
                Login = client.Login
            };

            db.Attach(clt);
            db.Add(clt);
            db.SaveChanges();

            var exist = db.Client.Where(c => c.IdClient == idClient).Count();
            if (exist == 1) return clientToReturn;
            else return null;
        }

        public string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public string Create(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                                password: value,
                                                salt: Encoding.UTF8.GetBytes(salt),
                                                prf: KeyDerivationPrf.HMACSHA512,
                                                iterationCount: 10000,
                                                numBytesRequested: 256 / 8);
            return Convert.ToBase64String(valueBytes);
        }

        public bool CheckPassword(LoginRequest loginRequest)
        {
            var exist = db.Client.Where(client => client.Login == loginRequest.Login).Count();
            if (exist == 0) return false;
            var salt = db.Client.Where(client => client.Login == loginRequest.Login)
                .Select(client => client.salt).FirstOrDefault();

            var hash = Create(loginRequest.Password, salt);
            var res = db.Client
                .Where(client => client.Login == loginRequest.Login
                && client.Password == hash).Count();

            if (res > 0) return true;
            else return false;
        }

        public bool GrandToken(string login, string token)
        {
            var client = db.Client.Where(client => client.Login == login)
                .SingleOrDefault();

            if (client == null)
                return false;

            var std = new Client
            {
                Login = login,
                RefreshToken = token
            };

            db.Attach(std);
            db.Entry(std).Property("RefreshToken").IsModified = true;

            //db.SaveChanges();

            return true;

        }

        public string CheckToken_returnLogin(string token)
        {
            var login = db.Client.Where(e => e.RefreshToken == token).Select(e => e.Login).FirstOrDefault();
            if (login == null)
                return "";
            return login;
        }

        public IEnumerable<Campaign> GetAllInformation()
        {
            var list = db.Campaign.Select(e => new Campaign
            {
                IdCampaign = e.IdCampaign,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                PricePerSquareMeter = e.PricePerSquareMeter,
                Client = db.Client.Where(c => c.IdClient == e.IdClient).Select(c => new Client
                {
                    IdClient = c.IdClient,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email
                }).SingleOrDefault(),

                BuildingFrom = db.Building.Where(b => b.IdBuilding == e.FromIdBuilding).Select(b => new Building
                {
                    IdBuilding = b.IdBuilding,
                    Street = b.Street,
                    City = b.City
                }).SingleOrDefault(),

                BuildingTo = db.Building.Where(b => b.IdBuilding == e.ToIdBuilding).Select(b => new Building
                {
                    IdBuilding = b.IdBuilding,
                    Street = b.Street,
                    City = b.City
                }).SingleOrDefault(),

                Banners = db.Banner.Where(b => b.IdCampaign == e.IdCampaign).Select(b => new Banner { 
                    IdAdvertisement = b.IdAdvertisement,
                    Name = b.Name,
                    Price = b.Price,
                    Area = b.Area
                }).ToList()
            }).OrderByDescending(e => e.StartDate).ToList();

            return list;
        }

        public double AddCampaign(NewCampaignRequest campaign)
        {
            CalculationService calculation = new CalculationService();

            var B1 = db.Building.Where(b => b.IdBuilding == campaign.FromIdBuilding)
                .SingleOrDefault();

            var B2 = db.Building.Where(b => b.IdBuilding == campaign.ToIdBuilding)
                .SingleOrDefault();

            if(B1 == null || B2 == null || B2.Street != B1.Street)
            {
                return -1;
            }

            var cmp = new Campaign
            {
                IdCampaign = db.Campaign.Max(e => e.IdCampaign) + 1,
                IdClient = campaign.IdClient,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                PricePerSquareMeter = campaign.PricePerSquareMeter,
                FromIdBuilding = campaign.FromIdBuilding,
                ToIdBuilding = campaign.ToIdBuilding
            };

            db.Attach(cmp);
            db.Campaign.Add(cmp);
            db.SaveChanges();

            var list = db.Building.Where(e => e.Street == B1.Street &&( 
            (e.StreetNumber > B1.StreetNumber && e.StreetNumber < B2.StreetNumber) ||
            (e.StreetNumber < B1.StreetNumber && e.StreetNumber > B2.StreetNumber)
            )
            ).OrderBy(e => e.StreetNumber).ToList();

            return calculation.getPrice(list, campaign.PricePerSquareMeter);
        }
    }
}
