﻿using Microsoft.Playwright;

//playwright startup
using var playwright = await Playwright.CreateAsync();

//launch browser
await using var browser = await playwright.Chromium.LaunchAsync();

//page instance
var context = await browser.NewContextAsync();
var page = await context.NewPageAsync();

Console.WriteLine("Opened Browser");
await page.GotoAsync("https://www.google.com");
Console.WriteLine("Page Loaded");

await page.GetByTitle("Search").FillAsync("hp laptop");
Console.WriteLine("Typed Search text");

///await page.GetByRole("button").ClickAsync();
