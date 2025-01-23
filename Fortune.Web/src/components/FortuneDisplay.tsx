import React, { useState, useEffect, useRef } from 'react';
import Styles from './FortuneDisplay.module.scss';
import {FortuneModel} from './../api/FortuneApiClient';
import { getRandomFortune } from '../api/ApiHandler';





const FortuneDisplay: React.FC = () => {


const[fortune, setFortune] = useState<FortuneModel | null>(null);

useEffect(() => {
  const fetchData = async () =>{
    try{
      const data = await getRandomFortune();
      if(data){
        setFortune(data);
      }
    }catch(error){
      console.error("error assigning fortune: ", error);
    }
    
    fetchData();
  }
}, [])

    const speak = (text: string | undefined) => {
        if ('speechSynthesis' in window) {
          const utterance = new SpeechSynthesisUtterance(text);
          utterance.lang = 'en-GB';
          utterance.rate = 1.0; // Speed
          utterance.pitch = 1.0; // Pitch
          speechSynthesis.speak(utterance);
        } else {
          console.error('Speech synthesis not supported in this browser.');
        }
      };
      
  return (
    <div className={Styles.FortuneContainer}>
      {/* Display the main fortune title */}
      <h1 className={Styles.FortuneHeading}>Your Fortune</h1>

      {/* Display fortune image if available */}
      {fortune?.fortuneImage && (
        <section>
          <img className={Styles.FortuneImage}
            src={`data:image/png;base64,${fortune?.fortuneImage}`}
            alt="Fortune related image"
            style={{ maxWidth: '100%', height: 'auto' }}
          />
        </section>
      )}

      {/* Display long fortune text */}
      <section>
        <p>{fortune?.longFortune}</p>
      </section>

      {/* Display play again text */}
      <section>
        <h2 className={Styles.PlayAgainSubHeading}>Play Again!</h2>
      </section>

      {/* Display QR code image if available */}
      {fortune?.qrImage && (
        <section>
          <h3>QR Code</h3>
          <img
            src={`data:image/png;base64,${fortune?.qrImage}`}
            alt="QR Code for more information"
            style={{ maxWidth: '150px', height: 'auto' }}
          />
        </section>
      )}

      {/* Display audio if available */}
      {fortune?.audio && (
        <section>
          <h3>Audio Guidance</h3>
          <audio controls>
            <source src={`data:audio/mpeg;base64,${fortune.audio}`} type="audio/mpeg" />
            Your browser does not support the audio element.
          </audio>
        </section>
      )}

      {/* Display lucky numbers */}
      <section>
        <h3>Lucky Numbers</h3>
        <p>{fortune?.luckyNumbers?.join(', ')}</p>
      </section>

      <button onClick={() => speak(fortune?.shortFortune)}>Speak</button>
    </div>
  );
};

export default FortuneDisplay;
