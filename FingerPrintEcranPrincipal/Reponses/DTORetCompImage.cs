using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrintEcranPrincipal.Reponses
{
    public class DTORetCompImage
    {
        public string r_id { get; set; }
        public string r_num_cni { get; set; }
        public string r_num_unique { get; set; }
        public string r_nom { get; set; }
        public string r_prenom { get; set; }
        public string r_sexe { get; set; }
        public int r_taille { get; set; }
        public string r_nationnalite { get; set; }
        public string r_lieu_de_naissance { get; set; }
        public string r_date_expiration { get; set; }
        public string r_NNI { get; set; }
        public string r_profession { get; set; }
        public string r_date_emission { get; set; }
        public string r_lieu_emission { get; set; }
        public string r_created_by { get; set; }
        public string r_created_on { get; set; }
        public string r_updated_by { get; set; }
        public string r_updated_on { get; set; }
        public bool r_is_lock { get; set; }
        public string r_description_lock { get; set; }
        public string r_date_naissance { get; set; }
    }
}
