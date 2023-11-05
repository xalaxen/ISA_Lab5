using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Text.Json;

namespace Lab5
{
    public class VkApp
    {
        // Form elements
        private ChromiumWebBrowser browser;
        private TextBox InfoTextBox;

        static readonly HttpClient client = new HttpClient();

        private static string APP_ID = "51784243";
        private static string Version = "5.154";
        private static string ACCESS_TOKEN { get; set; }
        private string USER_ID { get; set; }


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

        public void GetUserId(ref object sender, ref CefSharp.FrameLoadEndEventArgs e)
        {
            if (e.Url.Contains(@"oauth.vk.com/blank.html") && e.Url.Contains(@"#"))
            {
                string url = e.Url.Split('#')[1];
                ACCESS_TOKEN += HttpUtility.ParseQueryString(url).Get("access_token");
                USER_ID += HttpUtility.ParseQueryString(url).Get("user_id");
            }
        }

        public async void GetProfileInfo()
        {
            HttpResponseMessage response = await client.GetAsync(GetRequestUrl("account.getProfileInfo", new Dictionary<string, string>
            {
                ["user_id"] = USER_ID,
                ["access_token"] = ACCESS_TOKEN,
                ["lang"] = "ru",
                ["v"] = Version
            }));

            var responseContent = await response.Content.ReadAsStringAsync();
            var vkResponse  = JsonConvert.DeserializeObject<ProfileResponse>(responseContent);

            InfoTextBox.Text = vkResponse.ProfileInfo.ToString();
        }

        public async void GetFriendsList()
        {
            HttpResponseMessage response = await client.GetAsync(GetRequestUrl("friends.get", new Dictionary<string, string>
            {
                ["user_id"] = USER_ID,
                ["fields"] = "bdate,city",
                ["access_token"] = ACCESS_TOKEN,
                ["lang"] = "ru",
                ["v"] = Version
            }));

            string responseContent = await response.Content.ReadAsStringAsync();
            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(responseContent);
            List<VkFriend> friendsList = rootObject.Response.Items;
            StringBuilder stringBuilder = new StringBuilder();

            foreach (VkFriend friend in friendsList)
            {
                stringBuilder.Append(friend);
                stringBuilder.Append("\n\n");
            }

            InfoTextBox.Text = stringBuilder.ToString();
        }
        
        public string GetRequestUrl(string method, Dictionary<string, string> parameters)
        {
            var builder = new UriBuilder($"https://api.vk.ru/method/{method}");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["access_token"] = ACCESS_TOKEN;
            query["v"] = Version;

            foreach(var key in parameters.Keys)
            {
                query[key] = parameters[key];
            }

            builder.Query = query.ToString();
            string url = builder.ToString();
            return url;
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

        public VkApp(ref VkApp model, ref TextBox infoTb)
        {
            USER_ID = model.USER_ID;
            InfoTextBox = infoTb;
        }
    }
}