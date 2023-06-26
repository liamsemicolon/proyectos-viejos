using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Un programa que pida al usuario 5 números reales (pista: necesitarás un array de "float") y luego los muestre en
   el orden contrario al que se introdujeron. */
namespace Arrays1
{
    class Program
    {
        static float PedirFloat(int posNum)
        {
            Console.Write("Ingrese el valor del número " + posNum + ":\n> ");
            return Convert.ToSingle(Console.ReadLine());
        }
        static void Main(string[] args)
        {
            float[] num = new float[5];
            int i = new int();
            for (i = 0; i < num.Length; i++)
            {
                num[i] = PedirFloat(i + 1);
            }
            for (i = (num.Length - 1); i >= 0; i--)
            {
                Console.WriteLine("Número " + (i+1) + ": " + num[i]);
            }
            Console.Write("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}