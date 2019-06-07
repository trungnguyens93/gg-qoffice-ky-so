using Newtonsoft.Json;

namespace eoffice_qn_kyso.App.ViewModels
{
    public class BaseResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; } 
    }
}
