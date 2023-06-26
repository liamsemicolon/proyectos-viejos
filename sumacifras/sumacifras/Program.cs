using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumacifras
{
    class Program
    {
        static void Main(string[] args)
        {
            string direccionArchivoF = Directory.GetCurrentDirectory() + @"\numfinal.txt";
            string direccionArchivo = Directory.GetCurrentDirectory() + @"\num.txt";
            string[] num = File.ReadAllLines(direccionArchivo);
            string[] num2 = { sumacifras(num[0]), String.Empty };
            string[] numFinal = new string[2];
            if (Convert.ToInt16(num2[0]) >= 10) {
                num2[1] = sumacifras(num2[0]);
                numFinal[0] = num2[1];
            } else
            {
                numFinal[0] = num2[0];
            }
            File.WriteAllLines(direccionArchivoF, numFinal);
            File.WriteAllLines(direccionArchivo, num2);
        }

        static string sumacifras(string n) {
            int sum = 0;
            foreach (char c in n) {
                sum += Convert.ToInt16(Char.GetNumericValue(c));
            }
            return sum.ToString();
        }
    }
}
