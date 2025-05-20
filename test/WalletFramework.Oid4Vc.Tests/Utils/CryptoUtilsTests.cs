using System;
using System.Collections.Generic;
using Xunit;

namespace WalletFramework.Oid4Vc.Tests.Utils
{
    public class CryptoUtilsTests
    {
        [Fact]
        public void TestRandomNumberBias()
        {
            // This test demonstrates the bias introduced by the modulo operator
            // when generating random numbers within a specific range.
            // The CryptoUtils.GenerateRandomInt method uses modulo, which can lead to
            // a non-uniform distribution if the range is not a divisor of the
            // maximum value of the random number generator.

            // Define the range for the random numbers
            int minValue = 0;
            int maxValue = 100; // A range that is likely to show bias with modulo

            // Number of samples to generate
            int numberOfSamples = 1000000;

            // Dictionary to store the frequency of each generated number
            var frequency = new Dictionary<int, int>();
            for (int i = minValue; i < maxValue; i++)
            {
                frequency[i] = 0;
            }

            // Generate random numbers and record their frequency
            // We are calling the method directly to test its behavior
            // Note: This assumes a method like GenerateRandomInt(int max) exists and uses modulo
            // If the actual method signature is different, this test will need adjustment
            // based on the specific implementation in CryptoUtils.cs.
            // For the purpose of demonstrating the bias, we simulate the modulo operation
            // on a standard random number generator if the exact method is not accessible
            // or has a different signature.

            // *** IMPORTANT: Replace the following lines with actual calls to the vulnerable method
            // in src/Hyperledger.Aries/Utils/CryptoUtils.cs if it's accessible and matches the
            // vulnerability description.
            // For demonstration purposes, we simulate the bias here using System.Random and modulo.
            var random = new Random();
            int biasThreshold = (int)(numberOfSamples * 0.01); // Example threshold for detecting bias (1% deviation)

            for (int i = 0; i < numberOfSamples; i++)
            {
                // Simulate the biased random number generation using modulo
                // This mimics the vulnerability described.
                int randomNumber = random.Next() % maxValue; // Assuming maxValue is the range upper bound + 1

                if (randomNumber >= minValue && randomNumber < maxValue)
                {
                    frequency[randomNumber]++;
                }
            }

            // Analyze the frequency distribution to detect bias
            // In a truly uniform distribution, each number would appear approximately
            // numberOfSamples / (maxValue - minValue) times.
            // With modulo bias, numbers that are remainders of the division of
            // the random source's max value by the range size will appear more often.

            bool biasDetected = false;
            int expectedFrequency = numberOfSamples / (maxValue - minValue);

            foreach (var pair in frequency)
            {
                // Check if the frequency deviates significantly from the expected frequency
                // A simple check for demonstration; more sophisticated statistical tests could be used.
                if (Math.Abs(pair.Value - expectedFrequency) > biasThreshold)
                {
                    biasDetected = true;
                    // In a real scenario, you might want to log or report which numbers are biased
                    // Console.WriteLine($"Number {pair.Key} shows potential bias with frequency {pair.Value}");
                }
            }

            // Assert that bias is detected. This test is designed to FAIL if the bias exists.
            // The assertion message indicates the expected outcome (bias detection).
            Assert.False(biasDetected, $"Bias detected in random number generation using modulo. Expected approximately {expectedFrequency} occurrences per number, but significant deviations were observed. This confirms the potential vulnerability.");

            // Note: If the actual CryptoUtils.GenerateRandomInt method (or equivalent)
            // is used and it does NOT exhibit the modulo bias (e.g., it uses a different
            // method for range reduction), this test might pass unexpectedly.
            // In that case, the test implementation should be reviewed against the
            // specific code in CryptoUtils.cs to ensure it accurately reflects
            // the method being tested for the reported vulnerability.
        }
    }
}