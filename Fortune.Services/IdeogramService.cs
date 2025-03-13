using Fortune.Shared.Helpers;
using Fortune.Models.Enums;
using Fortune.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator.ShadowSocksConfig;
using Fortune.Shared.Models.Enums;

namespace Fortune.Services
{
    public class IdeogramService : IExternalImageAiService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly string _aspectRatio;
        private readonly int _height;
        private readonly int _width;

        public IdeogramService(HttpClient httpClient, string apiKey, string aspectRatio = "ASPECT_16_9", int width = 320, int height = 180)
        {
            _apiKey = apiKey;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Api-Key", _apiKey);
            _aspectRatio = aspectRatio;
            _width = width;
            _height = height;
        }


        public async Task<byte[]> GenerateImageAsync(string prompt)
        {
#if !DEBUG
            string imageUrl = "https://images.creativefabrica.com/products/previews/2023/10/28/ueUbh74zq/2XN9DphZmDsODrGTFcNNPPFfpqX-mobile.jpg";
#else
            var requestBody = new
            {
                image_request = new
                {
                    prompt = prompt,
                    aspect_ratio = _aspectRatio,
                    model = "V_1_TURBO",
                    magic_prompt_option = "AUTO"
                }
            };


            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.ideogram.ai/generate", content);
            var responseString = await response.Content.ReadAsStringAsync();

            dynamic jsonResponse = JsonConvert.DeserializeObject(responseString);
            string imageUrl = jsonResponse.data[0].url;

            Console.WriteLine(imageUrl);
#endif

            // Download the image as a byte array
            var imageBytes = await new HttpClient().GetByteArrayAsync(imageUrl);

            return imageBytes.Resize(_width, _height).ConvertToBlackAndTransparency();  // Return the byte array representing the image
        }
    }
}
