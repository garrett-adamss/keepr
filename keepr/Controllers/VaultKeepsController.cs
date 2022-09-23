using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;

        public VaultKeepsController(VaultKeepsService vaultKeepsService)
        {
            _vaultKeepsService = vaultKeepsService;
        }
    }
}