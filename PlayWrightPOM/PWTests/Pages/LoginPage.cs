using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PWPOM.PWTests.Pages
{
    internal class LoginPage
    {
        private IPage _page;
        private ILocator _loginLink;
        private ILocator _userNameInput;
        private ILocator _passwordInput;
        private ILocator _loginButton;
        private ILocator _helloadminMessage;

        public LoginPage(IPage page)
        {
            _page = page;
            _loginLink = _page.Locator(selector: "text=Login");
            _userNameInput = _page.Locator(selector: "#UserName");
            _passwordInput = _page.Locator(selector: "#Password");
            _loginButton = _page.Locator(selector: "input", new PageLocatorOptions
            {
                HasTextString = "Log in"
            });
            _helloadminMessage = _page.Locator(selector: "text='Hello admin!'");
        }

        public async Task ClickLoginLink()
        {
            await _loginLink.ClickAsync();
        }
        public async Task Login(string username,string password)
        {
            await _userNameInput.FillAsync(username);
            await _passwordInput.FillAsync(password);
            await _loginButton.ClickAsync();
        }
        public async Task<bool> CheckhelloadminMessage()
        {
            bool IsVisible = await _helloadminMessage.IsVisibleAsync();
            return IsVisible;
        }
    }
}
