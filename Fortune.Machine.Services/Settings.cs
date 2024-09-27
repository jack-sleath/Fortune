using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Machine.Services
{
    public static class Settings
    {
        public static int LedPin => 18; // Pin for LED
        public static int ButtonPin => 17; // Pin for Input (e.g., a button)
    }
}
