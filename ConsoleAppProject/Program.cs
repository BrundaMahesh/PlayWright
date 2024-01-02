using Microsoft.Playwright;

//playwright startup
using var playwright = await Playwright.CreateAsync();

//launch browser
await using var browser = await playwright.Chromium.LaunchAsync();

//page instance
var context = await browser.NewContextAsync();
var page = await context.NewPageAsync();



