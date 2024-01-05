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
        [TestCase(2)]
        public async Task GetAllUsers(int page)
        {
            var getResponse = await requestContext.GetAsync(url: "users?page=" + page);

            await Console.Out.WriteLineAsync("Response : " + getResponse.ToString());
            await Console.Out.WriteLineAsync("Code : " + getResponse.Status);
            await Console.Out.WriteLineAsync("Text : " + getResponse.StatusText);

            Assert.That(getResponse, Is.Not.Null);
            Assert.That(getResponse.Status.Equals(200));

            JsonElement jsonResponseBody = (JsonElement)await getResponse.JsonAsync();
            await Console.Out.WriteLineAsync("Json Response Body: " + jsonResponseBody.ToString());

        }

        [Test]
        [TestCase(2)]
        public async Task GetSingleUser(int uid)
        {
            var getResponse = await requestContext.GetAsync(url: "users/" + uid);

            await Console.Out.WriteLineAsync("Response : " + getResponse.ToString());
            await Console.Out.WriteLineAsync("Code : " + getResponse.Status);
            await Console.Out.WriteLineAsync("Text : " + getResponse.StatusText);

            Assert.That(getResponse, Is.Not.Null);
            Assert.That(getResponse.Status.Equals(200));

            JsonElement jsonResponseBody = (JsonElement)await getResponse.JsonAsync();
            await Console.Out.WriteLineAsync("Json Response Body: " + jsonResponseBody.ToString());

        }

        [Test]
        [TestCase(23)]
        public async Task GetSingleUserNotFound(int uid)
        {
            var getResponse = await requestContext.GetAsync(url: "users/" + uid);

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
        [TestCase("John","Engineer")]
        public async Task PostUser(string uname,string ujob)
        {
            var postData = new
            {
                name = uname,
                job = ujob
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

        [Test]
        [TestCase(2, "John", "Engineer")]
        public async Task PutUser(int uid, string uname, string ujob)
        {
            var putData = new
            {
                name = uname,
                job = ujob
            };

            var jsonData = System.Text.Json.JsonSerializer.Serialize(putData);

            var putResponse = await requestContext.PutAsync(url: "users/" + uid,
                new APIRequestContextOptions
                {
                    Data = jsonData
                });

            await Console.Out.WriteLineAsync("Response : " + putResponse.ToString());
            await Console.Out.WriteLineAsync("Code : " + putResponse.Status);
            await Console.Out.WriteLineAsync("Text : " + putResponse.StatusText);

            Assert.That(putResponse, Is.Not.Null);
            Assert.That(putResponse.Status.Equals(200));

        }

        [Test]
        [TestCase(2)]
        public async Task DeleteUser(int uid)
        {
           
            var deleteResponse = await requestContext.DeleteAsync(url: "users/" + uid);

            await Console.Out.WriteLineAsync("Response : " + deleteResponse.ToString());
            await Console.Out.WriteLineAsync("Code : " + deleteResponse.Status);
            await Console.Out.WriteLineAsync("Text : " + deleteResponse.StatusText);

            Assert.That(deleteResponse, Is.Not.Null);
            Assert.That(deleteResponse.Status.Equals(204));

        }

    }
}