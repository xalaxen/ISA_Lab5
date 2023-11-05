using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;

namespace Lab5
{
    public class VkApp
    {
        // Form elements
        private ChromiumWebBrowser browser;
        private TextBox InfoTextBox;
        private Button ProfileInfoBtn;
        private Button FriendsListBtn;

        static readonly HttpClient client = new HttpClient();

        private static string APP_ID = "51784243";
        private static string ACCESS_TOKEN { get; set; }
        private static string Version = "5.154";

        private string USER_ID { get; set; }

        public async Task<string> GET(string URL)
        {
            string response = await client.GetStringAsync(URL);
            return response;
        }

        public void GetUserId(ref object sender, ref CefSharp.FrameLoadEndEventArgs e)
        {
            if (e.Url.Contains(@"oauth.vk.com/blank.html") && e.Url.Contains(@"#"))
            {
                string url = e.Url.Split('#')[1];
                ACCESS_TOKEN += HttpUtility.ParseQueryString(url).Get("access_token");
                USER_ID += HttpUtility.ParseQueryString(url).Get("user_id");
            }
        }

        private Command logInCommand;
        public Command LogInCommand
        {
            get
            {
                return logInCommand ?? (logInCommand = new Command(obj =>
                {
                    var url = @"https://oauth.vk.com/authorize?client_id=" + APP_ID +
                        @"&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&v=5.52";
                    browser.Load(url);
                }));
            }
        }

        public async void GetProfileInfo()
        {
            HttpResponseMessage response = await client.GetAsync($"https://api.vk.ru/method/account.getProfileInfo?user_id={USER_ID}&access_token={ACCESS_TOKEN}&v={Version}");
            var responseContent = await response.Content.ReadAsStringAsync();
            InfoTextBox.Text = JsonToString(responseContent);
        }

        public async void GetFriendsList()
        {
            HttpResponseMessage response = await client.GetAsync($"https://api.vk.ru/method/friends.get?user_id={USER_ID}&fields=bdate&access_token={ACCESS_TOKEN}&v={Version}");
            var responseContent = await response.Content.ReadAsStringAsync();
            InfoTextBox.Text = JsonToString(responseContent);
        }

        public string JsonToString(string response)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(response);
            return JsonSerializer.Serialize(jsonElement, options);
        }

        public bool IsIDGot()
        {
            if (USER_ID != null) { return true; }
            return false;
        }

        public VkApp(ref ChromiumWebBrowser Browser)
        {
            browser = Browser;
        }

        public VkApp(ref VkApp model, ref TextBox infoTb, ref Button profileBtn, ref Button friendsBtn)
        {
            USER_ID = model.USER_ID;
            InfoTextBox = infoTb;
            ProfileInfoBtn = profileBtn;
            FriendsListBtn = friendsBtn;
        }
    }
}