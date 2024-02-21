using WalletApp.Server.Core.Models;

namespace WalletApp.Server.Domain
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository repository;

        public WalletService(IWalletRepository repository)
        {
            this.repository = repository;
        }

        public async Task Create(Wallet wallet)
        {
            await repository.Create(wallet);
        }

        public async Task<decimal> GetBalance(long accountId)
        {
            return await repository.GetBalance(accountId);
        }

        public async Task<IEnumerable<Wallet>> GetWallets()
        {
            return await repository.GetWallets();
        }
    }

    public interface IWalletService
    {
        Task Create(Wallet wallet);
        Task<decimal> GetBalance(long accountId);
        Task<IEnumerable<Wallet>> GetWallets();
    }
}
