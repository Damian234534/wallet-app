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
        [Route("addFunds")]
        public async Task<IActionResult> AddFunds(long walletId, decimal funds)
        {
            await walletService.AddFunds(walletId, funds);

            return Ok();
        }

        [HttpPost]
        [Route("removeFunds")]
        public async Task<IActionResult> RemoveFunds(long walletId, decimal funds)
        {
            await walletService.RemoveFunds(walletId, funds);

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
