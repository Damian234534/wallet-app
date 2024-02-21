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

        public async Task<Wallet?> Get(long walletId)
        {
            return await repository.Get(walletId);
        }

        public async Task AddFounds(long walletId, decimal founds)
        {
            var wallet = await repository.Get(walletId);

            if(wallet is null)
            {
                throw new ArgumentNullException();
            }

            wallet.Balance += founds;

            await repository.Update(wallet);
        }

        public async Task RemoveFounds(long walletId, decimal founds)
        {
            var wallet = await repository.Get(walletId);

            if (wallet is null)
            {
                throw new ArgumentNullException();
            }

            wallet.Balance -= founds;

            await repository.Update(wallet);
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
        Task AddFounds(long walletId, decimal founds);
        Task Create(Wallet wallet);
        Task<Wallet?> Get(long walletId);
        Task<decimal> GetBalance(long accountId);
        Task<IEnumerable<Wallet>> GetWallets();
        Task RemoveFounds(long walletId, decimal founds);
    }
}
