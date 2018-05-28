using System;
using System.Collections.Generic;
using Xunit;

namespace GDPR_Test_Practice
{
    public class ScrambleDatacs
    {
        /// <summary>
        /// This is called scrambeling data.
        /// This is taking a string and turning it into a new word.
        /// Issue is data recovery, this will not be able to be recovered once scrambled easily.
        /// </summary>
        /// 

        // scrambleing data like known cities e.g: London, a person can still guess london.
        [Fact]
        public void ScrambleLocation()
        {
            string userLocation = "London";
            List<string> locaiton = new List<string>();
            for (int i = 0; i < userLocation.Length; i++)
            {
                locaiton.Add(userLocation[i].ToString());
            }
            List<int> usedValues = new List<int>();
            string result = "";
            for (int i = 0; i < locaiton.Count; i++)
            {
                Random _random = new Random();
                int letter = _random.Next(0, locaiton.Count);
                while (usedValues.Contains(letter))
                {
                    letter = _random.Next(0, locaiton.Count);
                }
                usedValues.Add(letter);
                result += locaiton[letter].ToLower();
            }
            Assert.NotEqual(userLocation, result);

        }

        // for longer place names when the white space is removed and letters reoganised it can be hard to put back together.
        // you could if constructing mutiple lines of an address decord all words and maybe you cna make a full address.
        [Fact]
        public void ScrambleLocationWithSPaces()
        {
            string userLocation = "heath and Reach";
            List<string> locaiton = new List<string>();
            for (int i = 0; i < userLocation.Length; i++)
            {
                // remove the spaces int he place name.
                if(!string.IsNullOrWhiteSpace(userLocation[i].ToString()))
                locaiton.Add(userLocation[i].ToString());
            }
            List<int> usedValues = new List<int>();
            string result = "";
            for (int i = 0; i < locaiton.Count; i++)
            {
                Random _random = new Random();
                int letter = _random.Next(0, locaiton.Count);
                while (usedValues.Contains(letter))
                {
                    letter = _random.Next(0, locaiton.Count);
                }
                usedValues.Add(letter);
                result += locaiton[letter].ToLower();
            }
            Assert.NotEqual(userLocation, result);

        }
    }
}
