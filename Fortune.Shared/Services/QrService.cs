using QRCoder;
using Fortune.Shared.Helpers;
using Fortune.Shared.Services.Interfaces;

namespace Fortune.Shared.Services
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

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.H);

            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

            byte[] qrCodeImage = qrCode.GetGraphic(20);

            return qrCodeImage.ConvertToBlackAndTransparency();
        }
    }
}
