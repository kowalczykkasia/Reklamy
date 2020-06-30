using AdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Services
{
    public class CalculationService
    {
        public double getPrice(List<Building> buildings, double squarePrice)
        {
            var p1 = buildings[0].Height;
            var p2 = buildings[buildings.Count() - 1].Height * (buildings.Count() - 1);
            return (p1 + p2) * squarePrice;
        }
    }
}
