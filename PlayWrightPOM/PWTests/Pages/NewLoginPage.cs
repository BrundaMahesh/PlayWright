using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PWPOM.PWTests.Pages
{
    internal class NewLoginPage
    {
        private IPage _page;
        private ILocator LoginLink => _page.Locator(selector: "text=Login");
        private ILocator UserNameInput => _page.Locator(selector: "#UserName");
        private ILocator PasswordInput => _page.Locator(selector: "#Password");
        private ILocator LoginButton => _page.Locator(selector: "input", new PageLocatorOptions
        {
            HasTextString = "Log in"
        });
        private ILocator HelloadminMessage => _page.Locator(selector: "text='Hello admin!'");

        public NewLoginPage(IPage page) => _page = page;


        public async Task ClickLoginLink() => await LoginLink.ClickAsync();

        public async Task Login(string username,string password)
        {
            await UserNameInput.FillAsync(username);
            await PasswordInput.FillAsync(password);
            await LoginButton.ClickAsync();
        }
        public async Task<bool> CheckhelloadminMessage() => await HelloadminMessage.IsVisibleAsync();

    }
}
