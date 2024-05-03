using FingerPrintEcranPrincipal.Reponses;
using FingerPrintEcranPrincipal.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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


        public static string ListPropertiesAndValues(object obj)
        {
            // Obtention du type de l'objet
            Type objType = obj.GetType();

            // Obtention des propriétés publiques de l'objet
            PropertyInfo[] properties = objType.GetProperties();
            string contenue = "";
            // Parcours des propriétés et affichage de leur nom et valeur
            //Console.WriteLine("Propriétés et valeurs de l'objet :");
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj); // Obtention de la valeur de la propriété
                contenue+= $"{property.Name} : {value}" + Environment.NewLine;
            }
            return contenue;
        }

        public  static string ListPropertiesAndValuesEnroll(DtoEnroll dt)
        {
            string Contentdata = string.Empty;
            Contentdata += "RECAPITULATIF DES INFORMATIONS SIGNALETIQUES"+Environment.NewLine+Environment.NewLine;
            Contentdata += $"Numero CNI :{dt.txt_cni} "+Environment.NewLine;
            Contentdata += $"NOM :{dt.txt_nom} "+Environment.NewLine;
            Contentdata += $"PRENOMS :{dt.txt_prenom} "+Environment.NewLine;
            Contentdata += $"GENRE :{dt.txt_sexe} " +Environment.NewLine;
            Contentdata += $"Numero UNIQUE :{dt.txt_num_unique} "+Environment.NewLine;
            Contentdata += $"LIEU D'EMISSION :{dt.txt_lieu_emission} " +Environment.NewLine;
            Contentdata += $"LIEU DE NAISSANCE :{dt.txt_lieu_naissance} " +Environment.NewLine;
            Contentdata += $"NATIONNALITE :{dt.txt_nationnalite} " +Environment.NewLine;
            Contentdata += $"PROFESSION :{dt.txt_profession} " +Environment.NewLine;
            Contentdata += $"TAILLE (cm) :{dt.txt_taille} " +Environment.NewLine;
            Contentdata += $"DATE D'EMISSION :{dt.date_emiss_cni} " +Environment.NewLine;
            Contentdata += $"DATE D'EXPIRATION :{dt.date_expir_cni} " +Environment.NewLine;
            Contentdata += $"DATE DE NAISSANCE :{dt.date_naiss} " +Environment.NewLine;




            return Contentdata;
        }


        public static (bool, DataCarteNfc) Executerbatch(string type, string datenaiss, string datefin, string numero)
        {
            string urifile = @"C:/Users/sacha.ogou/source/repos/Futronic_REPO/FingerPrintEcranPrincipal/bin/Debug/net5.0-windows/APP NFC/NFCEXE.bat";


            string mesparams = "\" "+type+","+numero+","+datenaiss+","+datefin+" \"";
            try
            {
                using (StreamWriter writer = new StreamWriter(urifile))
                {
                    var test = @"@echo off " + Environment.NewLine;
                    test += "java -jar \"C:\\Users\\sacha.ogou\\source\\repos\\Futronic_REPO\\FingerPrintEcranPrincipal\\bin\\Debug\\net5.0-windows\\APP NFC\\scan.jar \" "+ mesparams+ Environment.NewLine; 
                    test +=" pause" + Environment.NewLine;
                    writer.Write(test);
                }
                string cheminFile = recup().CheminIntoBatfile;
                //********************************************Execute NFC*****************************************************
                string command = urifile;
                //string args = "MyParam1 MyParam2";

                Process process = new Process();
                process.StartInfo.FileName = command;
                //process.StartInfo.Arguments = args;
                process.Start();
                process.WaitForExit();

                if(!File.Exists(cheminFile))
                {
                    return (false, null);

                }
                string content = File.ReadAllText(cheminFile);
                GeneraleResponseNFC Gn1 = JsonConvert.DeserializeObject<GeneraleResponseNFC>(content);
                if (Gn1.code != 1)
                {
                    return (false, null);

                }

                //********************************************Execute NFC*****************************************************
                DataCarteNfc Dte = JsonConvert.DeserializeObject<DataCarteNfc>(Gn1.data);
                return (true, Dte);
            }
            catch (Exception ex)
            {

                return (false, null);
            }
        }
    }
}
