using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calctiempesp
{
    class Program
    {
        static float PedirFloat (string nom)
        {
            Console.Write("Ingrese el valor del tiempo " + nom + ":\n> ");
            return Convert.ToSingle(Console.ReadLine());
        }
        static void Main(string[] args)
        {
            float a, b, c, te;
            a = PedirFloat("optimista");
            b = PedirFloat("más probable");
            c = PedirFloat("pesimista");

            te = (a + (4 * b) + c) / 6;

            Console.Write("El tiempo esperado es: " + te + " o " + (te*60) + " minutos.\nToque cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
