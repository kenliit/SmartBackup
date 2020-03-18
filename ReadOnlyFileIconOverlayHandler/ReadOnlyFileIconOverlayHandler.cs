using Microsoft.WindowsAPICodePack.Shell;
using SharpShell.Interop;
using SharpShell.SharpIconOverlayHandler;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace ReadOnlyFileIconOverlayHandler
{
    [ComVisible(true)]
    public class ReadOnlyFileIconOverlayHandler : SharpIconOverlayHandler
    {
        /// <summary>
        /// Every time a shell item will be displayed, this function will be called. 
        /// The path to the shell item is provided in the path parameter.
        /// Return true if you want the icon overlay to be shown for the specified file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="attributes">FILE_ATTRIBUTE flags</param>
        /// <returns></returns>
        protected override bool CanShowOverlay(string path, FILE_ATTRIBUTE attributes)
        {
            try
            {
                //  Get the file attributes
                //var fileAttributes = new FileInfo(path);

                var file = ShellFile.FromFilePath(path);
                var status = file.Properties.System.ContentStatus.ToString();

                if (status == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

                //  Return true if the file is read only, meaning we'll show the overlay
                //return fileAttributes.IsReadOnly;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// You know what it is :)
        /// </summary>
        /// <returns></returns>
        protected override Icon GetOverlayIcon()
        {
            return MyResource.ReadOnly;
        }

        /// <summary>
        /// This function must return the priority of the handler. The priority is used when 
        /// there are two icon overlays for a single file - the shell will use the icon with 
        /// the highest priority. The highest priority is 0 - the lowest priority is 100.
        /// </summary>
        /// <returns></returns>
        protected override int GetPriority()
        {
            return 90;
        }
    }
}
