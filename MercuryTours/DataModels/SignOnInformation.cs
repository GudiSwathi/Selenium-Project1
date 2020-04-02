using Newtonsoft.Json;

namespace MercuryTours.DataModels
{
  public  class SignOnInformation
    {
        [JsonProperty("userName", Required = Required.Always)]
        public string UserName { get; set; }
        [JsonProperty("password", Required = Required.Always)]
        public string Password { get; set; }
    }
}
