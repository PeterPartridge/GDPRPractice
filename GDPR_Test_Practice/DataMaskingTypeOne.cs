using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Xunit;

namespace GDPR_Test_Practice
{
    public class DataMaskingTypeOne
    {

        /// <summary>
        /// make a single string visable
        /// This would be for string feilds holding a first or last name in singleton fashion.
        /// requires a {masking} variable.
        /// I think this is a style of blurring or encryption
        /// </summary>

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
        public void maskSensativeDataWithLetters()
        {
            string customerFirstName = "Bob";
            List<string> firstNames = new List<string>();
            for (int i = 0; i < customerFirstName.Length; i++)
            {
                string cName = customerFirstName.Substring(i, 1).ToLower();
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
                    int place = i / 10;
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
            Assert.Equal("b", result);
        }

        // if we have the divisonal number we can now unpick the masked data to reveal the orginal string.
        // the problem here is the database must have the divider value, or the entire project can only use a set divider/dividsers.
        // we could use more than one divider option but it still need to be a code standard, or a database column identief in some way.
        [Fact]
        public void makeMaskDataVisable()
        {
            // string created randomly in the maskSensativeDataWithLetters test 
            string customerFirstName = "cszaokcodabofdfomyxloaefucasazbyhgtqcqe";
            // divider value used in this test
            int divider = 10;
            // the result of the test.
            string result = "";
            //loop through the using the divider value to find the values.
            for (int i = divider; i < customerFirstName.Length; i += divider)
            {
                // if i is the same as the divider this is the start of the name
                if (i == divider)
                {
                    result += customerFirstName[i].ToString().ToUpper();
                }
                // this is the rest of the letters.
                else
                {
                    result += customerFirstName[i].ToString().ToLower();
                }
            }
            Assert.Equal("Bob", result);
        }

        [Fact]
        public void maskSensativeDataWithNumbers()
        {
           long customerCreditCard = 4568956321124;
            string creditCard = customerCreditCard.ToString();
            List<string> cardNumbers = new List<string>();
            for (int i = 0; i < creditCard.Length; i++)
            {
                string cName = creditCard.Substring(i, 1).ToLower();
                cardNumbers.Add(cName);
            }
            string maskedString = "";
            int divider = 20;
            for (int i = -divider; i < creditCard.Length * divider - 1; i++)
            {
                float val = i / divider;
                if (i >= 0 && i % divider == 0)
                {
                    int place = i / divider;
                    maskedString += cardNumbers[place];
                }
                else
                {
                    Random random = new Random();
                    // can only use single digit values at the moment as the mask.
                    int number = random.Next(0, 9);                 
                    maskedString += number;
                }
            }
            string result = maskedString[divider].ToString();
            Assert.Equal("4", result);
        }

    }
}
