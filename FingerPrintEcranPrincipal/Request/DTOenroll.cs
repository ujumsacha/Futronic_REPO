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
        public string txt_cni { get; set; }
        public string txt_lieu_emission { get; set; }
        public string txt_lieu_naissance { get; set; }
        public string txt_nationnalite { get; set; }
        public string txt_nni { get; set; }
        public string txt_nom { get; set; }
        public string txt_num_unique { get; set; }
        public string txt_prenom { get; set; }
        public string txt_profession { get; set; }
        public string txt_taille { get; set; }
        public char txt_sexe { get; set; }


        public string date_naiss { get; set; } // "AAAA/MM/JJ"
        public string date_emiss_cni { get; set; } // "AAAA/MM/JJ"
        public string date_expir_cni { get; set; } // "AAAA/MM/JJ"


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
