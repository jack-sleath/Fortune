// src/App.tsx
import React from 'react';
import FortuneDisplay from './components/FortuneDisplay';
import LayoutStyles from './App.module.scss';

const App: React.FC = () => {
  return (
    <div className={LayoutStyles.PageContainer}>
      <main className={LayoutStyles.Content}>
        <FortuneDisplay />
      </main>
    </div>
  );
};

export default App;
