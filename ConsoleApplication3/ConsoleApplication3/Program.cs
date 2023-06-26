using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication18
{
    namespace tp4
    {
        class Producto
        {

            string nombre;
            int cantidad;
            int precio;

            public Producto(string nombre, int cantidad)
            {
                this.nombre = nombre;
                this.cantidad = cantidad;
            }
            public string getNombre()
            {
                return this.nombre;
            }
            public int getCantidad()
            {
                return this.cantidad;
            }
            public int getPrecio()
            {
                return this.precio;
            }
            public void setPrecio(int precio)
            {
                this.precio = precio;
            }

        class Program
            {
                static void Main(string[] args)
                {
                    List<Producto> productos = new List<Producto>();
                    int op;
                    do
                    {
                        op = Menu();
                        switch (op)
                        {

                            case 1:
                                productos.Add(AgregarProductos());
                                break;
                            case 2:
                                productos = QuitarProductos(productos);
                                break;
                            case 3:
                                VerProductos(productos);
                                break;
                        }
                    } while (op != 4);
                }

                static int Menu()
                {
                    Console.Clear();
                    Console.SetCursorPosition(29, 0);
                    Console.WriteLine("MENU PRINCIPAL");
                    Console.WriteLine("1)Agregar producto");
                    Console.WriteLine("2)Quitar producto");
                    Console.WriteLine("3)Productos");
                    Console.WriteLine("4)Salir");

                    Console.WriteLine("Ingrese una opción:");
                    Console.SetCursorPosition(19, 5);
                    return PedirOpción();
                }

                static int PedirOpción()
                {
                    int op = new int();
                    do
                    {
                        op = PedirInt();
                    } while (op < 1 || op > 4);
                    return op;
                }
                static int PedirInt()
                {
                    int entero = new int();
                    bool valido = new bool();

                    do
                    {
                        try
                        {
                            entero = int.Parse(Console.ReadLine());
                            valido = true;
                        }
                        catch { Console.WriteLine("Error de tipo de dato"); }
                    } while (!valido);
                    return entero;
                }

                static List<Producto> QuitarProductos(List<Producto> productos)
                {
                    Console.Clear();
                    if (productos.Count > 0)
                    {
                        productos.RemoveAt(IndiceQuitarProducto(productos));
                    } else
                    {
                        Console.Write("No hay ningún producto en la lista. Añada por lo menos un producto.");
                        Console.ReadKey();
                    }
                    return productos;
                }

                static int IndiceQuitarProducto(List<Producto> productos)
                {
                    int eleccion = new int();
                    bool esValido = new bool();
                    Console.WriteLine("ELIJA UN PRODUCTO PARA QUITAR");
                    ListaProductos(productos);
                    do
                    {
                        eleccion = PedirInt();
                        if (eleccion > 0 && eleccion <= productos.Count)
                        {
                            esValido = true;
                            eleccion--;
                        } else
                        {
                            esValido = false;
                            Console.Write("Entrada inválida. Ingrese un número que esté en la lista.");
                            Console.ReadKey();
                        }
                    } while (!esValido);
                    return eleccion;
                }

                static Producto AgregarProductos()
                {
                    Console.Clear();
                    Console.SetCursorPosition(29, 0);
                    Console.WriteLine("AGREGUE UN PRODUCTO");
                    string nombre = PedirString("Ingrese nombre:", false);
                    int cantidad = int.Parse(PedirString("Ingrese cantidad:", false));
                    int precio = int.Parse(PedirString("Ingrese precio:", true));
                    Producto producto = new Producto(nombre, cantidad);
                    if (precio != 0) producto.setPrecio(precio);
                    return producto;
                }

                static string PedirString(string men, bool opcional)
                {
                    string cadena = string.Empty;
                    Console.WriteLine(men);
                    do
                    {
                        cadena = Console.ReadLine();
                    } while (!opcional && string.IsNullOrEmpty(cadena));
                    return cadena;
                }

                static void VerProductos(List<Producto> productos)
                {
                    Console.Clear();
                    Console.SetCursorPosition(29, 0);
                    Console.WriteLine("PRODUCTOS ALMACENADOS");
                    foreach (Producto producto in productos)
                    {
                        Console.WriteLine("nombre:" + producto.getNombre());
                        Console.WriteLine("cantidad:" + producto.getCantidad());
                        Console.WriteLine("precio:" + producto.getPrecio());
                        Console.WriteLine();
                    }
                    Console.ReadKey();

                }

                static void ListaProductos(List<Producto> productos)
                {
                    Console.Clear();
                    int i = new int();
                    for (i = 0; i < productos.Count; i++)
                    {
                        Console.WriteLine((i+1) + ". " + productos[i].getNombre());
                    }
                }
            }
        }
    }
}