using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrintEcranPrincipal
{
    public class Outils
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


        public static AdminsystemeParam recupAdminParam()
        {
            string lechemin = Path.Combine(Directory.GetCurrentDirectory(), "ConfigFolder");
            string file = Path.Combine(lechemin, "AdminSystem.json");
            try
            {
                // Lisez le contenu du fichier en utilisant File.ReadAllText.
                string contenuFichier = File.ReadAllText(file);
                return JsonConvert.DeserializeObject<AdminsystemeParam>(contenuFichier);
            }
            catch (Exception ex)
            {
                throw new Exception("erreur dans la deserialisation");
            }
        }


        public static void ModifierContenueParam(bool is_used)
        {
            try
            {
                string lechemin = Path.Combine(Directory.GetCurrentDirectory(), "ConfigFolder");
                if (!Directory.Exists(lechemin))
                {
                    Directory.CreateDirectory(lechemin);
                }

                string lechemin1 = Path.Combine(Directory.GetCurrentDirectory(), "tempfolder");
                if (!Directory.Exists(lechemin1))
                {
                    Directory.CreateDirectory(lechemin1);
                }


                string file = Path.Combine(lechemin, "AdminSystem.json");

                if (File.Exists(file))
                {

                    //********************************Lire le contenue ******************************************
                    string contenu = File.ReadAllText(file);

                    var Result = JsonConvert.DeserializeObject<AdminsystemeParam>(contenu);
                    //********************************Lire le contenue ******************************************
                    using (StreamWriter fs = File.CreateText(file))
                    {
                        Result.modify_at = DateTime.Now;
                        Result.Use_empreinte = is_used;
                        fs.Write(JsonConvert.SerializeObject(Result));
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("erreur Systeme");
            }
            
        }

        public static (bool,bool) ReadContenue()
        {
            try
            {
                string lechemin = Path.Combine(Directory.GetCurrentDirectory(), "ConfigFolder");
                if (!Directory.Exists(lechemin))
                {
                    Directory.CreateDirectory(lechemin);
                }

                string lechemin1 = Path.Combine(Directory.GetCurrentDirectory(), "tempfolder");
                if (!Directory.Exists(lechemin1))
                {
                    Directory.CreateDirectory(lechemin1);
                }


                string file = Path.Combine(lechemin, "AdminSystem.json");

                if (File.Exists(file))
                {

                    //********************************Lire le contenue ******************************************
                    string contenu = File.ReadAllText(file);

                    var Result = JsonConvert.DeserializeObject<AdminsystemeParam>(contenu);
                    //********************************Lire le contenue ******************************************
                    return (true, Result.Use_empreinte);
                }
                else
                {
                    return (false, false);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("erreur Systeme");
            }
        }
    }
}
