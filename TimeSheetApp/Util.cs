using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace TimeSheetApp
{
    static class Util
    {
     
        /// <summary>
        /// Method to write data to text file
        /// </summary>
        /// <param name="line">data</param>
        /// <param name="filepath">file path</param>
        public static void WriteFile(string line, string filepath)
        {
            using (StreamWriter w = File.AppendText(filepath))
            {
                w.AutoFlush = true;               
                w.WriteLine(line);
                w.Dispose();
                w.Close();
            }
        }     

        /// <summary>
        /// Method to accept only double value user input
        /// </summary>
        /// <param name="msg">message to display if invalid</param>
        /// <returns> double </returns>
        public static double ReadDouble(string msg)
        {
            double result;
            while (!double.TryParse(Console.ReadLine(), out result)) 
            {
                Console.WriteLine(msg);
            }
           
            return result;
        }

        /// <summary>
        /// Method to accept only correct date time user input
        /// </summary>
        /// <param name="msg">message to display if invlid</param>
        /// <returns> Date time</returns>
        public static DateTime ReadDateTime(string msg)
        {

            DateTime result;
            string[] formats = new[] { "dd/MM/yyyy", "d/MM/yyyy", "d/M/yyyy", "dd/M/yyyy" };
            while (!DateTime.TryParseExact(Console.ReadLine(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result)) 
            {
                Console.WriteLine(msg);
            }
           
            return result;
        }


      
    }
}
