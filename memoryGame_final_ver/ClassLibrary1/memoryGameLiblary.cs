using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace ClassLibrary
{
    public class Game
    {

        //tablica z numerami obrazkow
        public static int[] board = new int[12] { 1, 2, 3, 4, 5, 6, 6, 5, 4, 3, 2, 1 };
        public static string ImageDictionary(string btnName)
        {
            //Słownik zawierające nazwy obrazków ktore sa kluczowe w dzialaniu algorytmu

            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                { "B1", "A1" },
                { "B2", "A2" },
                { "B3", "A3" },
                { "B4", "A4" },
                { "B5", "A5" },
                { "B6", "A6" },
                { "B7", "A7" },
                { "B8", "A8" },
                { "B9", "A9" },
                { "B10", "A10" },
                { "B11", "A11" },
                { "B12", "A12" }
            };

            if (dictionary.ContainsKey(btnName))
            {
                string value = dictionary[btnName];
                return value;
            }

            return "";
        }
    }
}
