using Fortune.Services.Interfaces;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;

namespace Fortune.Services
{
    public class QrService : IQrService
    {
        private readonly string _webUrl;

        public QrService(string webUrl)
        {
            _webUrl = webUrl;
        }
        public async Task<byte[]> GetQRCodeBlobForGuid(Guid id)
        {
            var content = $"{_webUrl}/{id.ToString()}";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);

            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

            byte[] qrCodeImage = qrCode.GetGraphic(20);  

            return qrCodeImage;
        }
    }
}
