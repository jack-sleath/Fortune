using System.Reflection.Metadata;

namespace Fortune.Models.SaveObject
{
    public class FortuneModel
    {
        public FortuneModel() {
            id = Guid.NewGuid();
        }
        public Guid id { get; set; }
        public string LongFortune { get; set; }
        public string ShortFortune { get; set; }
        public string ImageTopics { get; set; }
        public byte[] QrImage { get; set; }
        public byte[] Audio { get; set; }
        public byte[] FortuneImage { get; set; }
        public List<int> LuckyNumbers { get; set; }
        public bool FortuneUsed { get; set; } = false;
        public string FortuneType { get; set; } = "Default";

       
    }
}
