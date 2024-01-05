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

        [Test]
        public async Task GetSingleUser()
        {
            var getResponse = await requestContext.GetAsync(url: "users/2");

            await Console.Out.WriteLineAsync("Response : " + getResponse.ToString());
            await Console.Out.WriteLineAsync("Code : " + getResponse.Status);
            await Console.Out.WriteLineAsync("Text : " + getResponse.StatusText);

            Assert.That(getResponse, Is.Not.Null);
            Assert.That(getResponse.Status.Equals(200));

            JsonElement jsonResponseBody = (JsonElement)await getResponse.JsonAsync();
            await Console.Out.WriteLineAsync("Json Response Body: " + jsonResponseBody.ToString());

        }

        [Test]
        public async Task GetSingleUserNotFound()
        {
            var getResponse = await requestContext.GetAsync(url: "users/23");

            await Console.Out.WriteLineAsync("Response : " + getResponse.ToString());
            await Console.Out.WriteLineAsync("Code : " + getResponse.Status);
            await Console.Out.WriteLineAsync("Text : " + getResponse.StatusText);

            Assert.That(getResponse, Is.Not.Null);
            Assert.That(getResponse.Status.Equals(404));

            JsonElement jsonResponseBody = (JsonElement)await getResponse.JsonAsync();
            await Console.Out.WriteLineAsync("Json Response Body: " + jsonResponseBody.ToString());

            Assert.That(jsonResponseBody.ToString(),Is.EqualTo("{}"));
        }

        [Test]
        public async Task PostUser()
        {
            var postData = new
            {
                name = "John",
                job = "Engineer"
            };

            var jsonData = System.Text.Json.JsonSerializer.Serialize(postData);   

            var postResponse = await requestContext.PostAsync(url: "users",
                new APIRequestContextOptions
                {
                    Data = jsonData
                });

            await Console.Out.WriteLineAsync("Response : " + postResponse.ToString());
            await Console.Out.WriteLineAsync("Code : " + postResponse.Status);
            await Console.Out.WriteLineAsync("Text : " + postResponse.StatusText);

            Assert.That(postResponse, Is.Not.Null);
            Assert.That(postResponse.Status.Equals(201));

            
        }

    }
}