import { useState } from 'react';
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
    </>
  );
}

export default App;
