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
    public class ProfilesController : ControllerBase
    {
        private readonly AccountService _accountService;

        public ProfilesController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> GetOne(string id)
        {
            try
            {
                Profile profile = _accountService.GetOne(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public ActionResult<Keep> GetKeeps(string id)
        {
            try 
            {
                List<Keep> keeps = _accountService.GetKeeps(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<Vault>> GetVaultsAsync(string id)
        {
            try 
            {
              Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
              List<Vault> vaults = _accountService.GetVaults(id, userInfo);
              return Ok(vaults);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
    }
}