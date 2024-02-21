namespace WalletApp.Server.Domain
{
    public class WalletService : IWalletService
    {
        public decimal GetBalance()
        {
            throw new NotImplementedException();
        }
    }

    public interface IWalletService
    {
        decimal GetBalance();
    }
}
