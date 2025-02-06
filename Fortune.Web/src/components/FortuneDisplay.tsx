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

  // Convert base64-encoded strings to data URLs.
  const audioSrc = fortune.audio ? `data:audio/mp3;base64,${fortune.audio}` : null;
  const qrImageSrc = fortune.qrImage ? `data:image/png;base64,${fortune.qrImage}` : null;
  const fortuneImageSrc = fortune.fortuneImage ? `data:image/png;base64,${fortune.fortuneImage}` : null;

  return (
    <div className={Styles.FortuneContainer}>
      {/* Long Fortune */}
      <div className={Styles.LongFortune}>
        {fortune.longFortune || "No fortune available."}
      </div>

      {/* Fortune Image */}
      {fortuneImageSrc && (
        <div className={Styles.FortuneImageContainer}>
          <img className={Styles.FortuneImage} src={fortuneImageSrc} alt="Fortune" />
        </div>
      )}

      {/* Lucky Numbers */}
      <div className={Styles.LuckyNumbers}>
        {fortune.luckyNumbers && fortune.luckyNumbers.length > 0
          ? fortune.luckyNumbers.join(", ")
          : "No lucky numbers."}
      </div>

      {/* QR Code */}
      {qrImageSrc && (
        <div className={Styles.QrCode}>
          <img className={Styles.FortuneImage} src={qrImageSrc} alt="QR Code" />
        </div>
      )}

      {/* Audio Play */}
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
