/*  Escrito por Liam Ruiz Romero
    Última actualización: 2 de octubre de 2021  */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyInfITP12A
{
    class Program
    {
        static string nombreArchivo = "datos.txt";
        static string direccionArchivo = Directory.GetCurrentDirectory() + @"\" + nombreArchivo;
        static string[] nombresDatos = { "DNI", "Apellido", "Nombre", "Dirección", "Localidad", "Provincia", "Teléfono", "Dirección de e-mail" };
        static char delimitador = '|';

        static bool CheckeoDeArchivo(){
            bool ExisteArchivoConDatos = false;
            FileInfo fi = new FileInfo(direccionArchivo);
            if (File.Exists(direccionArchivo) && fi.Length != 0)
            {
                ExisteArchivoConDatos = true;
            } else
            {
                ExisteArchivoConDatos = false;
            }
            return ExisteArchivoConDatos;
        }

        static string PedirString(string nombreInt)
        {
            Console.Write("Ingrese " + nombreInt + " del alumno:\n> ");
            return Console.ReadLine();
        }

        static void ImprimirCabecera(string tituloCabecera)
        {
            Console.WriteLine("Trabajo Práctico 12-A | " + tituloCabecera + " | " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
        }

        static bool Alta()
        {
            Console.Clear();
            ImprimirCabecera("Ingresar datos de alumno");
            if (!CheckeoDeArchivo()) {
                IngresarDatosInicial();
            }
            else
            {
                bool DNIEnArchivo = false;
                string DNI = PedirString(nombresDatos[0]);
                foreach (string line in File.ReadLines(direccionArchivo))
                {
                    if (line.Contains(DNI))
                    {
                        Console.Write("El DNI ingresado ya existe en el archivo. Intente con otro.");
                        DNIEnArchivo = true;
                        Console.ReadKey();
                    }
                }
                if (!DNIEnArchivo)
                {
                    IngresarDatos(DNI);
                }
            }
            Console.Clear();
            return false;
        }

        static bool Baja()
        {
            Console.Clear();
            ImprimirCabecera("Eliminar datos de alumno");
            if (CheckeoDeArchivo())
            {
                int i = new int();
                string[] datos = File.ReadAllLines(direccionArchivo);
                string DNI = PedirString(nombresDatos[0]);
                bool DNIEnArchivo = false;
                foreach (string line in File.ReadLines(direccionArchivo))
                {
                    i++;
                    if (line.Contains(DNI))
                    {
                        DNIEnArchivo = true;
                        break;
                    }
                }
                if (DNIEnArchivo)
                {
                    datos[i - 1] = null;
                    datos = datos.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    File.WriteAllLines(direccionArchivo, datos);
                    Console.Write("Datos eliminados. Presione cualquier tecla para salir...");
                }
                else
                {
                    Console.Write("El DNI ingresado no se encuentra en el archivo. Intente con otro.");
                }
            }
            else
            {
                Console.Write("No hay datos en el archivo. Ingrese datos en el mismo e intente de nuevo.");
            }
            Console.ReadKey();
            Console.Clear();
            return false;
        }

        static bool Modif()
        {
            Console.Clear();
            ImprimirCabecera("Modificar datos de alumno");
            if (CheckeoDeArchivo())
            {
                int n = new int(), i = new int();
                string[] datos = File.ReadAllLines(direccionArchivo);
                string datosPersonales = string.Empty;
                string[] datosSeparados = new string[8];
                bool DNIEnArchivo = false;
                string DNI = PedirString(nombresDatos[0]);
                foreach (string line in File.ReadLines(direccionArchivo))
                {
                    i++;
                    if (line.Contains(DNI))
                    {
                        DNIEnArchivo = true;
                        break;
                    }
                }
                n = i - 1;
                if (DNIEnArchivo)
                {
                    datosPersonales = datos[n];
                    datosSeparados = datosPersonales.Split(delimitador);
                    Console.WriteLine("Elija el dato que quiera editar:");
                    for (i = 0; i < 8; i++)
                    {
                        Console.WriteLine("\t" + (i + 1) + ") " + nombresDatos[i]);
                    }
                    Console.Write("> ");
                    int elecUsuario = Convert.ToInt32(Console.ReadLine());
                    datosSeparados[elecUsuario - 1] = PedirString(nombresDatos[elecUsuario - 1]);
                    datosPersonales = string.Join("|", datosSeparados);
                    datos[n] = datosPersonales;
                    File.WriteAllLines(direccionArchivo, datos);
                    Console.WriteLine("Datos actualizados:");
                    for (i = 0; i < 8; i++)
                    {
                        Console.WriteLine(nombresDatos[i] + ": " + datosSeparados[i]);
                    }
                    Console.Write("Presione cualquier tecla para salir...");
                }
                else
                {
                    Console.Write("El DNI ingresado no se encuentra en el archivo. Intente con otro.");
                }
            }
            else
            {
                Console.Write("No hay datos en el archivo. Ingrese datos en el mismo e intente de nuevo.");
            }
            Console.ReadKey();
            Console.Clear();
            return false;
        }

        static bool Cons()
        {
            Console.Clear();
            ImprimirCabecera("Mostrar datos de alumno");
            if (CheckeoDeArchivo())
            {
                int i = new int();
                string[] datos = File.ReadAllLines(direccionArchivo);
                string datosPersonales = string.Empty;
                string[] datosSeparados = new string[8];
                string DNI = PedirString(nombresDatos[0]);
                bool DNIEnArchivo = false;
                foreach (string line in File.ReadLines(direccionArchivo))
                {
                    i++;
                    if (line.Contains(DNI))
                    {
                        DNIEnArchivo = true;
                        break;
                    }
                }
                if (DNIEnArchivo)
                {
                    datosPersonales = datos[i - 1];
                    datosSeparados = datosPersonales.Split(delimitador);
                    for (i = 0; i < 8; i++)
                    {
                        Console.WriteLine(nombresDatos[i] + ": " + datosSeparados[i]);
                    }
                } else
                {
                    Console.WriteLine("No se encontró el DNI ingresado en el archivo. Intente con otro.");
                }
                Console.Write("Presione cualquier tecla para salir...");
            }
            else
            {
                Console.Write("No hay datos en el archivo. Ingrese datos en el mismo e intente de nuevo.");
            }
            Console.ReadKey();
            Console.Clear();
            return false;
        }

        static void IngresarDatosInicial()
        {
            string[] datos = new string[8];
            StreamWriter sw = new StreamWriter(direccionArchivo, true);
            for (int i = 0; i < datos.Length; i++)
            {
                datos[i] = PedirString(nombresDatos[i]);
                sw.Write(datos[i]);
                if(i != 7)
                {
                    sw.Write("|");
                }
            }
            sw.Close();
        }

        static void IngresarDatos(string valorDNI)
        {
            string[] datos = new string[8];
            StreamWriter sw = new StreamWriter(direccionArchivo, true);
            sw.Write(Environment.NewLine + valorDNI + "|");
            for (int i = 1; i < datos.Length; i++)
            {
                datos[i] = PedirString(nombresDatos[i]);
                sw.Write(datos[i]);
                if (i != 7)
                {
                    sw.Write("|");
                }
            }
            sw.Close();
        }

        static void MenuPrincipal()
        {
            bool terminarAplicación = false;
            do
            {
                ImprimirCabecera("Menú principal");
                Console.Write("Bienvenido al sistema de datos de los alumnos, por favor elija qué quiere hacer:\n\t1) Ingresar datos de alumno\n\t2) Eliminar datos de alumno\n\t3) Modificar datos de alumno\n\t4) Mostrar datos de alumno\n\t5) Salir\n> ");
                int elecUsuario = Convert.ToInt32(Console.ReadLine());
                switch (elecUsuario){
                    case 1:
                        terminarAplicación = Alta();
                        break;
                    case 2:;
                        terminarAplicación = Baja();
                        break;
                    case 3:
                        terminarAplicación = Modif();
                        break;
                    case 4:
                        terminarAplicación = Cons();
                        break;
                    case 5:
                        terminarAplicación = true;
                        Console.Write("Presione cualquier tecla para salir...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            } while (!terminarAplicación);
        }

        static void Main(string[] args)
        {
            MenuPrincipal();
        }
    }
}
