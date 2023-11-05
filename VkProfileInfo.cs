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
            return $@"Информация об аккаунте:
id: {id}
Имя: {first_name}
Фамилия: {last_name}
Отображаемое имя: {screen_name}
Город: {home_town}
Дата рождения: {bdate}";
        }
    }
}
