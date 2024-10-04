using Fortune.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Fortune.Services
{
    public class ChatGptService : IExternalAiService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public ChatGptService(HttpClient httpClient, string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> GenerateTextResponseAsync(string prompt)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",  // Specify the model you want to use
                messages = new[]
                {
                new { role = "user", content = prompt }
            }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            dynamic jsonResponse = JsonConvert.DeserializeObject(responseString);
            return jsonResponse.choices[0].message.content;
        }

        public async Task<byte[]> GenerateImageAsync(string prompt)
        {
#if !DEBUG
            string imageUrl = "https://oaidalleapiprodscus.blob.core.windows.net/private/org-BWIi40SUWvplZqj5EvLvY0xb/fortune-bot/img-pp7qeezZx1MxpZXhUh8zlqwz.png?st=2024-09-13T17%3A34%3A01Z&se=2024-09-13T19%3A34%3A01Z&sp=r&sv=2024-08-04&sr=b&rscd=inline&rsct=image/png&skoid=d505667d-d6c1-4a0a-bac7-5c84a87759f8&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2024-09-13T18%3A33%3A03Z&ske=2024-09-14T18%3A33%3A03Z&sks=b&skv=2024-08-04&sig=vyf4agfiyKt6X/VoZenr/KuwI9%2BWi%2ByqoB4hObJqOxc%3D";
#else
            var requestBody = new
            {
                prompt = prompt,
                n = 1,  // Number of images to generate
                size = "1024x1024"  // Image resolution
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/images/generations", content);
            var responseString = await response.Content.ReadAsStringAsync();

            dynamic jsonResponse = JsonConvert.DeserializeObject(responseString);
            string imageUrl = jsonResponse.data[0].url;
#endif
            Console.WriteLine(imageUrl);

            // Download the image as a byte array
            var imageBytes = await new HttpClient().GetByteArrayAsync(imageUrl);

            return imageBytes;  // Return the byte array representing the image
        }
    }
}