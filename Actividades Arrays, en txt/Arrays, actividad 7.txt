using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Crea una variante del ejemplo anterior (Ejemplo5) que añada un dato introducido por el usuario al final de los
   datos existentes. */
namespace Arrays7
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] datos = { 10, 15, 12, 25, 12, 15, 45, 0, 0 };
            int capacidad = datos.Length, cantidad = 7, i = new int(), numAAñadir = new int();
            Console.WriteLine("Números:");
            for (i = 0; i < cantidad; i++)
            {
                Console.WriteLine(datos[i]);
            }
            Console.Write("Ingrese el número a añadir al final del arreglo:\n> ");
            numAAñadir = Convert.ToInt16(Console.ReadLine());
            if (cantidad < capacidad)
            {
                datos[cantidad] = numAAñadir;
                cantidad++;
            }
            Console.WriteLine("Número " + numAAñadir + " añadido al final del arreglo.\nNúmeros:");
            for (i = 0; i < cantidad; i++)
            {
                Console.WriteLine(datos[i]);
            }
            Console.Write("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
