using NUnit.Framework;

namespace Dneprokos.Helper.Base.Client.RandomGenerators
{
    public static class IntegerGenerator
    {
        private static readonly Random Random = new();

        /// <summary>
        /// Generates random integer
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns></returns>
        public static int GenerateRandomNumber(int length)
        {
            Assert.That(!length.Equals(0));
            string? numberAsString = null;
            for (var i = 0; i < length; i++)
            {
                numberAsString += GenerateRandomNumber(0, 9).ToString();
            }

            return int.Parse(numberAsString!);
        }

        /// <summary>
        /// Generates random integer
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GenerateRandomNumber(int min, int max)
        {
            return Random.Next(min, max + 1);
        }
    }
}
