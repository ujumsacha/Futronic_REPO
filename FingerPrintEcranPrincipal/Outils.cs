using FingerPrintEcranPrincipal.Reponses;
using FingerPrintEcranPrincipal.Request;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace FingerPrintEcranPrincipal
{
    public class Outils
    {

        static Outils()
        {
            var dt = DateTime.Now.Date.ToString().Substring(0, 10).Replace("/", "");
            Serilog.Log.Logger = new LoggerConfiguration()
                   .WriteTo.File($"logFolder/log_{dt}_outils.txt") // Écrit les logs dans un fichier
                   .CreateLogger();
        }

        


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
            Contentdata += $"GENRE :{((dt.txt_sexe=="M") ? "Homme": "Femme")} " +Environment.NewLine;
            Contentdata += $"Numero UNIQUE :{dt.txt_num_unique} "+Environment.NewLine;
            Contentdata += $"LIEU D'EMISSION :{dt.txt_lieu_emission} " +Environment.NewLine;
            Contentdata += $"LIEU DE NAISSANCE :{dt.txt_lieu_naissance} " +Environment.NewLine;
            Contentdata += $"NATIONNALITE :{dt.txt_nationnalite} " +Environment.NewLine;
            Contentdata += $"PROFESSION :{dt.txt_profession} " +Environment.NewLine;
            Contentdata += $"TAILLE (cm) :{dt.txt_taille} " +Environment.NewLine;
            Contentdata += $"DATE D'EMISSION :{dt.date_emiss_cni} " +Environment.NewLine;
            Contentdata += $"DATE D'EXPIRATION :{dt.date_expir_cni} " +Environment.NewLine; 
            Contentdata += $"DATE DE NAISSANCE :{dt.date_naiss} " +Environment.NewLine;
            Contentdata += $"NNI :{dt.txt_nni} " +Environment.NewLine;

            return Contentdata;
        }


        public static (bool, object?) Executerbatch(string type, string datenaiss, string datefin, string numero)
        {
            string urifile = recup().NFCappLaunch;

            Log.Information("DEBUT =======> :" + urifile);
            string mesparams = "\""+type+","+numero+","+datenaiss+","+datefin+"\"";
            try
            {
                using (StreamWriter writer = new StreamWriter(urifile))
                {
                    var test = @"@echo off " + Environment.NewLine;
                    test += "java -jar \"" + recup().CheminScanJar + "\" " + mesparams+Environment.NewLine;
                    //test += "pause";
                    writer.Write(test);
                }

                string cheminFile = recup().CheminIntoBatfile;
                //********************************************Execute NFC*****************************************************
                string command = urifile;
                //string args = "MyParam1 MyParam2";




                //*****************************************USE PROCESS START*****************************************
                Log.Information("Debut de traitement Initialisation Execution commande");
                Process process = new Process();
                process.StartInfo.FileName = command;
                //process.StartInfo.Arguments = args;
                process.Start();

                process.WaitForExit();
                //Thread.Sleep(3000);
                Log.Information("Debut de traitement Initialisation Execution commande");
                //*****************************************USE PROCESS START*****************************************
                ///
                //*****************************************USE PROCESS START UPDATE*****************************************
                //Process.Start(command).WaitForExit();

                //*****************************************USE PROCESS START UPDATE*****************************************

                //*******************************************USE PROCESS START POUR INVITE DE COMMANDE************************
                //string  test1 = "java -jar \"" + recup().CheminScanJar + "\" " + mesparams + Environment.NewLine;


                //Log.Information("Debut de traitement Initialisation Execution commande");
                //string test1 = $"java -jar \"{recup().CheminScanJar}\" \"{mesparams}\"";
                //Process process = new Process();
                //process.StartInfo.FileName = "cmd.exe";
                //process.StartInfo.Arguments = $"/C {test1}"; // Commande à exécuter
                //process.StartInfo.UseShellExecute = true;
                //process.Start();
                //Log.Information("Debut de traitement Initialisation Execution commande");
                //process.WaitForExit();
                //////*******************************************USE PROCESS START POUR INVITE DE COMMANDE************************

                if (!File.Exists(cheminFile))
                {
                    return (false, "Fichier Inexistant");
                }
                
                string content = File.ReadAllText(cheminFile);
                Log.Information("LIEN DU FICHIER =======> :" + cheminFile);
                Log.Information("Lecture du fichier OK =======> :" + content);
                GeneraleResponseNFC Gn1 = JsonConvert.DeserializeObject<GeneraleResponseNFC>(content);
                if (Gn1.status != 1)
                {
                    Log.Information("rentrer dans l'erreur car code egale a :"+Gn1.status);
                    return (false, "Erreur dans la lecture du fichier");
                }
                Log.Information("rentrer ok car  :" + Gn1.status);
                //********************************************Execute NFC*****************************************************
                DataCarteNfc Dte = JsonConvert.DeserializeObject<DataCarteNfc>(Gn1.data);
                Log.Information("Gn1.data :" + Gn1.data);
                return (true, Dte);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return (false, new string(ex.Message));
            }
        }
    }
}
