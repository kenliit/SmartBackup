using DSOFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenCommonDLL
{
    public static class DsoWrapper
    {
        /// <summary>
        /// A property name that this sample code uses to store tag information.
        /// </summary>
        private static string FILE_PROPERTY = "SmartBackupFileTag";

        /// <summary>
        /// Gets value stored in a custom tag
        /// </summary>
        /// <param name="filename">Path to the file</param>
        /// <returns>Our custom value stored in the custom file tag</returns>
        public static string GetCustomPropertyValue(string filename)
        {
            string comment = string.Empty;
            OleDocumentProperties file = new DSOFile.OleDocumentProperties();

            try
            {
                // Open file
                file.Open(filename, false, DSOFile.dsoFileOpenOptions.dsoOptionDefault);
                comment = GetTagField(file);
            }
            catch (Exception)
            {
                // Handle errors here
            }
            finally
            {
                if (file != null) file.Close(); // Clean up
            }

            return comment;
        }

        /// <summary>
        /// Sets value stored in a files custom tag
        /// </summary>
        /// <param name="filename">Path to the file</param>
        /// <param name="value">Value to store in the custom file tag</param>
        public static void SetCustomPropertyValue(string filename, string value)
        {
            OleDocumentProperties file = new DSOFile.OleDocumentProperties();

            try
            {
                file.Open(filename, false, DSOFile.dsoFileOpenOptions.dsoOptionDefault);
                SetTagField(file, value);
            }
            catch (Exception)
            {
                // Handle errors here
            }
            finally // Always called, even when throwing in Exception block
            {
                if (file != null) file.Close(); // Clean up
            }
        }

        /// <summary>
        /// Gets the value of the file tag property
        /// </summary>
        /// <param name="file">Ole Document File</param>
        /// <returns>Contents of the file tag property. Can be null or empty.</returns>
        private static string GetTagField(DSOFile.OleDocumentProperties file)
        {
            string result = string.Empty;
            foreach (DSOFile.CustomProperty property in file.CustomProperties)
            {
                if (property.Name == FILE_PROPERTY) // Check property exists
                {
                    result = property.get_Value();
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Sets the value of the file tag property
        /// </summary>
        /// <param name="file">Ole Document File</param>
        /// <param name="value">Value to set as the property value</param>
        /// <param name="saveDocument">Saves document if set to true</param>
        /// <param name="closeDocument">Closes the document if set to true</param>
        private static void SetTagField(DSOFile.OleDocumentProperties file, string value, bool saveDocument = true, bool closeDocument = true)
        {
            bool found = false;
            foreach (DSOFile.CustomProperty property in file.CustomProperties)
            {
                if (property.Name == FILE_PROPERTY) // Check property exists
                {
                    property.set_Value(value);
                    found = true;
                    break;
                }
            }

            if (!found)
                file.CustomProperties.Add(FILE_PROPERTY, value);

            if (saveDocument)
                file.Save();

            if (closeDocument)
                file.Close();
        }
    }
}
