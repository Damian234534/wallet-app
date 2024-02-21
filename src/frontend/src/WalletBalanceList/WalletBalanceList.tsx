import { useState, useEffect } from 'react';
import restApiClient from '../RestApiClient';
import WalletBalanceResponse from '../Models/WalletBalanceResponse';
import './WalletBalanceList.css';

const WalletBalanceList = () => {
  const [walletBalances, setWalletBalances] = useState<WalletBalanceResponse[]>();
  const [userId, setUserId] = useState('');
  const [amount, setAmount] = useState('');

  const [userIds, setUserIds] = useState<WalletBalanceId[]>();

  const [selectedUserId, setSelectedUserId] = useState('');

  useEffect(() => {
    fetchUserIds();
    fetchWalletBalances();
  }, []);

  const fetchUserIds = async () => {
    try {
      const response = await restApiClient.get<WalletBalanceId[]>('/wallet/balance');
      setUserIds(response.data);
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
      const response = await restApiClient.get<WalletBalanceResponse[]>('/wallet/balance');
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
          <li key={index}>User {index + 1}: {balance.name}</li>
        ))}
      </ul>
      <div className="operation-section">
        <h2>Perform Operations</h2>
        <div className="select-container">
        <select value={selectedUserId} onChange={(e) => setSelectedUserId(e.target.value)}>
          <option value="">Select User ID</option>
          {userIds?.map(userId => (
            <option key={userId.id} value={userId.id}>{userId.id}</option>
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



type WalletBalanceId = Pick<WalletBalanceResponse, "id">;