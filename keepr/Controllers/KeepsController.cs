using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
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
        public ActionResult<List<Keep>> GetAll()
        {
            try
            {
                List<Keep> keeps = _keepsService.GetAll();
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Keep>> GetOne(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                Keep keep = _keepsService.GetOne(id, user?.Id);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
        {
            try 
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                keepData.CreatorId = user.Id;
                Keep keep = _keepsService.Create(keepData, user);
                keep.Creator = user;
                return Ok(keep);
              
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> Update(int id, [FromBody] Keep keepData)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                keepData.Id = id;
                Keep keep = _keepsService.Update(keepData, user);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try 
            {
              Account user = await HttpContext.GetUserInfoAsync<Account>();
              string message = _keepsService.Delete(id, user);
              return Ok(message);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }

        // [HttpGet("{id}/vaults")]
    }
}