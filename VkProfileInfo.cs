using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class ProfileResponse
    {
        [JsonProperty("response")]
        public VkProfileInfo ProfileInfo { get; set; }
    }

    public class VkProfileInfo
    {
        [JsonProperty("id")]
        int id;
        [JsonProperty("home_town")]
        string home_town;
        [JsonProperty("first_name")]
        string first_name;
        [JsonProperty("last_name")]
        string last_name;
        [JsonProperty("bdate")]
        string bdate;
        [JsonProperty("screen_name")]
        string screen_name;

        public override string ToString()
        {
            return $@"Information about account:
id = {id}
name = {first_name}
surname = {last_name}
screen name = {screen_name}
hometown = {home_town}
bdate = {bdate}";
        }
    }
}
