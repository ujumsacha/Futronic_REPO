using Api_Verif_Empreinte.Dtos;
using Newtonsoft.Json;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using   System.Threading.Tasks;
using System.IO;
using System.Linq;


namespace Api_Verif_Empreinte
{
    public class Program
    {
        public static string lecheminsortie = Path.Combine(Directory.GetCurrentDirectory(), "OutputJson");
        public static string lechemin = Path.Combine(Directory.GetCurrentDirectory(), "ConfigFolder");
        public static string _filename = Path.Combine(lecheminsortie, "people.json");
        static void Main(string[] args)
        {
            
            try
            {

                //**************************Creation du fichier si non existant******************************
                createfile();
                //**************************Creation du fichier si non existant******************************

                //**************************Verification de l'empreinte digital******************************
                (bool, double, string) res = BiometricVerification.Verify(args[0]).Result;
                if (!res.Item1)
                {
                    RetourApp rt = new RetourApp { p_Code = "ErrCP007", p_description = "aucune empreinte trouvé" };
                    WriteTofile(rt);
                    
                }
                else
                {
                    //**************************************recupere l'empreinte l'utilisateur dans la BD********************************
                    string[] retarray = res.Item3.Split("\\");
                    
                    RetourApp rt = new RetourApp { p_Code = "OK001", p_description = retarray.Last().ToString()};
                    Console.Write(JsonConvert.SerializeObject(rt));
                                       
                    //**************************************recupere l'empreinte l'utilisateur dans la BD********************************
                }
                //**************************Verification de l'empreinte digital******************************
            }
            catch (IndexOutOfRangeException ex1)
            {
                 
                RetourApp rt = new RetourApp { p_Code = "ErrCP008", p_description = "Vous n'avez passez aucun chemin de fichier" };

                WriteTofile(rt);

            }
            catch (Exception ex)
            {
                RetourApp rt = new RetourApp { p_Code = "ErrCP001", p_description = "Erreur systeme veuillez conatacter l'administrateur" };
                WriteTofile(rt);
            }

            finally { Console.WriteLine("close"); };
        }

        //******************************Fonction d'ecriture dans le fichier attendu *******************************************
        public static void WriteTofile(RetourApp data)
        {
            try
            {
                string jsonret = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(_filename, jsonret);
                
            }
            catch (Exception ex)
            {
                RetourApp rt = new RetourApp { p_Code = "ErrCP001", p_description = "Erreur systeme veuillez conatacter l'administrateur" };
                string jsonret = JsonConvert.SerializeObject(rt, Formatting.Indented);
                File.WriteAllText(_filename, jsonret);
               
            }
            
        }
        public static void createfile()
        {
            try
            {
                
                if (!Directory.Exists(lechemin))
                {
                    Directory.CreateDirectory(lechemin);
                }

                string lechemin1 = Path.Combine(Directory.GetCurrentDirectory(), "tempfolder");
                if (!Directory.Exists(lechemin1))
                {
                    Directory.CreateDirectory(lechemin1);
                } 
                
                if (!Directory.Exists(lecheminsortie))
                {
                    Directory.CreateDirectory(lecheminsortie);
                }


                string file = Path.Combine(lechemin, "param.json");

                if (!File.Exists(file))
                {
                    using (StreamWriter fs = File.CreateText(file))
                    {
                        Param pr = new Param();
                        pr.publicrepertory = "";
                        //pr.DatabaseString = "";
                        fs.Write(JsonConvert.SerializeObject(pr));
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Exception dans la creation de fichier");
            }
        }
    }
}
