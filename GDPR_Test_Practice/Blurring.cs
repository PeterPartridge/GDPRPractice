using System;
using System.Collections.Generic;
using Xunit;

namespace GDPR_Test_Practice
{
    public class Blurring
    {
        /// <summary>
        /// Blurring is taking data and making it into an aproxomation.
        /// It wil stop data being identfied to a person, but remains usable.
        /// </summary>
        [Fact]
        public void PayRiseBluringusingPercentage()
        {
            // the pay rise employees were given 3 years ago.
            //HR have agreed this pay rise should be blurred as it past 2 fincial years 
            //and HR have agreed a variance up or down of 2%.
            double payrise = 2.2;
            var maxPercentageIncrease = (2 * payrise) / 100;
            double newPay = Math.Round(payrise);
            var percentage = (2 * (payrise + maxPercentageIncrease)) / 100;
            List<double> payRises = new List<double>();
            for (double i = -percentage; i < percentage; i += 0.0001)
            {
                payRises.Add(i);
            }
            Random random = new Random();
            int grab = random.Next(0, payRises.Count);
            double genralPayrise = payrise + payRises[grab];
            Assert.NotEqual(2.2, genralPayrise);
        }

        [Fact]
        public void PayRiseBluringusingBiggerPercentage()
        {
            // the pay rise employees were given 3 years ago.
            //HR have agreed this pay rise should be blurred as it past 2 fincial years 
            //and HR have agreed a variance up or down of 5%.
            double payrise = 30;
            int blurrValue = 5;
            //this is the maximum amount allowed.
            var maxPercentageIncrease = (blurrValue * payrise) / 100;
            //double newPay = Math.Round(payrise); this could be used to create a more generic figure if decimals are used.
            // work out the maximum percentage we can blur by including the 5% increase.
            var percentage = (blurrValue * (payrise + maxPercentageIncrease)) / 100;
            //list to hold all the values.
            List<double> payRises = new List<double>();
            // start with minus 5% and work up to the maximum.
            for (double i = -percentage; i < percentage; i += 0.0001)
            {
                payRises.Add(i);
            }
            Random random = new Random();
            int grab = random.Next(0, payRises.Count);
            // use any figure in the list to blur the increase.
            double genralPayrise = payrise + payRises[grab];
            Assert.NotEqual(payrise, genralPayrise);
        }
    }
}
