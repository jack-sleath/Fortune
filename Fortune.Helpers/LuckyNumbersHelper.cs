using Fortune.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Helpers
{
    public static class LuckyNumbersHelper
    {

        public static List<int> GetLuckyNumbers(this LuckyNumberConfig luckyNumberConfig) 
        {
            Random random = new Random();

            HashSet<int> randomNumbers = new HashSet<int>();


            while (randomNumbers.Count < luckyNumberConfig.Count)
            {
                int number = random.Next(luckyNumberConfig.Min, luckyNumberConfig.Max + 1);
                randomNumbers.Add(number);
            }
     
            List<int> uniqueNumbers = randomNumbers.ToList();
            uniqueNumbers.Sort();

            return uniqueNumbers;
        }
    }
}
