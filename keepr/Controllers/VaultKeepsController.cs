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
    [Route ("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;

        public VaultKeepsController(VaultKeepsService vaultKeepsService)
        {
            _vaultKeepsService = vaultKeepsService;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeepViewModel>> Create([FromBody] VaultKeep newVaultKeep)
        {
            try 
            {
            Account user = await HttpContext.GetUserInfoAsync<Account>();
            VaultKeepViewModel keep = _vaultKeepsService.Create(newVaultKeep, user.Id);
            return Ok(keep);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
        //Deletes relationship
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try 
            {
              Account user = await HttpContext.GetUserInfoAsync<Account>();
              string message = _vaultKeepsService.Delete(id, user);
              return Ok(message);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
    }
}