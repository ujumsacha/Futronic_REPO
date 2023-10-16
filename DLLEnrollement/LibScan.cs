using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DLLEnrollement
{
    public class LibScan
    {
        /// <summary>
        /// Opens the device for reading
        /// </summary>
        /// <returns></returns>
        [DllImport("ftrScanAPI.dll")]
        public static extern IntPtr ftrScanOpenDevice();

        /// <summary>
        /// Returns the last error encountered reading from the device
        /// </summary>
        /// <returns></returns>
        [DllImport("ftrScanAPI.dll")]
        public static extern long GetLastError();

        /// <summary>
        /// Disposed the memory handle used to access the device
        /// </summary>
        /// <param name="ftrHandle"></param>
        [DllImport("ftrScanAPI.dll")]
        public static extern void ftrScanCloseDevice(IntPtr ftrHandle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftrHandle"></param>
        /// <param name="pFrameParameters"></param>
        /// <returns>Returns true if a finger has been placed on the device</returns>
        [DllImport("ftrScanAPI.dll")]
        public static extern bool ftrScanIsFingerPresent(IntPtr ftrHandle, out _FTRSCAN_FRAME_PARAMETERS pFrameParameters);

        [DllImport("ftrScanAPI.dll")]
        public static extern bool ftrScanSetDiodesStatus(IntPtr ftrHandle, byte byGreenDiodeStatus, byte byRedDiodeStatus);

        [DllImport("ftrScanAPI.dll")]
        public static extern bool ftrScanGetDiodesStatus(IntPtr ftrHandle, out bool pbIsGreenDiodeOn, out bool pbIsRedDiodeOn);

        [DllImport("ftrScanAPI.dll")]
        public static extern bool ftrScanGetImageSize(IntPtr ftrHandle, out _FTRSCAN_IMAGE_SIZE pImageSize);

        /// <summary>
        /// Reads the stream of data from the device and writes the output into the <paramref name="pBuffer"/>
        /// </summary>
        /// <param name="ftrHandle"></param>
        /// <param name="nDose"></param>
        /// <param name="pBuffer">The byte array which the image is to be written to</param>
        /// <returns></returns>
        [DllImport("ftrScanAPI.dll")]
        public static extern bool ftrScanGetImage(IntPtr ftrHandle, int nDose, byte[] pBuffer);


        public struct _FTRSCAN_FAKE_REPLICA_PARAMETERS
        {
            public bool bCalculated;
            public int nCalculatedSum1;
            public int nCalculatedSumFuzzy;
            public int nCalculatedSumEmpty;
            public int nCalculatedSum2;
            public double dblCalculatedTremor;
            public double dblCalculatedValue;
        }

        public struct _FTRSCAN_FRAME_PARAMETERS
        {
            public int nContrastOnDose2;
            public int nContrastOnDose4;
            public int nDose;
            public int nBrightnessOnDose1;
            public int nBrightnessOnDose2;
            public int nBrightnessOnDose3;
            public int nBrightnessOnDose4;
            public _FTRSCAN_FAKE_REPLICA_PARAMETERS FakeReplicaParams;
            public _FTRSCAN_FAKE_REPLICA_PARAMETERS Reserved;
        }

        public struct _FTRSCAN_IMAGE_SIZE
        {
            public int nWidth;
            public int nHeight;
            public int nImageSize;
        }



        //******************************************************Faire un test ***************************************************************
        public static Bitmap ReadFingerprint(IntPtr ftrHandle)
        {
            int NDose = 4;
            var t = new _FTRSCAN_IMAGE_SIZE();
            ftrScanGetImageSize(ftrHandle, out t);

            byte[] arr = new byte[t.nImageSize];
            ftrScanGetImage(ftrHandle, NDose, arr);

            var width = t.nWidth;
            var height = t.nHeight;

            var image = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte a = (byte)(0xFF - arr[(y * width) + x]);

                    image.SetPixel(x, y, Color.FromArgb(a, a, a));
                }
            }

            return image;
        }
        //******************************************************Faire un test ***************************************************************

    }
}
