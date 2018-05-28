using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Xunit;

namespace GDPR_Test_Practice
{
    public class DataMasking
    {

        //the first way to mask data is through SQL. 


        // The second way to Mask Data is using Hex code.
        // this is a very simple hex mask.
        [Fact]
        public void maskSensativeDataHex()
        {
            string customerFirstName = "Bob";
            string[] customerArray = customerFirstName.Split();
            int hashString = customerArray.GetHashCode();

            string newCusomerFirstName = hashString.ToString();
            Assert.NotSame(customerFirstName, newCusomerFirstName);
        }
        // cannot convert back to a string as does not hold all the data.
        //This is only really used to put data into hashed tables.


        // this is simepl mask we split all words into a sub string of 1 charecter long, this could be more than
        //one chececter, but one would make it harder.
        // we take the length and times it by a value, when adding substrings we find the whole number.
        //this is done by dividing using the length builder.
        [Fact]
        public void maskSensativeDataWithA()
        {
            string customerFirstName = "Bob";
            List<string> firstNames = new List<string>();
            for (int i = 0; i < customerFirstName.Length; i++)
            {
                string cName = customerFirstName.Substring(i, 1);
                firstNames.Add(cName);
            }
            string maskedString = "";
            // set length of the mask using length times X.
            // we set the final i value to the opposite opposite type of number we are looking for.
            // i's final value is 29 this is an odd number so we get number 0,1,2 but never 3, so we stop length error.
            for (int i = -10; i < customerFirstName.Length * 10 - 1; i++)
            {
                float val = i / 10;
                // when we find the whole divisable value add to the string.
                if (i >= 0 && i % 10 == 0)
                {
                        int place = i/10;
                        maskedString += firstNames[place];   
                }
                else
                {
                    Random random = new Random();
                    int number = random.Next(0, 26);
                    char letter = (char)('a' + number);
                    maskedString += letter.ToString();
                }
            }
            string result = maskedString[10].ToString();
            Assert.Equal("B",result);
        }
    }
}
