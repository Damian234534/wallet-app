using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using WalletApp.Server.Core.Models;

namespace WalletApp.Server.Module.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : ControllerBase
    {
        private static List<Wallet> wallets = new List<Wallet>();

        [HttpPost]
        [Route("create")]
        public IActionResult CreateWallet()
        {

            wallets.Add(new Wallet(){
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString()
            });
            return Ok();
        }

        [HttpGet]
        [Route("balance")]
        public IActionResult GetBalance()
        {
            return Ok(wallets);
        }
    }
}
