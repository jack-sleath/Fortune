using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Shared.Services.Interfaces
{
    public interface IQrService
    {
        Task<byte[]> GetQRCodeBlobForGuid(Guid guid);
    }
}
