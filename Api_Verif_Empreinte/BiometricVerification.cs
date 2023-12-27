using SourceAFIS;
using System.Collections.Generic;
using Image = System.Drawing.Image;
using Microsoft.Extensions.Configuration;

using System;

using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using Npgsql;

//using Microsoft.Extensions.FileSystemGlobbing;

namespace Api_Verif_Empreinte
{
    public static class BiometricVerification
    {
        private static readonly IConfiguration iconfig;
        private static List<string> ret = new List<string>();
        public static Task<(bool, double,string)> Verify(string fingerprint1)
        {
            int nbrTentative = 0;
            string? bmpdefault = "";
            try
            {
                LabelDepart:
                //string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "FolderImage"); // Remplacez par le chemin de votre répertoire
                string directoryPath = Outils.recup().publicrepertory; // Remplacez par le chemin de votre répertoire
                var verificationOptions = new FingerprintImageOptions
                {
                    Dpi = 500
                };

                ListAllFiles(directoryPath);
                //convertir ma premiere image en byte array 
                //byte[] imageBytes;

                //using (MemoryStream ms = new MemoryStream())
                //{
                //    fingerprint1.Save(ms, fingerprint1.RawFormat);
                //    imageBytes = ms.ToArray();
                //}
                //convertir ma premiere image en byte array 
                bmpdefault = "before go to checke repertoire";
                // Vérifier si le répertoire existe
                if (Directory.Exists(directoryPath))
                {
                    // Récupérer tous les fichiers .bmp du répertoire
                    string[] bmpFiles = Directory.GetFiles(directoryPath, "*.bmp");

                    foreach (string bmpFile in ret)
                    {
                        Image fingerprint2 = Image.FromFile(bmpFile);
                        //var probe = new FingerprintTemplate(new FingerprintImage(fingerprint1.Width, fingerprint1.Height,
                        //    new byte[8]));
                        var probe = new FingerprintTemplate( new FingerprintImage(convertTobytearray(fingerprint1)));

                        var candidate = new FingerprintTemplate(new FingerprintImage(convertTobytearray(bmpFile)));

                        double score = new FingerprintMatcher(probe).Match(candidate);

                        double threshold = 40;
                        bool match = score >= threshold;
                        if(match)
                            return Task.FromResult((match, score, string.Empty));

                    }
                    return Task.FromResult((false, 0.0, string.Empty));

                }
                else
                {
                   DirectoryInfo ret=  Directory.CreateDirectory(directoryPath);
                    if(ret.Exists)
                    {
                        goto LabelDepart;
                    }
                    return Task.FromResult((false, 0.0, string.Empty));
                }
            }
            catch (Exception ex)
            {

                return Task.FromResult((false, 0.0, string.Empty));
            }
        }

        public static byte[] convertTobytearray(string uri)
        {
            return File.ReadAllBytes(uri);
        }


        static List<string> ListAllFiles(string directoryPath)
        {
            List<string> LstString = new List<string>();
            try
            {
                // Récupérez tous les fichiers dans le répertoire actuel
                string[] fichiers = Directory.GetFiles(directoryPath);
                foreach (string fichier in fichiers)
                {
                    LstString.Add(fichier);
                    ret.Add(fichier);
                }
                // Récupérez tous les sous-dossiers dans le répertoire actuel
                string[] sousDossiers = Directory.GetDirectories(directoryPath);
                foreach (string sousDossier in sousDossiers)
                {
                    // Récursion pour parcourir les sous-dossiers et leurs fichiers
                    ListAllFiles(sousDossier);
                }
                return LstString;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur dans la gestion des fichier " + ex.Message);
            }
        }


        //public string GetInfoFromUser(string info)
        //{
        //    try
        //    {
        //        NpgsqlConnection con = new NpgsqlConnection(); ;
        //        con.Open();
        //        NpgsqlCommand cmd = new NpgsqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandText = $"SELECT p1.* FROM sc_enrollement.t_info_personne as p1 inner join sc_enrollement.t_empreinte as p2 on p2.r_id_personne_fk=p1.r_id where p2.r_lien like '%{info}'";
        //        NpgsqlDataReader rd = cmd.ExecuteReader();

        //        if (!rd.HasRows)
        //        {
        //            MessageBox.Show("Empreinte existante mais données non trouvé");
        //        }
        //        else
        //        {
        //            rd.Read();
        //            string _cni = rd.GetString(1);
        //            string _numuniq = rd.GetString(2);
        //            string _nom = rd.GetString(3);
        //            string _prenom = rd.GetString(4); ;
        //            string _sex = rd.GetString(5); ;
        //            string _taile = rd.GetString(6); ;
        //            string _nationnalite = rd.GetString(7);
        //            string _lieunaissance = rd.GetString(8);
        //            DateTime _dateexpire = rd.GetDateTime(9);
        //            string _nni = rd.GetString(10);
        //            string _profession = rd.GetString(11);
        //            DateTime _dateemission = rd.GetDateTime(12);
        //            string _lieuemission = rd.GetString(13);
        //            DateTime _datenaissance = rd.GetDateTime(20);
        //            rd.Close();





        //            Thread thread = new Thread(() =>
        //            {
        //                // Effectuez votre travail long ici

        //                // Créez un délégué pour mettre à jour l'interface utilisateur avec des paramètres
        //                Action<string> updateDelegate = new Action<string>((message) =>
        //                {
        //                    // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
        //                    UpdateUIFromThread(_cni, _numuniq, _nom, _prenom, _sex, _taile, _nationnalite, _lieunaissance, _dateexpire, _nni, _profession, _dateemission, _lieuemission, _datenaissance);
        //                });

        //                // Appelez Invoke sur le formulaire pour mettre à jour l'interface utilisateur
        //                this.Invoke(updateDelegate, "Mise à jour depuis le thread secondaire avec des paramètres");
        //            });

        //            // Démarrez le thread
        //            thread.Start();

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        con.Close();
        //        MessageBox.Show("Erreur systeme veuillez contacter l'administrateur");
        //    }
        //    finally { /*con.Close();*/ }
        //}
    }
}
