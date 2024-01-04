using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PWPOM.PWTests.Pages;

namespace PWPOM.PWTests.Tests
{
    [TestFixture]
    public class LoginPageTest : PageTest
    {
        Dictionary<string, string> Properties;
        private void ReadConfigSettings()
        {
            Properties = new Dictionary<string, string>();
            string? currdir = Directory.GetParent(@"../../../")?.FullName;

            string fileName = currdir + "/configsettings/config.properties";
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains('='))
                {
                    string[] parts = line.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    Properties[key] = value;
                }
            }
        }

        [SetUp]
        public async Task Setup()
        {
            Console.WriteLine("Opened Browser");
            await Page.GotoAsync(Properties["baseUrl"]);
            Console.WriteLine("eaapp.somee home page loaded");
        }

        [Test]
        [TestCase("admin", "password")]
        [TestCase("admin", "****")]
        public async Task LoginTest(string username, string password)
        {
            /*
            LoginPage loginPage = new LoginPage(Page);

            await loginPage.ClickLoginLink();
            await Console.Out.WriteLineAsync("Clicked on Login link");

            await loginPage.Login(username,password);
            await Console.Out.WriteLineAsync("Typed UserName");
            await Console.Out.WriteLineAsync("Typed Password");
            await Console.Out.WriteLineAsync("Clicked on Login button");

            Assert.IsTrue(await loginPage.CheckhelloadminMessage());
            await Console.Out.WriteLineAsync("Hello admin! is visible");
            */

            NewLoginPage newLoginPage = new NewLoginPage(Page);

            await newLoginPage.ClickLoginLink();
            await Console.Out.WriteLineAsync("Clicked on Login link");

            await newLoginPage.Login(username, password);
            await Console.Out.WriteLineAsync("Typed UserName");
            await Console.Out.WriteLineAsync("Typed Password");
            await Console.Out.WriteLineAsync("Clicked on Login button");

            Assert.IsTrue(await newLoginPage.CheckhelloadminMessage());
            await Console.Out.WriteLineAsync("Hello admin! is visible");
        }
    }
}