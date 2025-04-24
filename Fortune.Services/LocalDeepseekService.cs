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

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                "http://localhost:11434/api/generate",
                content
            );
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return ProcessResponse(responseContent);
        }

        private string ProcessResponse(string responseContent)
        {
            var content = JsonConvert.DeserializeObject<dynamic>(responseContent);

            var responseString = content.response.ToString();

            string noThinkString = responseString.Split("</think>")[1];

            return noThinkString;
        }
    }
}