using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Crea un programa que prepare espacio para un máximo de 10 nombres. Deberá mostrar al usuario un menú
   que le permita realizar las siguientes operaciones:
        · Añadir un dato al final de los ya existentes.
        · Insertar un dato en una cierta posición (como ya se ha comentado, los que queden detrás deberán
          desplazarse "a la derecha" para dejarle hueco; por ejemplo, si el array contiene "hola”, “adiós" y se pide
          insertar "bien" en la segunda posición, el array pasará a contener "hola”, “bien", "adiós".
        · Borrar el dato que hay en una cierta posición (como se ha visto, lo que estaban detrás deberán desplazarse
          "a la izquierda" para que no haya huecos; por ejemplo, si el array contiene "hola”, “bien", "adiós" y se
          pide borrar el dato de la segunda posición, el array pasará a contener "hola", "adiós".
        · Mostrar los datos que contiene el array.
        · Salir del programa. */
namespace Arrays10
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] nombres = { "Mario", "Juan", "María", "Liam", "Josué", "Ariel", string.Empty, string.Empty, string.Empty, string.Empty };
            string nomAAñadir = string.Empty;
            int cantidad = 6, i = new int(), posNom = new int();
            bool salir = new bool();
            do
            {
                Console.Write("Elija que quiere hacer:\n\t1) Añadir un nombre al final de la lista\n\t2) Insertar un nombre en una posición específica\n\t3) Borrar un nombre\n\t4) Mostrar nombres\n\t5) Salir\n> ");
                int elecUsuario = Convert.ToInt16(Console.ReadLine());
                switch (elecUsuario)
                {
                    case 1:
                        salir = false;
                        if (cantidad < nombres.Length)
                        {
                            Console.Clear();
                            Console.Write("Ingrese el nombre a añadir al final del arreglo:\n> ");
                            nomAAñadir = Console.ReadLine();
                            nombres[cantidad] = nomAAñadir;
                            cantidad++;
                            Console.WriteLine("Nombre '" + nomAAñadir + "' añadido al final del arreglo.");
                        } else
                        {
                            Console.WriteLine("La cantidad de nombres es la máxima. Borre un nombre e intente de nuevo.");
                        }
                        break;
                    case 2:
                        salir = false;
                        if (cantidad < nombres.Length) {
                            Console.Clear();
                            Console.Write("Ingrese el nombre a añadir:\n> ");
                            nomAAñadir = Console.ReadLine();
                            Console.Write("Ingrese la posición del nombre a añadir:\n> ");
                            posNom = (Convert.ToInt16(Console.ReadLine()) - 1);
                            if (posNom > cantidad)
                            {
                                Console.WriteLine("La posición ingresada no es válida. Intente de nuevo...");
                            }
                            else
                            {
                                if (posNom >= nombres.Length)
                                {
                                    Console.WriteLine("La posición ingresada no es válida. Intente de nuevo...");
                                }
                                else
                                {
                                    if (posNom <= cantidad)
                                    {
                                        for (i = cantidad; i > posNom; i--)
                                        {
                                            nombres[i] = nombres[i - 1];
                                        }
                                        nombres[posNom] = nomAAñadir;
                                        cantidad++;
                                    }
                                    else if (posNom > cantidad && posNom < nombres.Length)
                                    {
                                        nombres[posNom] = nomAAñadir;
                                        cantidad += (nombres.Length - cantidad);
                                    }
                                    Console.WriteLine("Nombre '" + nomAAñadir + "' añadido en la posición " + (posNom + 1) + ".");
                                }
                            }
                            } else {
                                Console.WriteLine("La cantidad de nombres es la máxima. Borre un nombre e intente de nuevo.");
                            }
                        break;
                    case 3:
                        salir = false;
                        Console.Clear();
                        Console.Write("Ingrese la posición del número a borrar:\n> ");
                        posNom = (Convert.ToInt16(Console.ReadLine()) - 1);
                        if (posNom >= cantidad)
                        {
                            Console.WriteLine("La posición ingresada no es válida. Intente de nuevo...");
                        }
                        else
                        {
                            for (i = posNom; i < cantidad; i++)
                            {
                                nombres[i] = nombres[i + 1];
                            }
                            cantidad--;
                            nombres[cantidad] = string.Empty;
                            Console.WriteLine("Nombre borrado de la posición " + (posNom + 1) + ".");
                        }
                        break;
                    case 4:
                        salir = false;
                        Console.Clear();
                        Console.WriteLine("Nombres:");
                        for (i = 0; i < cantidad; i++)
                        {
                            if (nombres[i] != string.Empty)
                            {
                                Console.WriteLine((i + 1) + ") " + nombres[i]);
                            }
                        }
                        break;
                    case 5:
                        salir = true;
                        break;
                    default:
                        salir = false;
                        Console.WriteLine("Entrada no válida.");
                        break;
                }
                Console.Write("Presione cualquier tecla para salir...");
                Console.ReadKey();
                Console.Clear();
            } while (!salir);
        }
    }
}
