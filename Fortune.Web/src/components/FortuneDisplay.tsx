import React from 'react';

// Define the Fortune interface
interface Fortune {
  id: string;
  longFortune: string;
  shortFortune: string;
  imageTopics: string;
  qrImage: string; // Assuming it's a base64 encoded string
  audio: string | null;
  fortuneImage: string; // Assuming it's a base64 encoded string
  luckyNumbers: number[];
  fortuneType: string;
}

// Define the component's props interface
interface FortuneProps {
  fortune: Fortune;
}

const FortuneDisplay: React.FC<FortuneProps> = ({ fortune }) => {

    const speak = (text) => {
        if ('speechSynthesis' in window) {
          const utterance = new SpeechSynthesisUtterance(text);
          utterance.lang = 'en-US';
          utterance.rate = 1.0; // Speed
          utterance.pitch = 1.0; // Pitch
          speechSynthesis.speak(utterance);
        } else {
          console.error('Speech synthesis not supported in this browser.');
        }
      };
      
      // Example: Add to a button in your React component
      

  return (
    <div style={{ padding: '20px', maxWidth: '600px', border: '1px solid #ccc', borderRadius: '8px' }}>
      {/* Display the main fortune title */}
      <h2>Professor Fortuna's Fortune Reading</h2>

      {/* Display long fortune text */}
      <section>
        <p>{fortune.longFortune}</p>
      </section>

      {/* Display fortune image if available */}
      {fortune.fortuneImage && (
        <section>
          <img
            src={`data:image/png;base64,${fortune.fortuneImage}`}
            alt="Fortune related image"
            style={{ maxWidth: '100%', height: 'auto' }}
          />
        </section>
      )}

      {/* Display QR code image if available */}
      {fortune.qrImage && (
        <section>
          <h3>QR Code</h3>
          <img
            src={`data:image/png;base64,${fortune.qrImage}`}
            alt="QR Code for more information"
            style={{ maxWidth: '150px', height: 'auto' }}
          />
        </section>
      )}

      {/* Display audio if available */}
      {fortune.audio && (
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
        <p>{fortune.luckyNumbers.join(', ')}</p>
      </section>

      <button onClick={() => speak(fortune.shortFortune)}>Speak</button>
    </div>
  );
};

export default FortuneDisplay;
