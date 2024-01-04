using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PWPOM.PWTests.Pages;
using PWPOM.Test_Data_Classes;
using PWPOM.Utilities;

namespace PWPOM.PWTests.Tests
{
    [TestFixture]
    public class LoginPageTest : PageTest
    {
        Dictionary<string, string> Properties;
        string? currdir;
        private void ReadConfigSettings()
        {
            Properties = new Dictionary<string, string>();
            currdir = Directory.GetParent(@"../../../")?.FullName;

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
            ReadConfigSettings();
            Console.WriteLine("Opened Browser");
            await Page.GotoAsync(Properties["baseUrl"]);
            Console.WriteLine("eaapp.somee home page loaded");
        }

        [Test]
        public async Task LoginTest()
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

            string? excelFilePath = currdir + "/Test Data/EA Data.xlsx";
            string? sheetName = "LoginData";

            List<EAText> excelDataList = DataRead.ReadLoginData(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {

                string? username = excelData.UserName;
                string? password = excelData.Password;

                await Console.Out.WriteLineAsync(username + password);
                await newLoginPage.ClickLoginLink();
                await Console.Out.WriteLineAsync("Clicked on Login link");

                await newLoginPage.Login(username, password);
                await Page.ScreenshotAsync(new()
                {
                    Path =  currdir + "/Screenshots/screenshot.png",
                    FullPage = true,
                });

                await Console.Out.WriteLineAsync("Typed UserName");
                await Console.Out.WriteLineAsync("Typed Password");
                await Console.Out.WriteLineAsync("Clicked on Login button");

                Assert.That(await newLoginPage.CheckhelloadminMessage(), Is.True);
                await Console.Out.WriteLineAsync("Hello admin! is visible");
            }
        }
    }
}