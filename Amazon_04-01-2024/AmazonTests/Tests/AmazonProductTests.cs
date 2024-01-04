using Amazon_04_01_2024.AmazonTests.Pages;
using Amazon_04_01_2024.Test_Data_Classes;
using Amazon_04_01_2024.Utilities;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon_04_01_2024.AmazonTests.Tests
{
    internal class AmazonProductTests : PageTest
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
            await Page.GotoAsync(Properties["baseUrl"], new Microsoft.Playwright.PageGotoOptions
            { WaitUntil = Microsoft.Playwright.WaitUntilState.DOMContentLoaded});
            Console.WriteLine("Amazon home page loaded");
        }

        [Test]
        public async Task SearchProductTest()
        {
            HomePage homePage = new HomePage(Page);

            string? excelFilePath = currdir + "/Test Data/AmazonData.xlsx";
            string? sheetName = "SearchData";

            List<AmazonText> excelDataList = DataRead.ReadData(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {

                string? searchtext = excelData.SearchText;
                homePage.Search(searchtext);

                await Console.Out.WriteLineAsync("Clicked on Search Input Box");
                await Console.Out.WriteLineAsync($"Typed searchtext: {searchtext}");


                await Console.Out.WriteLineAsync(Page.Url);


            }
        }
    }
}
