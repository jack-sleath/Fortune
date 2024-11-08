import { useState } from 'react';
import reactLogo from './assets/react.svg';
import viteLogo from '/vite.svg';
import './App.css';
import FortuneDisplay from './components/FortuneDisplay';
import fortunes from './data/test_responses.json'; // Import the JSON data

function App() {
  const [count, setCount] = useState(0);

  // Pick a random fortune from the array
  const randomFortune = fortunes[Math.floor(Math.random() * fortunes.length)];

  return (
    <>
      <div>
        {/* Pass the random fortune to the FortuneDisplay component */}
        <FortuneDisplay fortune={randomFortune} />
      </div>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </>
  );
}

export default App;
