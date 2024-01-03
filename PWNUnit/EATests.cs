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
        [Test]
        public async Task LoginTest()
        {
            Console.WriteLine("Opened Browser");
            await Page.GotoAsync("http://eaapp.somee.com/");
            Console.WriteLine("Page Loaded");

            await Page.GetByText("Login").ClickAsync();
            await Console.Out.WriteLineAsync("Clicked on Login link");

            //full url
            await Expect(Page).ToHaveURLAsync("http://eaapp.somee.com/Account/Login");

            //partial url
            //await Expect(Page).ToHaveURLAsync(new Regex("Login"));

            await Page.GetByLabel("UserName").FillAsync(value: "admin");
            await Console.Out.WriteLineAsync("Typed UserName");

            await Page.GetByLabel("Password").FillAsync(value: "password");
            await Console.Out.WriteLineAsync("Typed Password");

            //await Page.Locator("//input[@value='Log in']").ClickAsync();
            var loginButton = Page.Locator(selector: "input", new PageLocatorOptions
            {
                HasTextString = "Log in"
            });
            await loginButton.ClickAsync();
            await Console.Out.WriteLineAsync("Clicked on Login button");

            await Expect(Page).ToHaveTitleAsync("Home - Execute Automation Employee App");



        }
    }
}
