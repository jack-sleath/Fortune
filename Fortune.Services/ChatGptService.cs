using Fortune.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Fortune.Helpers;
using Fortune.Models.Enums;

namespace Fortune.Services
{
    public class ChatGptService : IExternalTextAiService, IExternalImageAiService
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
            string imageUrl = "https://images.creativefabrica.com/products/previews/2023/10/28/ueUbh74zq/2XN9DphZmDsODrGTFcNNPPFfpqX-mobile.jpg";
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

            return imageBytes.Resize(128, 128).ConvertToBlackAndTransparency().CropToAspectRatio(EAspectRatio.SixteenByNine);  // Return the byte array representing the image
        }
    }
}