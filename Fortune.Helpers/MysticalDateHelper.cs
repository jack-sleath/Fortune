using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Helpers
{
    public static class MysticalDateHelper
    {
        public static string MysticalDate(this DateTime dateTime)
        {
            return $"{dateTime.GetWesternZodiac()} - {dateTime.GetChineseZodiac()}";
        }


        // Western Zodiac Signs
        private static readonly (int StartMonth, int StartDay, int EndMonth, int EndDay, string Sign)[] ZodiacSigns = new[]
        {
            (1, 20, 2, 18, "Aquarius"),
            (2, 19, 3, 20, "Pisces"),
            (3, 21, 4, 19, "Aries"),
            (4, 20, 5, 20, "Taurus"),
            (5, 21, 6, 20, "Gemini"),
            (6, 21, 7, 22, "Cancer"),
            (7, 23, 8, 22, "Leo"),
            (8, 23, 9, 22, "Virgo"),
            (9, 23, 10, 22, "Libra"),
            (10, 23, 11, 21, "Scorpio"),
            (11, 22, 12, 21, "Sagittarius"),
            (12, 22, 1, 19, "Capricorn")
        };

        // Chinese Zodiac Signs (12-year cycle)
        private static readonly string[] ChineseZodiac = new[]
        {
            "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox",
            "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat"
        };

        public static string GetWesternZodiac(this DateTime dateTime)
        {
            foreach (var (startMonth, startDay, endMonth, endDay, sign) in ZodiacSigns)
            {
                if ((dateTime.Month == startMonth && dateTime.Day >= startDay) ||
                    (dateTime.Month == endMonth && dateTime.Day <= endDay))
                {
                    return sign;
                }
            }
            return "Unknown";
        }

        public static string GetChineseZodiac(this DateTime dateTime)
        {
            return $"Year of the {ChineseZodiac[dateTime.Year % 12]}";
        }
    }
}
