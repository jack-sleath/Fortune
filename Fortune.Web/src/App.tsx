// src/App.tsx
import React from 'react';
import FortuneDisplay from './components/FortuneDisplay';

const App: React.FC = () => {
  return (
    <div>
      <h1>Random Fortune</h1>
      <FortuneDisplay />
    </div>
  );
};

export default App;
