using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PWPOM.PWTests.Pages;

namespace PWPOM.PWTests.Tests
{
    [TestFixture]
    public class LoginPageTest : PageTest
    {
        [SetUp]
        public async Task Setup()
        {
            Console.WriteLine("Opened Browser");
            await Page.GotoAsync("http://eaapp.somee.com/", new PageGotoOptions
            {
                Timeout = 5000,
                WaitUntil = WaitUntilState.DOMContentLoaded
            });
            Console.WriteLine("eaapp.somee home page loaded");
        }

        [Test]
        [TestCase("admin", "password")]
        [TestCase("admin", "****")]
        public async Task LoginTest(string username, string password)
        {
            LoginPage loginPage = new LoginPage(Page);

            await loginPage.ClickLoginLink();
            await Console.Out.WriteLineAsync("Clicked on Login link");

            await loginPage.Login(username,password);
            await Console.Out.WriteLineAsync("Typed UserName");
            await Console.Out.WriteLineAsync("Typed Password");
            await Console.Out.WriteLineAsync("Clicked on Login button");

            Assert.IsTrue(await loginPage.CheckhelloadminMessage());
            await Console.Out.WriteLineAsync("Hello admin! is visible");
        }
    }
}