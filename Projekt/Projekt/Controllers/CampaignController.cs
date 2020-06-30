using AdvertApi.DTOs.Requests;
using AdvertApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Controllers
{
    [ApiController]
    [Route("api/campaigns")]
    [Authorize]
    public class CampaignController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllCampaigns([FromServices] IClientDal _dbService)
        {
            return Ok(_dbService.GetAllInformation());
        }

        [HttpPost]
        public IActionResult AddNewCampaign([FromServices] IClientDal _dbService, NewCampaignRequest request)
        {
            var result = _dbService.AddCampaign(request);
            if (result == -1)
                return StatusCode(404, "Wrong buildings");
            else
                return Ok(result);
        }
    }
}
