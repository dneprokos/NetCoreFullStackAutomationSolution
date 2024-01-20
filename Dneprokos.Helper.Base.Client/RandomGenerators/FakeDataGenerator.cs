using Bogus;

namespace Dneprokos.Helper.Base.Client.RandomGenerators
{
    public class FakeDataGenerator
    {
        public static FakeDataGenerator Instance => new();

        private readonly Faker _faker;

        private FakeDataGenerator()
        {
            _faker = new Faker();
        }

        #region Person

        public string FirstName => _faker.Person.FirstName;
        public string LastName => _faker.Person.LastName;

        #endregion

        #region Lorem

        public string LoremText => _faker.Lorem.Text();

        public string LoremWord => _faker.Lorem.Word();

        #endregion
    }
}
