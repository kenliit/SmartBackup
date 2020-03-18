using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Security.Cryptography;
using System.IO;

namespace KenCommonDLL
{
    public static class KenSystemTool
    {
        /// <summary>
        /// Get the sha256 hashstring of a string
        /// </summary>
        /// <param name="randomString"></param>
        /// <returns></returns>
        static string sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        /// <summary>
        /// Get the first Mac Address of a computer
        /// </summary>
        /// <returns></returns>
        public static string GetComputerFirstMacAddress()
        {
            String firstMacAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();

            return firstMacAddress;
        }

        /// <summary>
        /// Get Hard Drives Informations
        /// </summary>
        public class HardDrive
        {
            public string Model { get; set; }
            public string InterfaceType { get; set; }
            public string Caption { get; set; }
            public string SerialNo { get; set; }
        }

        public static List<HardDrive> GetAllDiskDrives()
        {
            List<HardDrive> hdCollection = new List<HardDrive>();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                HardDrive hd = new HardDrive();
                hd.Model = wmi_HD["Model"].ToString().Trim();
                hd.InterfaceType = wmi_HD["InterfaceType"].ToString().Trim();
                hd.Caption = wmi_HD["Caption"].ToString().Trim();

                hd.SerialNo = wmi_HD.GetPropertyValue("SerialNumber").ToString().Trim();//get the serailNumber of diskdrive

                hdCollection.Add(hd);
            }

            return hdCollection;
        }

        public static string GetFirstDiskDriveSN()
        {
            List<HardDrive> hdCollection = GetAllDiskDrives();
            return hdCollection.Count > 0 ? hdCollection.FirstOrDefault().SerialNo : "";
        }

    }
}
