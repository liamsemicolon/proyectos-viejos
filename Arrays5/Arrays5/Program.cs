using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Crea un programa que pregunte al usuario cuántos números enteros va a introducir (por ejemplo, 10), le pida
todos esos números, los guarde en un array y finalmente calcule y muestre la media de esos números. */
namespace Arrays5
{
    class Program
    {
        static int PedirInt(int posNum)
        {
            Console.Write("Ingrese el valor del número " + posNum + ":\n> ");
            return Convert.ToInt32(Console.ReadLine());
        }
        static void Main(string[] args)
        {
            int i = new int(), cantNum = new int();
            float media = new float();
            Console.Write("Ingrese la cantidad de números que quiere introducir:\n> ");
            cantNum = Convert.ToInt32(Console.ReadLine());
            int[] arrayNum = new int[cantNum];
            for (i = 0; i < cantNum; i++)
            {
                arrayNum[i] = PedirInt(i + 1);
                media += arrayNum[i];
            }
            media /= cantNum;
            Console.Write("La media es de " + media + ".\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
