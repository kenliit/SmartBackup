using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:\Share\Phone.png";
            //DsoWrapper.SetCustomPropertyValue(filePath, "1");
            Console.WriteLine(DsoWrapper.GetCustomPropertyValue(filePath));
            Console.ReadLine();
        }
    }
}
