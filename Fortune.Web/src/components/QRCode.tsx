import QR from 'react-qr-code';
import Styles from './QRCode.module.scss'
import config from '../config';

interface QRCodeProps {
  value: string | undefined;
}

const QRCode: React.FC<QRCodeProps> = ({ value }) => {
  const qrValue = value 
    ? `${config.baseUrl}/${value}`
    : `${config.baseUrl}/`;

    console.log(qrValue);
  return (
    <>
      <div className={Styles.QRContainer}>
        <div className={Styles.QRCode}>
          <QR value={qrValue} size={200} bgColor='transparent' level='Q'/>
          </div>
      </div> 
    </>
  );
};

export default QRCode;
