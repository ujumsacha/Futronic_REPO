using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FingerPrintEcranPrincipal.Request
{
    public class DtoEnroll
    {
        [JsonPropertyName("Numero CNI")]
        public string txt_cni { get; set; }
        [JsonPropertyName("Lieu d'emission")]
        public string txt_lieu_emission { get; set; }
        [JsonPropertyName("Lieu de naissance")]
        public string txt_lieu_naissance { get; set; }
        [JsonPropertyName("Nationalité")]
        public string txt_nationnalite { get; set; }
        [JsonPropertyName("Numero NNI")]
        public string txt_nni { get; set; }
        [JsonPropertyName("Nom")]
        public string txt_nom { get; set; }
        [JsonPropertyName("Numero Unique")]
        public string txt_num_unique { get; set; }
        [JsonPropertyName("prenoms")]
        public string txt_prenom { get; set; }
        [JsonPropertyName("Profession")]
        public string txt_profession { get; set; }
        [JsonPropertyName("Taille(cm) ")]
        public string txt_taille { get; set; }
        [JsonPropertyName("Sexe")]
        public char txt_sexe { get; set; }

        [JsonPropertyName("Date de Naissance")]
        public string date_naiss { get; set; } // "AAAA/MM/JJ"
        [JsonPropertyName("Date emission")]
        public string date_emiss_cni { get; set; } // "AAAA/MM/JJ"
        [JsonPropertyName("Date expiration")]
        public string date_expir_cni { get; set; } // "AAAA/MM/JJ"
        [JsonPropertyName("type_piece")]
        public string type_piece { get; set; } // "AAAA/MM/JJ"


        [JsonPropertyName("empreintes")]
        public DtoEmpreinte empreintes { get; set; }
    }

    public class DtoEmpreinte
    {
        public string pouce { get; set; }
        public string index { get; set; }
        public string majeur { get; set; }
        public string annulaire { get; set; }
        public string auriculaire { get; set; }
    }
}
