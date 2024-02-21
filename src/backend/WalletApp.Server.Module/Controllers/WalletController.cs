using Microsoft.AspNetCore.Mvc;
using WalletApp.Server.Core.Models;
using WalletApp.Server.Domain;

namespace WalletApp.Server.Module.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService walletService;

        public WalletController(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateWallet()
        {
            await walletService.Create(new Wallet()
            {
                Balance = 0,
                Name = Guid.NewGuid().ToString()
            });

            return Ok();
        }

        [HttpPost]
        [Route("addFounds")]
        public async Task<IActionResult> AddFounds(long walletId, decimal founds)
        {
            await walletService.AddFounds(walletId, founds);

            return Ok();
        }

        [HttpPost]
        [Route("removeFounds")]
        public async Task<IActionResult> RemoveFounds(long walletId, decimal founds)
        {
            await walletService.RemoveFounds(walletId, founds);

            return Ok();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetWallets()
        {
            var walletList = await walletService.GetWallets();

            return Ok(walletList);
        }
    }
}
