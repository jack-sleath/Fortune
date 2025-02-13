// src/components/FortuneDisplay.tsx
import React, { useEffect, useState } from 'react';
import { ApiHandler } from '../api/ApiHandler';
import { FortuneModel } from '../api/FortuneApiClient';
import Styles from './FortuneDisplay.module.scss';

const FortuneDisplay: React.FC = () => {
  const [fortune, setFortune] = useState<FortuneModel | null>(null);
  const [error, setError] = useState<string | null>(null);

  const fetchFortune = async () => {
    try {
      const result = await ApiHandler.getRandomFortune();
      setFortune(result);
    } catch (err) {
      setError("Failed to fetch fortune.");
      console.error(err);
    }
  };

  useEffect(() => {
    fetchFortune();
  }, []);

  if (error) {
    return <div style={{ color: 'red' }}>Error: {error}</div>;
  }

  if (!fortune) {
    return <div>Loading fortune...</div>;
  }

  const audioSrc = fortune.audio ? `data:audio/mp3;base64,${fortune.audio}` : null;
  const qrImageSrc = fortune.qrImage ? `data:image/png;base64,${fortune.qrImage}` : null;
  const fortuneImageSrc = fortune.fortuneImage ? `data:image/png;base64,${fortune.fortuneImage}` : null;

  return (
    <div className={Styles.FortuneContainer}>
      <h1>Your Fortune</h1>

      {fortuneImageSrc && (
        <div className={Styles.FortuneImageContainer}>
          <img className={Styles.FortuneImage} src={fortuneImageSrc} alt="Fortune" />
        </div>
      )}

      <div className={Styles.LongFortune}>
        {fortune.longFortune || "No fortune available."}
      </div>

      <div className={Styles.PlayAgain}>
        Play Again!
      </div>

      {fortune.luckyNumbers && fortune.luckyNumbers.length > 0 && (
        <div className={Styles.LuckyNumbers}>
          Your Lucky Numbers: {fortune.luckyNumbers.join(", ")}
        </div>
      )}
   
      {qrImageSrc && (
        <div className={Styles.QrCode}>
          <img className={Styles.FortuneImage} src={qrImageSrc} alt="QR Code" />
        </div>
      )}

      {audioSrc && (
        <div className={Styles.AudioPlay}>
          <audio controls src={audioSrc}>
            Your browser does not support the audio element.
          </audio>
        </div>
      )}
    </div>
  );
};

export default FortuneDisplay;
