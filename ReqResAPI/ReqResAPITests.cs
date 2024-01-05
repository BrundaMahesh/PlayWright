using Microsoft.Playwright;
using System.Text.Json;

namespace PWReqResAPI
{
    [TestFixture]
    public class ReqResAPITests
    {
        IAPIRequestContext requestContext;

        [SetUp]
        public async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            requestContext = await playwright.APIRequest.NewContextAsync(
                new APIRequestNewContextOptions
                {
                    BaseURL = "https://reqres.in/api/", 
                    IgnoreHTTPSErrors = true,
                });
        }

        [Test]
        public async Task GetAllUsers()
        {
            var getResponse = await requestContext.GetAsync(url: "users?page=2");

            await Console.Out.WriteLineAsync("Response : " + getResponse.ToString());
            await Console.Out.WriteLineAsync("Code : " + getResponse.Status);
            await Console.Out.WriteLineAsync("Text : " + getResponse.StatusText);

            Assert.That(getResponse, Is.Not.Null);
            Assert.That(getResponse.Status.Equals(200));

            JsonElement jsonResponseBody = (JsonElement)await getResponse.JsonAsync();
            await Console.Out.WriteLineAsync("Json Response Body: " + jsonResponseBody.ToString());

        }
    }
}