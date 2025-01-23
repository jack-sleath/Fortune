import { useState } from 'react';
import './App.css';
import FortuneDisplay from './components/FortuneDisplay';
import fortunes from './data/test_responses.json'; // Import the JSON data
import {getRandomFortune} from './api/ApiHandler';

const App: React.FC = async () => {
  const [count, setCount] = useState(0);

  // Pick a random fortune from the array
  // const randomFortune = fortunes[Math.floor(Math.random() * fortunes.length)];
  const fetchedFortune = await getRandomFortune();
  

  return (
    <>
      <div>
        {/* Pass the random fortune to the FortuneDisplay component */}
        <FortuneDisplay fortune={fetchedFortune} />
      </div>
    </>
  );
}

export default App;
