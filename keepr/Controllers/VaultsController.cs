using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Services;
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
        [HttpGet]
        public ActionResult<List<Vault>> GatAll()
        {
            try 
            {
              List<Vault> vault = _vaultsService.GetAll();
              return Ok(vault);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }        

    }
}