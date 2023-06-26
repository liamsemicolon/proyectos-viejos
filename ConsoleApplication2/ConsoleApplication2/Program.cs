using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static float PedirNum(int i, int j)
        {
            float numAPedir = new float();
            bool esValido = new bool();
            do
            {
                Console.Write("Ingrese el valor {0} del conjunto {1}\n> ", i, j);
                try
                {
                    numAPedir = Convert.ToSingle(Console.ReadLine());
                    esValido = true;
                } catch
                {
                    esValido = false;
                    Console.Write("Entrada inválida.\nPresione cualquier tecla para intentar de nuevo...");
                    Console.ReadKey();
                }
            } while (!esValido);
            return numAPedir;
        }

        static float PromedioConjunto(float aCalcular)
        {
            return aCalcular / 3;
        }

        static float PromedioTotal(float aCalcular)
        {
            return aCalcular / 9;
        }

        static void ImprimirValores(float[] af, float f)
        {
            int i = new int();
            for (i = 0; i < 3; i++)
            {
                Console.WriteLine("Promedio del conjunto {0}: {1}", i+1, af[i]);
            }
            Console.WriteLine("Promedio total: {0}", f);
        }

        static void Main(string[] args)
        {
            float[,] matriz = new float[3, 3];
            float[] suma = new float[3];
            float sumaTotal = new float();
            int i = new int(), j = new int();

            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    matriz[i, j] = PedirNum(j + 1, i + 1);
                    suma[i] += matriz[i, j];
                }
                sumaTotal += suma[i];
            }

            for (i = 0; i < 3; i++)
            {
                suma[i] = PromedioConjunto(suma[i]);
            }

            sumaTotal = PromedioTotal(sumaTotal);

            ImprimirValores(suma, sumaTotal);
        }
    }
}
