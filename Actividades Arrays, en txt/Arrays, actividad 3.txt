using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Un programa que almacene en una tabla el número de días que tiene cada mes (supondremos que es un año no
bisiesto), pida al usuario que le indique un mes (1=enero, 12=diciembre) y muestre en pantalla el número de días
que tiene ese mes. */
namespace Arrays3
{
    class Program
    {
        static int PedirInt()
        {
            Console.Write("Ingrese el número del mes del cual quiera saber los días:\n> ");
            return Convert.ToInt32(Console.ReadLine());
        }
        static void Main(string[] args)
        {
            int[] diasEnMes = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            string[] nombresMeses = { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };
            int mes = PedirInt();
            Console.Write("La cantidad de días en el mes " + mes + " (" + nombresMeses[mes - 1] + ") es de " + diasEnMes[mes - 1] + " días.\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
