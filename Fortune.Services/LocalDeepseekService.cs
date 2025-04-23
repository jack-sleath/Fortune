using Fortune.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Fortune.Shared.Helpers;
using Fortune.Models.Enums;
using Fortune.Shared.Models.Enums;

namespace Fortune.Services
{
    public class LocalDeepseekService : IExternalTextAiService
    {
        private readonly HttpClient _httpClient;

        public LocalDeepseekService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromMinutes(10);
        }

        public async Task<string> GenerateTextResponseAsync(string prompt)
        {
            var requestBody = new
            {
                model = "deepseek-r1:7b",
                prompt = prompt,
                stream = false
            };

            // 2) Serialize & wrap in StringContent
            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // 3) Post to your local endpoint
            var response = await _httpClient.PostAsync(
                "http://localhost:11434/api/generate",
                content
            );
            response.EnsureSuccessStatusCode();

            // 4) Read the entire response as a string
            //    (if your API truly streams SSE, see the streaming example below)
            return await response.Content.ReadAsStringAsync();
        }
    }
}