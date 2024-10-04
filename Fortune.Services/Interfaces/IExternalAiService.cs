using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Services.Interfaces
{
    public interface IExternalAiService
    {
        Task<string> GenerateTextResponseAsync(string prompt);
        Task<byte[]> GenerateImageAsync(string prompt);
    }
}
