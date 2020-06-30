using AdvertApi.DTOs;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateClient([FromServices] IClientDal _dbService, CreateClientRequest client)
        {
            var response = _dbService.CreateClient(client);
            if (response != null)
            {
                return StatusCode(201, response);
            }
            return StatusCode(400, "error");
        }

    }
}
