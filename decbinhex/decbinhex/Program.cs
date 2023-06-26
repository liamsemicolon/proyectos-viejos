using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace decbinhex
{
    class Program
    {
        static void Main(string[] args)
        {
            string direccionArchivo = Directory.GetCurrentDirectory() + @"\palabra.txt";
            string palabra = "XXXARQXXXUIXTECXXXTUXRA";
            char[] carAContar = { 'X', 'A', 'R', 'U', 'T', 'Q', 'I', 'E', 'C' };
            string[] salida = new string[10];
            int i = new int();
            for (i = 0; i < 9; i++) {
                salida[i] = carAContar[i] + ": " + palabra.Count(f => (f == carAContar[i]));
            }
            salida[9] = "Caracter que mas se repite: " + carAContar[0];
            File.WriteAllLines(direccionArchivo, salida);
        }
    }
}
