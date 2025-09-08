using Allure.Net.Commons;
using Dneprokos.Api.Base.Client.Extenstions;
using Dneprokos.Helper.Base.Client.RandomGenerators;
using Dneprokos.Movies.Api.Client.Data;
using Dneprokos.Movies.Api.Client.Models.Genres;
using Dneprokos.Movies.Api.Client.RequestActions;
using Dneprokos.Movies.Api.Tests.BaseClasses;
using Dneprokos.Movies.Api.Tests.Utils;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using System.Net;

namespace Dneprokos.Movies.Api.Tests.Tests.Genres
{
    [Parallelizable]
    [AllureSuite("GENRES")]
    [AllureSubSuite("POST /genres")]
    public class CreateGenreApiTests : MoviesApiTestBase
    {
        private List<int> _idsToCleanUp = new();

        [OneTimeTearDown]
        public void AfterFeature()
        {
            _idsToCleanUp.ForEach(id 
                => GenreActions.DeleteGenreSuccessfully(AdminAuthentication!, Logger!, BaseUrl!, id));
        }

        [Test]
        [AllureTag(AllureTags.MainFlow)]
        [AllureSeverity(SeverityLevel.critical)]
        public void CreateGenre_WithValidNameLength_ShouldBeCreated()
        {
            //Arrange
            var newGenreName = StringGenerator.GenerateRandomString(3);

            //Act
            GenreApiModel responseBody = MoviesApiRequests!
                .Genres()
                .WithBodyName(newGenreName)
                .SendPostGenres(AdminAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.Created)
                .ConvertJsonToModel<GenreApiModel>()!;

            //Assert
            using (new AssertionScope())
            {
                responseBody.Name.Should().Be(newGenreName);
                responseBody.Id.Should().BeGreaterThan(0);
            }
        }

        [Test]
        [AllureSeverity(SeverityLevel.minor)]
        public void CreateGenre_WithNameLessThanMinSymbols_ShouldBeBadRequest()
        {
            //Arrange
            var newGenreName = StringGenerator.GenerateRandomString(2);

            //Act
            string responseContent = MoviesApiRequests!
                .Genres()
                .WithBodyName(newGenreName)
                .SendPostGenres(AdminAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.BadRequest)
                .Content!;

            //Assert
            responseContent.Should().Be(MoviesApiConstants.NameLessSymbolsMesssage);
        }

        [Test]
        [AllureSeverity(SeverityLevel.critical)]
        public void CreateGenre_WithNonAdminToken_ShouldBeForbidden()
        {
            //Arrange
            var newGenreName = StringGenerator.GenerateRandomString(3);

            //Act
            string responseContent = MoviesApiRequests!
                .Genres()
                .WithBodyName(newGenreName)
                .SendPostGenres(RegularAuthentication!)
                .VerifyStatusCodeIsEqualTo(HttpStatusCode.Forbidden)
                .Content!;

            //Assert
            responseContent.Should().Be(MoviesApiConstants.NoUserPermissionsMessage);
        }

        [Test]
        public void Temp()
        {
            //Arrange
            var test = StringGenerator.GenerateRandomString(100);

            //Act
            char [] chars = test.ToCharArray();
            Dictionary<char, int> charsRepeatCount = new();

            foreach (char c in chars)
            {
                bool isContain = charsRepeatCount.ContainsKey(c);
                if (isContain)
                    charsRepeatCount[c] += 1;
                else charsRepeatCount.Add(c, 1);
            }

            //Assert
            foreach (KeyValuePair<char, int> pair in charsRepeatCount)
            {
                Console.WriteLine($"KEY: {pair.Key}, VALUE: {pair.Value}");
            }


            var pairWithMaxValue = charsRepeatCount.MaxBy(pair => pair.Value);
            Console.WriteLine($"Pair with max value KEY: {pairWithMaxValue.Key}, VALUE: {pairWithMaxValue.Value}");
        }

        [Test]
        public void Temp2()
        {
            var excludeChars = new List<char> { ',', ' ', '!', '<', '#'};
            var test = "blaffdf , !asfdsfdfd< # csfdsdfsdfsdfsdf";

            var filtered = new string(test.Where(@char => !excludeChars.Contains(@char)).ToArray());

        }

        [Test]
        public void Temp3()
        {
            var list = new List<string> { "test", "Test", "tset" };
            List<string> capitalizedWords = list.Where(s => char.IsUpper(s[0])).ToList();

            var aggregate = list.Aggregate((a, b) =>  a + b);
            Console.WriteLine(aggregate);

            var list2 = new List<string> { "test", "test" };
        }
    }
}
