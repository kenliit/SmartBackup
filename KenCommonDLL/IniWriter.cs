using System;
using System.Runtime.InteropServices;

namespace KenCommonDLL
{
	/// <summary>
	/// Wrapper class for WritePrivateProfileString Win32 API function.
	/// </summary>
	public class IniWriter
	{
        [DllImport("kernel32")] 
        private static extern int WritePrivateProfileString(
                string iniSection, 
                string iniKey, 
                string iniValue, 
                string iniFilePath);		
        
        /// <summary>
        /// Adds to (or modifies) a value to an .ini file. If the file does not exist,
        /// it will be created.
        /// </summary>
        public static void WriteValue(string iniSection, 
                                     string iniKey, 
                                     string iniValue,
                                     string iniFilePath)
        {
            WritePrivateProfileString(iniSection, iniKey, iniValue, iniFilePath);
        }
        
	}
}
