using ElevenLabs;
using ElevenLabs.TextToSpeech;
using Fortune.Models.Configs;
using Fortune.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Services {
    public class ElevenlabsTtsService : ITtsService {

        private readonly TtsConfig _config;
        private ElevenLabsClient apiClient;

        public ElevenlabsTtsService(TtsConfig config) {
            _config = config;

            //Shouldnt new things up like this in the constructor, but I wont tell if you dont
            apiClient = new ElevenLabsClient(_config.ApiKey);
        }


        public async Task<byte[]> GetTTSBlob(string text) {
            var voices = await apiClient.VoicesEndpoint.GetAllVoicesAsync();
            var selectedVoice = voices.FirstOrDefault(v => v.Id == _config.TtsVoiceId) ?? voices.First();

            var request = new TextToSpeechRequest(selectedVoice, text);
            var voiceClip = await apiClient.TextToSpeechEndpoint.TextToSpeechAsync(request);

#if DEBUG
            string folderPath = "Output/Tts"; // Change this to your desired folder
            Directory.CreateDirectory(folderPath); // Ensure the directory exists

            string filePath = Path.Combine(folderPath, $"{voiceClip.Id}.mp3");

            await File.WriteAllBytesAsync(filePath, voiceClip.ClipData.ToArray());
#endif
            return voiceClip.ClipData.ToArray();
        }
    }
}
