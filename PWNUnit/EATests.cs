using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PWNUnit
{
    [TestFixture]
    internal class EATests : PageTest
    {
        [SetUp]
        public async Task SetUp()
        {
            Console.WriteLine("Opened Browser");
            await Page.GotoAsync("http://eaapp.somee.com/");
            Console.WriteLine("eaapp.somee home page loaded");
        }

        [Test]
        public async Task LoginTest()
        {

            //await Page.GetByText("Login").ClickAsync();

            /*ILocator loginLink = Page.Locator(selector: "text=Login");
            await loginLink.ClickAsync();
            */

            await Page.ClickAsync(selector: "text=Login");

            await Console.Out.WriteLineAsync("Clicked on Login link");


            //full url assert
            await Expect(Page).ToHaveURLAsync("http://eaapp.somee.com/Account/Login");

            //partial url assert
            //await Expect(Page).ToHaveURLAsync(new Regex("Login"));


            /*await Page.GetByLabel("UserName").FillAsync(value: "admin");
            await Console.Out.WriteLineAsync("Typed UserName");
            await Page.GetByLabel("Password").FillAsync(value: "password");
            await Console.Out.WriteLineAsync("Typed Password");
            */

            await Page.FillAsync(selector: "#UserName", "admin");
            await Console.Out.WriteLineAsync("Typed UserName");
            await Page.FillAsync(selector: "#Password", "password");
            await Console.Out.WriteLineAsync("Typed Password");

            //await Page.Locator("//input[@value='Log in']").ClickAsync();
            var loginButton = Page.Locator(selector: "input", new PageLocatorOptions
            {
                HasTextString = "Log in"
            });
            await loginButton.ClickAsync();
            await Console.Out.WriteLineAsync("Clicked on Login button");

            await Expect(Page).ToHaveTitleAsync("Home - Execute Automation Employee App");

            /*await Expect(Page.Locator(selector: "text='Hello admin!'")).ToBeVisibleAsync();
            await Console.Out.WriteLineAsync("Hello admin! is visible");
            */

            await Task.WhenAll
            (
                Expect(Page.Locator(selector: "text='Hello admin!'")).ToBeVisibleAsync(),
                Expect(Page.Locator(selector: "text='Log off'")).ToBeVisibleAsync()
            );
            await Console.Out.WriteLineAsync("Hello admin! is visible");
            await Console.Out.WriteLineAsync("Log off is visible");


        }
    }
}
