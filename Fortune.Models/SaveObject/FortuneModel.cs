using System.Reflection.Metadata;

namespace Fortune.Models.SaveObject
{
    public class FortuneModel
    {
        public Guid id { get; set; }
        public string LongFortune { get; set; }
        public string ShortFortune { get; set; }
        public byte[] QrImage { get; set; }
        public byte[] Audio { get; set; }
        public byte[] FortuneImage { get; set; }
        public string LuckyNumbers { get; set; }
        public string FortuneType { get; set; } = "Default";
    }
}
