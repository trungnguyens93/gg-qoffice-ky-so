using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoffice_qn_kyso.App.Models.Include
{
    public class FileDuThaoInclude
    {
        [JsonProperty("file_id")]
        public long FileId { get; set; }
    }
}
