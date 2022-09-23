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

        // Functions start here vv
    }
}