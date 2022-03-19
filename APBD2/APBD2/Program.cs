using APBD2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace APBD2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var path = args[0];

            //var fi = new FileInfo(path);
            FileInfo fi = new(path);

            var fileContent = new List<string>();

            using (StreamReader stream = new(fi.OpenRead()))
            {
                string line = null;

                while ((line = await stream.ReadLineAsync()) != null)
                {
                    fileContent.Add(line);
                }

                //stream.Dispose();
            }

            /*
             * foreach (var item in File.ReadLines(path)){} 
             */

            foreach (var item in fileContent)
            {
                Console.WriteLine(item);
            }

            //DateTime - typ danych dla daty

            // Parsowanie daty
            var date = DateTime.Parse("2021-03-18");
            Console.WriteLine(date);

            // Analogicznie co "", ale ładniejsze rozwiązanie w C#
            string wrt = string.Empty;

            // Sprawdzenie czy string dostarczony jako argument jest nullem lub jest pusty
            if (string.IsNullOrEmpty(wrt)) { }

            // Sprawdzenie czy string dostarczony jako argument jest nullem lub składa się tylko z białych znaków
            if (string.IsNullOrWhiteSpace(wrt)) { }
            
            var hashSet = new HashSet<Student>(new MyComparer());

            // hashSet.Add(obj) <- dodawanie elementu do HashSet

        }
    }
}
