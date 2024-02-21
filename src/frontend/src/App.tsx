import logo from './logo.svg';
import './App.css';
import WalletBalanceList from './WalletBalanceList/WalletBalanceList';

const App = () => {
  return (
    <div className="App">
      <h1>My Dashboard</h1>
      <header className="App-header">
        <WalletBalanceList /> {/* Render the WalletBalanceList component */}
      </header>
    </div>
  );
}

export default App;


