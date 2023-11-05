using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class FriendResponse
    {
        public int Count { get; set; }
        [JsonProperty("items")]
        public List<VkFriend> Items { get; set; }
    }

    public class RootObject
    {
        [JsonProperty("response")]
        public FriendResponse Response { get; set; }
    }

    public class City
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class VkFriend
    {
        [JsonProperty("id")]
        int id;
        [JsonProperty("bdate")]
        string bdate = "скрыт";
        [JsonProperty("first_name")]
        string first_name;
        [JsonProperty("last_name")]
        string last_name;
        [JsonProperty("city")]
        City city;

        public override string ToString()
        {
            return $@"id: {id}
Имя: {first_name}
Фамилия: {last_name}
Дата рождения: {bdate}
Город: {(city!=null ? city.Title : "не указан")}";
        }
    }
}
