using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Services.Interfaces
{
    public interface IExternalTextAiService
    {
        Task<string> GenerateTextResponseAsync(string prompt);
    }
}
