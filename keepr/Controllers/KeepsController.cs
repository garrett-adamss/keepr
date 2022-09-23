using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;

        public KeepsController(KeepsService keepsService)
        {
            _keepsService = keepsService;
        }

        // Functions Start Here

        [HttpGet]
        public async Task<ActionResult<List<Keep>>> GetAll()
        {
            try 
            {
              Account user = await HttpContext.GetUserInfoAsync<Account>();
              List<Keep> keeps = _keepsService.GetAll(user?.Id);
              return Ok(keeps);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }



    }
}