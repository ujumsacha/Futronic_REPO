using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api_Verif_Empreinte.Dtos
{
    public class RetourApp
    {
        [JsonPropertyName("Code")]
        public string p_Code { get; set; }
        [JsonPropertyName("Description")]
        public string p_description { get; set; }
    }
}
