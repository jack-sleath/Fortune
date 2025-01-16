using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Models.Configs {
    public class TtsConfig {
        public string TtsProvider { get; set; }
        public string TtsVoiceId { get; set; }

        public string ApiKey { get; set; }
    }
}
