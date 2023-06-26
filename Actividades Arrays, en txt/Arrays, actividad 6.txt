using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Crea una variante del ejemplo anterior (Ejemplo5) que pida al usuario el dato a buscar, avise si ese dato no
   aparece, y que diga cuántas veces se ha encontrado en caso contrario. */
namespace Arrays6
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] datos = { 10, 15, 12, 25, 12, 15, 45, 0, 0 };
            int capacidad = datos.Length, cantidad = 7, i = new int(), numABuscar = new int(), numEncontrado = new int();
            Console.Write("Ingrese el número a buscar en el arreglo:\n> ");
            numABuscar = Convert.ToInt16(Console.ReadLine());
            for(i = 0; i < cantidad; i++)
            {
                if (datos[i] == numABuscar)
                {
                    numEncontrado++;
                }
            }
            if (numEncontrado == 0)
            {
                Console.WriteLine("El número no ha sido encontrado en el arreglo.");
            } else if (numEncontrado == 1)
            {
                Console.WriteLine("El número ha sido encontrado en el arreglo una vez.");
            } else if (numEncontrado > 1)
            {
                Console.WriteLine("El número ha sido encontrado en el arreglo " + numEncontrado + " veces.");
            }
            Console.Write("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
