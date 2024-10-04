using Fortune.Services.Interfaces;

namespace Fortune.Services
{
    public class QrService : IQrService
    {
        private readonly string _webUrl;

        public QrService(string webUrl)
        {
            _webUrl = webUrl;
        }
        public string GetQRCodeBlobForGuid(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
