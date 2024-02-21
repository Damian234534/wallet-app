import { useState, useEffect } from 'react';
import restApiClient from '../RestApiClient';
import WalletResponse from '../Models/WalletBalanceResponse';
import './WalletBalanceList.css';

type WalletId = Pick<WalletResponse, "id">;

const WalletBalanceList = () => {
  const [walletBalances, setWalletBalances] = useState<WalletResponse[]>();
  const [userId, setUserId] = useState('');
  const [amount, setAmount] = useState('');

  const [walletIds, setWalletIds] = useState<number[]>();

  const [selectedWalletId, setSelectedWalletId] = useState('');

  useEffect(() => {
    fetchUserIds();
    fetchWalletBalances();
  }, []);

  const fetchUserIds = async () => {
    try {
      const response = await restApiClient.get<WalletResponse[]>('/wallet/all');
      setWalletIds(response.data.map(x=> x.id));
    } catch (error) {
      console.error('Error fetching user IDs:', error);
    }
  };

  const handleDeposit = async () => {
    try {
      await restApiClient.post('/api/wallet/deposit', { userId, amount });
      fetchWalletBalances();
      setUserId('');
      setAmount('');
    } catch (error) {
      console.error('Error depositing funds:', error);
    }
  };

  const fetchWalletBalances = async () => {
    try {
      const response = await restApiClient.get<WalletResponse[]>('/wallet/all');
      setWalletBalances(response.data);
    } catch (error) {
      console.error('Error fetching wallet balances:', error);
    }
  };

  const handleWithdraw = async () => {
    try {
      await restApiClient.post('/api/wallet/withdraw', { userId, amount });
      fetchWalletBalances();
      setUserId('');
      setAmount('');
    } catch (error) {
      console.error('Error withdrawing funds:', error);
    }
  };

  return (
    <div>
      <h1>Wallet Balances</h1>
      <ul>
        {walletBalances?.map((balance, index) => (
          <li key={index}>Account {index + 1}: {balance.name}</li>
        ))}
      </ul>
      <div className="operation-section">
        <h2>Perform Operations</h2>
        <div className="select-container">
        <select value={selectedWalletId} onChange={(e) => setSelectedWalletId(e.target.value)}>
          <option value="">Select account ID</option>
          {walletIds?.map(accountId => (
            <option key={accountId} value={accountId}>{accountId}</option>
          ))}
        </select>
        </div>
        <input
          type="number"
          placeholder="Amount"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
        />
        <button className="deposit-btn" onClick={handleDeposit}>Deposit</button>
        <button className="withdraw-btn" onClick={handleWithdraw}>Withdraw</button>
      </div>
    
    </div>
    
  );
};

export default WalletBalanceList;
