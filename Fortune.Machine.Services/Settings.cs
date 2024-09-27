using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Machine.Services
{
    public static class Settings
    {
        public static int LedPin => 18;
        public static int ButtonPin => 17;

        public static int LedFlashCount => 5;
        public static int LedFlashIntervalMilliseconds => 500;
    }
}
