using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Actividad: Un programa que pida al usuario 4 números, los memorice (utilizando un array), calcule su media aritmética y
   después muestre en pantalla la media y los datos tecleados.*/
namespace Arrays1
{
    class Program
    {
        static float PedirFloat(int posNum)
        {
            Console.Write("Ingrese el valor del número " + posNum +":\n> ");
            return Convert.ToSingle(Console.ReadLine());
        }
        static void Main(string[] args)
        {
            float[] num = new float[4];
            float promedio = new float();
            for(int i = 0; i < num.Length; i++)
            {
                num[i] = PedirFloat(i + 1);
                promedio += num[i];
            }
            promedio /= num.Length;
            Console.Write("El promedio de los números es: " + promedio + "\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
