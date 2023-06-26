using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace distanciafechas
{
    class Program
    {
        static void Main(string[] args)
        {
            string direccionArchivo = Directory.GetCurrentDirectory() + @"\fecha.txt";
            string[] texto = File.ReadAllLines(direccionArchivo);
            DateTime dt = DateTime.ParseExact(texto[0], "yyyyMMdd", CultureInfo.InvariantCulture);
            DateTime ahora = DateTime.Now;
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan span = ahora - dt;
            int years = (zeroTime + span).Year - 1;
            int months = (zeroTime + span).Month - 1;
            int days = (zeroTime + span).Day;
            string[] salida = { "A: " + years, "M: " + months, "D: " + days };
            File.WriteAllLines(direccionArchivo, salida);
        }
    }
}
