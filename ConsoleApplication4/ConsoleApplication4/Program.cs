using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Refugio
    {
        string nombre;
        List<Perro> perros;

        public Refugio(string nombre, List<Perro> perros)
        {
            this.nombre = nombre;
            this.perros = perros;
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void AddPerro(Perro p)
        {
            this.perros.Add(p);
        }

        public List<Perro> getPerros()
        {
            return this.perros;
        }
    }

    class Perro
    {
        string nombre, raza;

        public Perro(string nombre, string raza)
        {
            this.nombre = nombre;
            this.raza = raza;
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public string getRaza()
        {
            return this.raza;
        }
    }
    class Program
    {

        static Perro IngresarPerro()
        {
            string[] nombresValores = { "el nombre", "la raza" }, valores = new string[2];
            int i = new int();
            string entrada;
            for (i = 0; i < 2; i++)
            {
                entrada = PedirString(nombresValores[i] + " del perro");
                valores[i] = entrada;
            }
            Perro p = new Perro(valores[0], valores[1]);
            return p;
        }
        static int Menu()
        {
            int elec;
            Console.Write("1. Cargar refugio\n2. Cargar perro\n3. Ver\n4. Salir");
            elec = PedirNum(4);
            return elec;
        }

        static string PedirString(string nomValor)
        {
            string entrada;
            do
            {
                Console.Write("Ingrese {0}\n> ", nomValor);
                entrada = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(entrada));
            return entrada;
        }

        static int PedirNum(int tope)
        {
            int num = 0;
            bool esValido = new bool();
            do
            {
                try
                {
                    Console.Write("\n> ");
                    num = int.Parse(Console.ReadLine());

                    if (num < 0 || num > tope)
                    {
                        Console.Write("Número inválido.\nPresione cualquier tecla para ingresar uno nuevamente...");
                        Console.ReadKey();
                        esValido = false;
                    }
                    else
                    {
                        esValido = true;
                        Console.Clear();
                    }
                }
                catch
                {
                    Console.Write("Entrada inválida. Por favor ingrese un número.\nPresione cualquier tecla para intentar de nuevo...");
                    Console.ReadKey();
                }
            } while (!esValido);
            return num;
        }

        static Refugio CargarRefugio()
        {
            string nombre = PedirString("nombre del refugio");
            List<Perro> perros = new List<Perro>();
            return new Refugio(nombre, perros);
        }

        static int ElegirRefugio(List<Refugio> lr)
        {
            VerRefugios(lr);
            int elec = new int();
            Console.Write("Elija el refugio:");
            elec = PedirNum(lr.Count) - 1;
            return elec;
        }

        static List<Refugio> CargarPerro(List<Refugio> lr)
        {
            if (lr.Count > 0)
            {
                lr[ElegirRefugio(lr)].AddPerro(IngresarPerro());
            } else
            {
                Console.WriteLine("No hay refugios. Por favor ingrese por lo menos uno.");
            }
            return lr;
        }

        static void VerRefugios(List<Refugio> lr)
        {
            int i = new int();
            Console.WriteLine("Lista de refugios:");
            for (i = 0; i < lr.Count; i++)
            {
                Console.WriteLine("{0}. {1}", (i + 1), lr[i].getNombre());
            }
        }

        static void ListaPerros(List<Perro> lp)
        {
            if (lp.Count > 0)
            {
                foreach (Perro p in lp)
                {
                    Console.WriteLine("Nombre del perro: {0}\nRaza del perro: {1}", p.getNombre(), p.getRaza());
                }
            }
            else
            {
                Console.WriteLine("No hay perros. Por favor ingrese por lo menos uno.");
            }
        }

        static void VerPerros(List<Refugio> lr)
        {
            int elec = new int();
            if (lr.Count > 0)
            {
                elec = ElegirRefugio(lr);
                List<Perro> lp = lr[elec].getPerros();
                ListaPerros(lp);
            }
            else
            {
                Console.WriteLine("No hay refugios. Por favor ingrese por lo menos uno.");
            }
        }

        static void Main(string[] args)
        {
            List<Refugio> refugios = new List<Refugio>();
            int numMenu;
            do
            {
                numMenu = Menu();
                switch (numMenu)
                {
                    case 1:
                        refugios.Add(CargarRefugio());
                        break;
                    case 2:
                        CargarPerro(refugios);
                        break;
                    case 3:
                        VerPerros(refugios);
                        break;
                }
            } while (numMenu < 4);
        }
    }
}
