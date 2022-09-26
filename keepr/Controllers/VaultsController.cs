using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;

        public VaultsController(VaultsService vaultsService)
        {
            _vaultsService = vaultsService;
        }

        // Functions start here
        // [HttpGet]
        // public ActionResult<List<Vault>> GatAll()
        // {
        //     try 
        //     {
        //       List<Vault> vault = _vaultsService.GetAll();
        //       return Ok(vault);
        //     }
        //     catch (Exception e)
        //     {
        //        return BadRequest(e.Message);
        //     }
        // }        

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
        {
            try 
            {
              Account user = await HttpContext.GetUserInfoAsync<Account>();
              newVault.CreatorId = user.Id;
              Vault vault = _vaultsService.Create(newVault, user);
              vault.Creator = user;
              return Ok(vault);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
    }
}