using SixLabors.ImageSharp;
using SourceAFIS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing.Imaging;
using Image = System.Drawing.Image;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
//using Microsoft.Extensions.FileSystemGlobbing;

namespace FingerPrintEcranPrincipal
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
    }
}
