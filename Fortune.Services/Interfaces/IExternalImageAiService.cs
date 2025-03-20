using Fortune.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Services.Interfaces
{
    public interface IExternalImageAiService
    {
        Task<byte[]> GenerateImageAsync(string prompt, EAspectRatio eAspectRatio, int height);
    }
}
