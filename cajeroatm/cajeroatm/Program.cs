using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cajeto_ATM
{
    class Program
    {
        static string[] nombres;
        static float[] saldo;
        static int menu;
        static bool mostrarestado = false, menub1 = true, menub2 = false, menub3 = false;

        static void Main(string[] args)
        {
            SeleccionadorDeMenu();

        }


        static void Menu1()
        {
            do
            {

                MostrarEstadoActual();

                Console.SetCursorPosition(20, 2);
                Console.WriteLine("Bienvenido al Cajero ATM");
                Console.SetCursorPosition(19, 3);
                Console.WriteLine("- - - - - - - - - - - - -");
                Console.SetCursorPosition(20, 5);
                Console.WriteLine("1. Crear cuenta");
                Console.SetCursorPosition(20, 15);
                Console.WriteLine("6. Salir");
                Console.SetCursorPosition(20, 17);
                Console.WriteLine("Escribe el numero de la opcion donde quiere ingresar: ");
                Console.SetCursorPosition(74, 17);
                menu = int.Parse(Console.ReadLine());

                Console.Clear();
            } while (menu != 1 && menu != 6);
            opciones();

        }

        static void Menu2()
        {
            do
            {
                MostrarEstadoActual();
                Console.SetCursorPosition(20, 2);
                Console.WriteLine("Bienvenido al Cajero ATM");
                Console.SetCursorPosition(19, 3);
                Console.WriteLine("- - - - - - - - - - - - -");
                Console.SetCursorPosition(20, 5);
                Console.WriteLine("2. Cargar cuenta de terceros");
                Console.SetCursorPosition(20, 7);
                Console.WriteLine("3. Deposito");
                Console.SetCursorPosition(20, 9);
                Console.WriteLine("4. Extraccion");
                Console.SetCursorPosition(20, 15);
                Console.WriteLine("6. Salir");
                Console.SetCursorPosition(40, 15);
                EstasSeguro();
                Console.SetCursorPosition(20, 17);
                Console.WriteLine("Escribe el numero de la opcion donde quiere ingresar: ");
                Console.SetCursorPosition(74, 17);
                menu = int.Parse(Console.ReadLine());
                Console.Clear();

            } while (menu <= 1 || menu > 6 || menu == 5);
            opciones();
        }

        static void Menu3()
        {
            do
            {
                MostrarEstadoActual();
                Console.SetCursorPosition(20, 2);
                Console.WriteLine("Bienvenido al Cajero ATM");
                Console.SetCursorPosition(19, 3);
                Console.WriteLine("- - - - - - - - - - - - -");
                Console.SetCursorPosition(20, 9);
                Console.WriteLine("3. Deposito");
                Console.SetCursorPosition(20, 11);
                Console.WriteLine("4. Extraccion");
                Console.SetCursorPosition(20, 13);
                Console.WriteLine("5. Transferencia");
                Console.SetCursorPosition(20, 15);
                Console.WriteLine("6. Salir");
                Console.SetCursorPosition(40, 15);
                EstasSeguro();
                Console.SetCursorPosition(20, 17);
                Console.WriteLine("Escribe el numero de la opcion donde quiere ingresar: ");
                Console.SetCursorPosition(74, 17);
                menu = int.Parse(Console.ReadLine());
                Console.Clear();

            } while (menu <= 2 || menu > 6);
            opciones();
        }
        static void SeleccionadorDeMenu()
        {
            if (menub1)
            {
                Menu1();
            }
            if (menub2)
            {
                Menu2();
            }
            if (menub3)
            {
                Menu3();
            }

        }
        static void opciones()
        {


            switch (menu)
            {
                case 1:
                    MostrarCrearCuenta();
                    mostrarestado = true;
                    menub1 = false;
                    menub2 = true;
                    SeleccionadorDeMenu();
                    break;
                case 2:
                    MostrarCrearCuentaTercero();
                    menub2 = false;
                    menub3 = true;
                    SeleccionadorDeMenu();
                    break;
                case 3:
                    Deposito();
                    SeleccionadorDeMenu();
                    break;
                case 4:
                    OperacionExtraccion();
                    SeleccionadorDeMenu();
                    break;
                case 5:
                    MostrarTransferencia();
                    SeleccionadorDeMenu();
                    break;
                case 6:

                    break;
            }



        }


        static void CrearCuentaNombre()
        {

            nombres = new string[2];
            Console.SetCursorPosition(20, 2);
            Console.WriteLine("Introduzca tu nombre de la cuenta:");
            Console.SetCursorPosition(55, 2);
            nombres[0] = Console.ReadLine();

        }
        static void CrearCuentaSaldo()
        {
            saldo = new float[2];
            Console.SetCursorPosition(20, 4);
            Console.WriteLine("Introduzca tu saldo:");
            Console.SetCursorPosition(40, 4);
            float dinero = float.Parse(Console.ReadLine());
            saldo[0] = dinero;

        }
        static void MostrarCrearCuenta()
        {
            CrearCuentaNombre();
            CrearCuentaSaldo();
            mostrarestado = true;
            menub1 = false;
            Console.Clear();
        }



        static void CrearTercerosNombre()
        {
            Console.SetCursorPosition(20, 2);
            Console.WriteLine("Introduzca el nombre de la cuenta del tercero:");
            Console.SetCursorPosition(66, 2);
            nombres[1] = Console.ReadLine();
        }

        static void CrearTercerosSaldo()
        {
            Console.SetCursorPosition(20, 4);
            Console.WriteLine("Introduzca el saldo que tendra:");
            Console.SetCursorPosition(51, 4);
            float dinero = float.Parse(Console.ReadLine());
            saldo[1] = dinero;
        }

        static void MostrarCrearCuentaTercero()
        {
            CrearTercerosNombre();
            CrearTercerosSaldo();

            Console.Clear();
        }

        static void Deposito()
        {
            Console.SetCursorPosition(20, 2);
            Console.WriteLine("Introduzca el importe a depositar:");
            Console.SetCursorPosition(54, 2);
            float dinero = float.Parse(Console.ReadLine());
            if (dinero <= 0)
            {
                Console.SetCursorPosition(20, 6);
                Console.WriteLine("no se pueden colocar importes negativos o cero");
                Console.ReadKey();
                Console.Clear();
                opciones();
            }
            saldo[0] = saldo[0] + dinero;
            Console.SetCursorPosition(20, 4);
            Console.WriteLine("Tu operacion se ha realizado con exito, Tu saldo actual es de: {0}", saldo[0]);
            Console.ReadKey();
            Console.Clear();
        }

        static float Extraccion()
        {

            Console.SetCursorPosition(20, 2);
            Console.WriteLine("Introduzca el importe a extraer:");
            Console.SetCursorPosition(52, 2);
            float dinero = float.Parse(Console.ReadLine());
            if (dinero <= 0)
            {
                Console.SetCursorPosition(20, 6);
                Console.WriteLine("no se pueden colocar importes negativos o cero");
                Console.ReadKey();
                Console.Clear();
                opciones();
            }
            return dinero;
        }

        static void OperacionExtraccion()
        {
            float dinero = Extraccion();
            if (dinero > saldo[0])
            {
                Console.SetCursorPosition(20, 4);
                Console.WriteLine("Tu saldo es insuficiente para realizar esta operacion");
                Console.ReadKey();
                Console.Clear();
                opciones();
            }
            else
            {
                saldo[0] = saldo[0] - dinero;
                Console.SetCursorPosition(20, 4);
                Console.WriteLine("Tu operacion se ha realizado con exito, Tu saldo actual es de: {0}", saldo[0]);
                Console.ReadKey();
                Console.Clear();
            }
        }
        static string PedirNombreParaTransferir()
        {
            Console.SetCursorPosition(20, 2);
            Console.WriteLine("Instroduzca el nombre de la cuenta a transferir:");
            Console.SetCursorPosition(68, 2);
            string nombre = Console.ReadLine();
            return nombre;
        }

        static void MostrarTransferencia()
        {
            string nombretwo = PedirNombreParaTransferir();
            string nombretercero = nombres[1];
            bool comparar = nombretercero.Equals(nombretwo);

            if (comparar)
            {
                OperacionTransferencia();
            }
            else
            {
                Error();
            }

        }

        static float Transferencia()
        {
            Console.SetCursorPosition(20, 4);
            Console.WriteLine("Introduzca el importe a transferir:");
            Console.SetCursorPosition(56, 4);
            float importe = float.Parse(Console.ReadLine());
            if (importe <= 0)
            {
                Console.SetCursorPosition(20, 6);
                Console.WriteLine("no se pueden colocar importes negativos o cero");
                Console.ReadKey();
                opciones();
                return importe;

            }
            else
            {
                return importe;
            }
        }

        static void OperacionTransferencia()
        {
            float importe = Transferencia();

            bool comparar = false;

            if (saldo[0] >= importe)
            {
                comparar = true;
            }
            if (comparar)
            {
                saldo[0] = saldo[0] - importe;
                saldo[1] = saldo[1] + importe;
                Console.SetCursorPosition(20, 6);
                Console.WriteLine("Tu operacion se ha realizado con exito, el saldo actual de " + nombres[1] + " y su saldo actual es de: " + saldo[1]);
                Console.SetCursorPosition(20, 8);
                Console.WriteLine("Tu saldo actual es de: {0}", saldo[0]);
                Console.ReadKey();
                Console.Clear();

            }
            else
            {
                Error();
            }

        }
        static void Error()
        {
            Console.Clear();
            Console.SetCursorPosition(20, 2);
            Console.WriteLine("No se pudo realizar la operacion con exito");
            Console.ReadKey();
            Console.Clear();
        }



        static void EstadoActual()
        {
            Console.SetCursorPosition(20, 20);
            Console.WriteLine("Señor/ra {0}, Su Saldo Actual es de: {1}", nombres[0], saldo[0]);

        }

        static void MostrarEstadoActual()
        {
            if (mostrarestado)
            {
                EstadoActual();

            }
            else
            {

            }
        }
        static void EstasSeguro()
        {
            if (menu == 6)
            {
                Console.WriteLine("Si estas seguro, presione otra vez 6");
            }

        }
    }
}
