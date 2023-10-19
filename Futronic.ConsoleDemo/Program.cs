using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Futronic.Scanners.FS26X80;
using Newtonsoft.Json;
namespace Futronic.ConsoleDemo
{
    class Program
    {
        static string Main(string[] args)
        {

            if (args.Length == 0)
            {
                RetourDto rt = new RetourDto { code = "Err001", data = "Erreur aucun argument passé" };
                return JsonConvert.SerializeObject(rt);
            }

            Console.WriteLine("Starting reader...");

            var accessor = new DeviceAccessor();

            using (var device = accessor.AccessFingerprintDevice())
            {
                device.SwitchLedState(false, false);

                device.FingerDetected += (sender, eventArgs) =>
                {
                    Console.WriteLine("Finger Detected!");

                    device.SwitchLedState(true, false);
                    
                    // Save fingerprint to temporary folder
                    var fingerprint = device.ReadFingerprint();
                    //var tempFile = Guid.NewGuid().ToString();

                    var tempFile = "Empreinteretour";
                    var tmpBmpFile = Path.ChangeExtension(tempFile, "bmp");
                    string pathimage = Path.Combine(args[0].ToString(), tmpBmpFile);
                    fingerprint.Save(pathimage);
                    //args[0].ToString()
                    Console.WriteLine("Saving file " + tmpBmpFile);
                    
                    device.StopFingerDetection();
                    device.Dispose();
                    RetourDto rt = new RetourDto { code = "OK001", data = pathimage };
                    return JsonConvert.SerializeObject(rt);
                };
                //device.FingerReleased += (sender, eventArgs) =>
                //{
                //    Console.WriteLine("Finger Released!");

                //    device.SwitchLedState(false, true);
                //};

                Console.WriteLine("Fingerprint Device Opened");

                device.StartFingerDetection();
                device.SwitchLedState(false, true);

                Console.ReadLine();

                Console.WriteLine("Exiting...");

                device.SwitchLedState(false, false);
                RetourDto rt = new RetourDto { code = "Err001", data = "Erreur aucun argument passé" };
                return JsonConvert.SerializeObject(rt);
            }
        }
    }
}
