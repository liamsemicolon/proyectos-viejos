/* este borrador solo implementa dos de las funciones principales:
        - añadir una reunión a la lista de reuniones semanales
        - revisar la lista de reuniones semanales
        */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;      // permite usar el tipo de dato DataTable
using LumenWorks.Framework.IO.Csv; // librería externa, permite leer archivos .csv

namespace MacronBorrador
{
    public static class ConversorDTaCSV {
        public static void ACSV(this DataTable dtTabla, string nombre)
        {
            StreamWriter sw = new StreamWriter(nombre + ".csv", false);   
            for (int i = 0; i < dtTabla.Columns.Count; i++)
            {
                sw.Write(dtTabla.Columns[i]);
                if (i < dtTabla.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtTabla.Rows)
            {
                for (int i = 0; i < dtTabla.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtTabla.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    };

    class Program
    {
        static DataTable tablaSemanal = new DataTable();

        static void CrearTabla(string nombreArchivo)
        {

            bool checkeoTablaEnArchivo = CheckeoDeArchivo(nombreArchivo + ".csv");

            if (checkeoTablaEnArchivo == false)
            {
                tablaSemanal.Columns.Add("Materia", typeof(string));
                tablaSemanal.Columns.Add("Día", typeof(string));
                tablaSemanal.Columns.Add("Comienzo", typeof(string));
                tablaSemanal.Columns.Add("Fin", typeof(string));
                tablaSemanal.Columns.Add("Descripción", typeof(string));           
            } else
            {
                tablaSemanal = LeerCSV(nombreArchivo);
            }
            tablaSemanal.ACSV(nombreArchivo);
        }

        static bool CheckeoDeArchivo (string nombreArchivo)
        {
            string direccionArchivo = Directory.GetCurrentDirectory();
            return File.Exists(direccionArchivo + @"\" + nombreArchivo);
        }

        static DataTable LeerCSV(string nombre)
        {
            DataTable tabla = new DataTable();
            using (CsvReader csvReader =
                new CsvReader(new StreamReader(nombre + ".csv"), true))
            {
                tabla.Load(csvReader);
            }
            return tabla;
        }

        static void ImprimirCabecera (string parametroCabecera)
        {
            Console.WriteLine("Macron | " + parametroCabecera + " | " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
        }

        static void ImprimirTabla (DataTable tabla) // imprime la tabla, con el espaciado correcto
        {
            Console.WriteLine();
            Dictionary<string, int> colWidths = new Dictionary<string, int>();

            foreach (DataColumn col in tabla.Columns)
            {
                Console.Write(col.ColumnName);
                var maxLabelSize = tabla.Rows.OfType<DataRow>()
                        .Select(m => (m.Field<object>(col.ColumnName)?.ToString() ?? "").Length)
                        .OrderByDescending(m => m).FirstOrDefault();

                colWidths.Add(col.ColumnName, maxLabelSize);
                for (int i = 0; i < maxLabelSize - col.ColumnName.Length + 10; i++) Console.Write(" ");
            }

            Console.WriteLine();

            foreach (DataRow dataRow in tabla.Rows)
            {
                for (int j = 0; j < dataRow.ItemArray.Length; j++)
                {
                    Console.Write(dataRow.ItemArray[j]);
                    for (int i = 0; i < colWidths[tabla.Columns[j].ColumnName] - dataRow.ItemArray[j].ToString().Length + 10; i++) Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        static void MenuPrincipal()
        {
            int entUsuario;
            bool salir = false;
            do
            {
                ImprimirCabecera("Menú principal");
                Console.Write("Opciones:\n      1. Revisar tabla semanal\n      2. Añadir a la tabla semanal\n      3. Salir\n> ");
                entUsuario = int.Parse(Console.ReadLine());
                if (entUsuario < 0 && entUsuario > 4)
                {
                    Console.WriteLine("Entrada no válida, intente de nuevo...");
                    Console.ReadKey();
                    salir = false;
                }
                else
                { 
                    switch (entUsuario)
                    {
                        case 1:
                            salir = RevisarTabla();
                            break;
                        case 2:
                            salir = AñadirATabla();
                            break;
                        case 3:
                            Console.Clear();
                            ProcesoSalida("salir");
                            salir = true;
                            break;
                    }
                }
            } while (salir == false);
        }

        static bool RevisarTabla ()
        {
            Console.Clear();
            ImprimirCabecera("Tabla semanal");
            ImprimirTabla(tablaSemanal);
            ProcesoSalida("volver al menú principal");
            return false;
        }

        static bool AñadirATabla()
        {
            Console.Clear();
            ImprimirCabecera("Edición de tabla");
            tablaSemanal.Rows.Add(PedirString("materia"), PedirString("día"), PedirString("hora de comienzo del evento"), PedirString("hora de fin del evento"), PedirString("descripción del evento (plataforma, link, etc.)"));
            ProcesoSalida("volver al menú principal");
            return false;
        }

        static string PedirString(string nombreString)
        {
            Console.Write("Ingrese " + nombreString + ":\n> ");
            return Console.ReadLine();
        }

        static void ProcesoSalida(string accion)
        {
            Console.Write("Presione cualquier tecla para " + accion + "...");
            Console.ReadKey();
            Console.Clear();
        }

        static void Main(string[] args)
        {
            CrearTabla("TablaSemanal");
            MenuPrincipal();
        }
    }
}
