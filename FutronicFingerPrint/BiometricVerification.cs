using Futronic.Models;
using Futronic.SDKHelper;
using SourceAFIS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FutronicFingerPrint
{
   public static class BiometricVerification
   {
        public static Task<(bool, double)> Verify(Image fingerprint1, Image fingerprint2)
        {
            var verificationOptions = new FingerprintImageOptions
            {
                Dpi = 500
            };

            ////////////**************************************************IMAGE 1 *******************************************************
            MemoryStream stream = new MemoryStream();
            fingerprint1.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp); 
            byte[] imageBytesreceive = stream.ToArray();
            ////////////**************************************************IMAGE 1 *******************************************************
            ////////////**************************************************IMAGE 1 *******************************************************
            MemoryStream stream1 = new MemoryStream();
            fingerprint2.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] imageBytesget = stream.ToArray();
            ////////////**************************************************IMAGE 1 *******************************************************

            var probe = new FingerprintTemplate(new FingerprintImage(imageBytesreceive));
            var candidate = new FingerprintTemplate(new FingerprintImage(imageBytesget));


            double score = new FingerprintMatcher(probe).Match(candidate);

            double threshold = 40;
            bool match = score >= threshold;
            return Task.FromResult((match, score));
        }
   }
}
