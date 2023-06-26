using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Crea una variante del ejemplo anterior (Ejemplo5) que inserte un dato introducido por el usuario en la posición
   que elija el usuario. Debe avisar si la posición escogida es incorrecta (porque esté más allá del final de los datos). */
namespace Arrays8
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] datos = { 10, 15, 12, 25, 12, 15, 45, 0, 0 };
            int capacidad = datos.Length, cantidad = 7, i = new int(), numAAñadir = new int(), posNumAAñadir = new int();
            bool posValida = new bool();
            Console.WriteLine("Números:");
            for (i = 0; i < cantidad; i++)
            {
                Console.WriteLine(datos[i]);
            }
            Console.Write("Ingrese el número a añadir:\n> ");
            numAAñadir = Convert.ToInt16(Console.ReadLine());
            do
            {
                Console.Write("Ingrese la posición del número a añadir:\n> ");
                posNumAAñadir = Convert.ToInt16(Console.ReadLine());
                if (posNumAAñadir >= capacidad)
                {
                    posValida = false;
                    Console.WriteLine("La posición ingresada no es válida. Intente de nuevo...");
                } else
                {
                    posValida = true;
                    if (posNumAAñadir <= cantidad)
                    {
                        for (i = cantidad; i > posNumAAñadir; i--)
                        {
                            datos[i] = datos[i - 1];
                        }
                        datos[posNumAAñadir] = numAAñadir;
                        cantidad++;
                    }
                    else if (posNumAAñadir > cantidad && posNumAAñadir < capacidad)
                    {
                        datos[posNumAAñadir] = numAAñadir;
                        cantidad += (capacidad - cantidad);
                    }
                    Console.WriteLine("Número " + numAAñadir + " añadido en la posición " + posNumAAñadir + ".\nNúmeros:");
                    for (i = 0; i < cantidad; i++)
                    {
                        Console.WriteLine(datos[i]);
                    }
                }
            } while (!posValida); 
            Console.Write("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
