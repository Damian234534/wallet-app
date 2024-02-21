using Microsoft.EntityFrameworkCore;
using WalletApp.Server.Core.Models;
using WalletApp.Server.Infrastructure;

namespace WalletApp.Server.Domain
{
    public class WalletReposiotry : IWalletRepository
    {
        private readonly WalletContext context;

        public WalletReposiotry(WalletContext context)
        {
            this.context = context;
        }

        public async Task Create(Wallet wallet)
        {
            await context.AddAsync(wallet);
            await context.SaveChangesAsync();
        }

        public async Task<decimal> GetBalance(long accountId)
        {
            return await context.Wallets.Where(x=>x.Id == accountId).Select(x=>x.Balance).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Wallet>> GetWallets()
        {
            return await context.Wallets.ToListAsync();         
        }
    }

    public interface IWalletRepository
    {
        Task Create(Wallet wallet);
        Task<decimal> GetBalance(long accountId);
        Task<IEnumerable<Wallet>> GetWallets();
    }
}
