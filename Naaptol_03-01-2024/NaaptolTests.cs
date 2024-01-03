using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Naaptol_03_01_2024
{
    [TestFixture]
    internal class NaaptolTests // : PageTest
    {
      //  [SetUp]
        /*public async Task SetUp()
        {
            Console.WriteLine("Opened Browser");
            await page.GotoAsync("https://www.naaptol.com/");
            Console.WriteLine("Naaptol home page loaded");
        }
*/
        [Test]
        public async Task SearchProductTest()
        {
            using var playwright = await Playwright.CreateAsync();

            //launch browser
                await using var browser = await playwright.Chromium.LaunchAsync(
                   new BrowserTypeLaunchOptions
                    {
                        Headless = false
                    });

            //page instance
                var context = await browser.NewContextAsync();
                var page = await context.NewPageAsync();
            await page.GotoAsync("https://www.naaptol.com/");

            ILocator searchInput = page.Locator("#header_search_text");
            await searchInput.ClickAsync();
            await searchInput.FillAsync("eyewear");

           // await searchInput.PressAsync(key: "Enter");
            await Console.Out.WriteLineAsync("Typed eyewear");
            // await searchInput.PressAsync(key: "Enter");
            // await Page.Locator(selector: " #header_search .search a").Locator("visible=true").ClickAsync();
            Thread.Sleep(2000);
            

            string title = await page.TitleAsync();
            await Console.Out.WriteLineAsync(title);
            

            await Console.Out.WriteLineAsync(page.Url);
           //await Expect(Page).ToHaveURLAsync("https://www.naaptol.com/search.html?type=srch_catlg&kw=eyewear");


        }
    }
}
