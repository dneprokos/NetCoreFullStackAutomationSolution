using System.Text;

namespace Dneprokos.Helper.Base.Client.RandomGenerators
{
    public static class StringGenerator
    {
        private static readonly Random Random = new();

        /// <summary>
        /// Generates random string. Upper-case by default
        /// </summary>
        /// <param name="size">Letters count</param>
        /// <param name="lowerCase">Should it be generated in lower case.</param>
        /// <returns></returns>
        public static string GenerateRandomString(int size, bool lowerCase = false)
        {
            if (size.Equals(0))
                return string.Empty;
            var builder = new StringBuilder(size);
            // char is a single Unicode character  
            var offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)Random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
