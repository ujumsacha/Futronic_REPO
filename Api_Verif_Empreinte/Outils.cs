using Newtonsoft.Json;
using System;
using System.IO;
namespace Api_Verif_Empreinte
{
    public static class Outils
    {
        public static Param recup()
        {
            string lechemin = Path.Combine(Directory.GetCurrentDirectory(), "ConfigFolder");
            string file = Path.Combine(lechemin, "param.json");
            try
            {
                // Lisez le contenu du fichier en utilisant File.ReadAllText.
                string contenuFichier = File.ReadAllText(file);
                return JsonConvert.DeserializeObject<Param>(contenuFichier);
            }
            catch (Exception ex)
            {
                throw new Exception("erreur dans la deserialisation");
            }
        }
    }
}
