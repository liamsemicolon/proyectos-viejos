using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Cajero_ATM_ANUAL
{
    public class Cliente
    { 
        public int nroCuenta, dni;
        public float saldo;
        public string nombre, apellido, direccion, telefono, email, pin;
        public bool pFijo;
        public bool[,] servAsoc;
        public DateTime[] servPagos;

        public Cliente()
        {
        }

        public static Cliente StringACliente(string str)
        {
            Cliente c = new Cliente();
            c.servAsoc = new bool[3, 2];
            c.servPagos = new DateTime[3];
            string[] strlist = new string[19];
            strlist = str.Split('|');
            c.nroCuenta = Convert.ToInt32(strlist[0]);
            c.dni = Convert.ToInt32(strlist[1]);
            c.saldo = Convert.ToSingle(strlist[2]);
            c.nombre = strlist[3];
            c.apellido = strlist[4];
            c.direccion = strlist[5];
            c.telefono = strlist[6];
            c.email = strlist[7];
            c.pin = strlist[8];
            c.pFijo = Convert.ToBoolean(strlist[9]);
            c.servAsoc[0, 0] = Convert.ToBoolean(strlist[10]);
            c.servAsoc[0, 1] = Convert.ToBoolean(strlist[11]);
            c.servAsoc[1, 0] = Convert.ToBoolean(strlist[12]);
            c.servAsoc[1, 1] = Convert.ToBoolean(strlist[13]);
            c.servAsoc[2, 0] = Convert.ToBoolean(strlist[14]);
            c.servAsoc[2, 1] = Convert.ToBoolean(strlist[15]);
            c.servPagos[0] = Convert.ToDateTime(strlist[16]);
            c.servPagos[1] = Convert.ToDateTime(strlist[17]);
            c.servPagos[2] = Convert.ToDateTime(strlist[18]);
            return c;
        }
        public static Cliente LlenarDatos(string cabecera)
        {
            Cliente c = new Cliente();
            c.saldo = 0;
            c.pFijo = false;
            c.servAsoc = new bool[3, 2];
            c.servPagos = new DateTime[3];
            string[] nombresDatos = { "nombre", "apellido", "DNI", "dirección", "número de teléfono", "dirección de e-mail", "PIN / contraseña"};
            string[] datos = new string[7];
            int i = new int();
            for (i = 0; i < nombresDatos.Length; i++)
            {
                Console.Write("Ingrese su " + nombresDatos[i] + ":\n> ");
                datos[i] = Console.ReadLine();
            }
            c.nombre = datos[0].Trim();
            c.apellido = datos[1].Trim().ToUpper();
            try
            {
                c.dni = Convert.ToInt32(datos[2].Trim());
            }
            catch
            {
                Console.Write("DNI inválido, intente de nuevo...\n> ");
                c.dni = Program.PedirInt();
            }
            c.direccion = datos[3].Trim();
            c.telefono = datos[4].Trim();
            c.email = datos[5].Trim();
            c.pin = datos[6].Trim();

            string[] nombresServicios = { "Edesur", "Gas Natural BAN", "Telecom" };
            char elec = new char();
            for(i = 0; i < 3; i++)
            {
                bool bucle = new bool();
                bool[,] afiliado = new bool[3, 2];
                do
                {
                    Program.ImprimirCabecera(cabecera);
                    Console.Write("¿Está afiliado a " + nombresServicios[i] + "? [S/N]\n> ");
                    elec = Program.PedirChar();
                    if (elec == 'S')
                    {
                        c.servAsoc[i, 0] = true;
                        bucle = false;
                    }
                    else if (elec == 'N')
                    {
                        c.servAsoc[i, 0] = false;
                        bucle = false;
                    } else
                    {
                        bucle = true;
                        Console.Write("Entrada no válida. Presione cualquier tecla para intentar de nuevo...");
                        Console.ReadKey();
                        continue;
                    }
                } while (bucle);
            }
            return c;
        }

        public static Cliente EditarDatos(Cliente c, string cabecera)
        {
            bool bucle = new bool();
            int i = new int(), elec = new int();
            string[] nombresDatos = { "Nombre", "Apellido", "Dirección", "Número de teléfono", "Dirección de e-mail", "Servicios asociados" };
            do
            {
                Program.ImprimirCabecera(cabecera);
                Console.WriteLine("¿Qué quiere modificar de la cuenta?");
                for(i = 0; i < nombresDatos.Length; i++)
                {
                    Console.WriteLine("\t" + (i + 1) + ") " + nombresDatos[i]);
                }
                Console.Write("> ");
                elec = Program.PedirInt();
                if(elec < 0 || elec > 7)
                {
                    bucle = true;
                    Console.Write("Número fuera del rango de los índices. Intente de nuevo...");
                    Console.ReadKey();
                } else
                {
                    bucle = false;
                }
            } while (bucle);
            bucle = new bool();
            do
            {
                Program.ImprimirCabecera(cabecera);
                switch (elec)
                {
                    case 1:
                        Console.Write("Ingrese el nuevo nombre que se asociará a la cuenta:\n> ");
                        c.nombre = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Ingrese el nuevo apellido que se asociará a la cuenta:\n> ");
                        c.apellido = Console.ReadLine().ToUpper();
                        break;
                    case 3:
                        Console.Write("Ingrese la nueva dirección que se asociará a la cuenta:\n> ");
                        c.direccion = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("Ingrese el nuevo número de teléfono que se asociará a la cuenta:\n> ");
                        c.telefono = Console.ReadLine();
                        break;
                    case 5:
                        Console.Write("Ingrese la nueva dirección de e-mail que se asociará a la cuenta:\n> ");
                        c.email = Console.ReadLine();
                        break;
                    case 6:
                        string[] nombresServicios = { "Edesur", "Gas Natural BAN", "Telecom" };
                        char elecSer = new char();
                        for (i = 0; i < 3; i++)
                        {
                            bucle = new bool();
                            do
                            {
                                Program.ImprimirCabecera(cabecera);
                                Console.Write("¿Está afiliado a " + nombresServicios[i] + "? [S/N]\n> ");
                                elecSer = Program.PedirChar();
                                if (elecSer == 'S')
                                {
                                    c.servAsoc[i, 0] = true;
                                    c.servAsoc[i, 1] = false;
                                    bucle = false;
                                }
                                else if (elecSer == 'N')
                                { 
                                    c.servAsoc[i, 0] = false;
                                    c.servAsoc[i, 1] = false;
                                    bucle = false;
                                }
                                else
                                {
                                    bucle = true;
                                    Console.Write("Entrada no válida. Presione cualquier tecla para intentar de nuevo...");
                                    Console.ReadKey();
                                    continue;
                                }
                            } while (bucle);
                        }
                        break;
                }
            } while (bucle);
            return c;
        }

        public static Cliente PagarServicios(Cliente c)
        {
            int i = new int(), elec = new int();
            int[] precios = { 1000, 400, 800 };
            string[] nombresServicios = { "Edesur", "Gas Natural BAN", "Telecom" };
            bool bucle = new bool();
            bool condEdesur = c.servAsoc[0, 0] && c.servAsoc[0, 1];
            bool condBAN = c.servAsoc[1, 0] && c.servAsoc[1, 1];
            bool condTelecom = c.servAsoc[2, 0] && c.servAsoc[2, 1];
            bool condFinal = condEdesur || condBAN || condTelecom;
            if (condFinal)
            {
                Program.ImprimirCabecera("Pagar servicios");
                Console.WriteLine("No hay servicios para pagar.");
            }
            else
            {
                do
                {
                    Program.ImprimirCabecera("Pagar servicios");
                    Console.WriteLine("¿Qué servicio quiere pagar?");
                    for (i = 0; i < 3; i++)
                    {
                        if (c.servAsoc[i, 0] == true && c.servAsoc[i, 1] == false)
                        {
                            Console.WriteLine((i + 1) + ") " + nombresServicios[i]);
                        }
                    }
                    Console.Write("> ");
                    elec = Program.PedirInt();
                    if (elec > 0 && elec < 4)
                    {
                        bucle = false;
                        c.saldo -= precios[elec - 1];
                        c.servPagos[elec - 1] = DateTime.Today;
                        c.servAsoc[elec - 1, 1] = true;
                        Console.WriteLine("La factura de " + nombresServicios[elec - 1] + " ha sido pagada. Se han debitado $" + precios[elec - 1] + " a su cuenta.");
                    }
                    else
                    {
                        bucle = true;
                    }
                } while (bucle);
            }
            return c;
        }

        public static void Consulta(Cliente c)
        {
            Console.Write(c.apellido + ", " + c.nombre + "\nN° de cuenta: " + c.nroCuenta + "\nSaldo: " + c.saldo);
        }
        
        static public bool loginPIN(Cliente c)
        {
            bool b = new bool();
            Console.Write("Ingrese su PIN o contraseña:\n> ");
            string pin = Console.ReadLine().Trim();
            if (pin == c.pin)
            {
                b = false;
            }
            else
            {
                Console.Write("PIN incorrecto. Intente de nuevo...");
                Console.ReadKey();
                Console.Clear();
                b = true;
            }
            return b;
        }
    }

    public class Extraccion
    {
        public Cliente cliente;
        public DateTime fechaRetiro;
        public float monto;

        public Extraccion(Cliente cliente)
        {
            this.cliente = cliente;
        }

        public static Extraccion SacarTurno(Cliente c)
        {
            Extraccion e = new Extraccion(c);
            float monto = new float();
            int cantDias = new int();
            bool bucle = new bool();
                do
                {
                    bucle = new bool();
                    Console.Write("¿Cuánto dinero desea extraer?\n> ");
                    monto = Program.PedirFloat();
                    if (monto > 0)
                    {
                        e.monto = monto;
                        c.saldo -= monto;
                        bucle = false;
                    }
                    else
                    {
                        Console.Write("El monto extraído no puede ser menor a $0. Intente de nuevo...");
                        Console.ReadKey();
                        Console.Clear();
                        bucle = true;
                    }
                } while (bucle);
                do
                {
                    bucle = new bool();
                    Console.Write("¿En cuántos días quiere retirar el monto (máximo 15 días, atención las 24 hs)?\n> ");
                    cantDias = Program.PedirInt();
                    if (cantDias >= 0 && cantDias <= 15)
                    {
                        e.fechaRetiro = DateTime.Today.AddDays(cantDias);
                        bucle = false;
                    }
                    else
                    {
                        Console.Write("El número ingresado está afuera del rango permitido. Intente de nuevo...");
                        Console.ReadKey();
                        Console.Clear();
                        bucle = true;
                    }
                } while (bucle);
            return e;
        }
    }

    public class PlazoFijo
    {
        public float importe;
        public int nroCuenta, plazo, dni;
        public DateTime fechaComienzo, fechaFin;

        public PlazoFijo()
        {
        }
        public static PlazoFijo StringAPlazoFijo(string str)
        {
            PlazoFijo pF = new PlazoFijo();
            string[] strlist = new string[6];
            strlist = str.Split('|');
            pF.importe = Convert.ToSingle(strlist[0]);
            pF.nroCuenta = Convert.ToInt32(strlist[1]);
            pF.dni = Convert.ToInt32(strlist[2]);
            pF.plazo = Convert.ToInt32(strlist[3]);
            pF.fechaComienzo = Convert.ToDateTime(strlist[4]);
            pF.fechaFin = Convert.ToDateTime(strlist[5]);
            return pF;
        }

        public static PlazoFijo LlenarDatos(Cliente c)
        {
            PlazoFijo pF = new PlazoFijo();
            pF.nroCuenta = c.nroCuenta;
            bool bucle = new bool();
            float importe = new float();
            if (!c.pFijo)
            {
                do
                {
                    Program.ImprimirCabecera("Crear plazo fijo");
                    bucle = new bool();
                    Console.Write("¿Cuán largo quiere que sea el plazo fijo?\n\t1) 30 días\n\t2) 60 días\n\t3) 90 días\n> ");
                    int elec = Program.PedirInt();
                    if (elec > 0 && elec < 4)
                    {
                        pF.plazo = (elec - 1);
                        pF.fechaComienzo = DateTime.Today;
                        pF.fechaFin = pF.fechaComienzo.AddDays(30 * elec);
                        bucle = false;
                    }
                    else
                    {
                        bucle = true;
                        Console.Write("Entrada no válida. Presione cualquier tecla para intentar de nuevo...");
                        Console.ReadKey();
                    }
                } while (bucle);
                do
                {
                    Program.ImprimirCabecera("Crear plazo fijo");
                    bucle = new bool();
                    Console.Write("¿Cuánto dinero quiere asignar al plazo fijo? (monto mínimo: $5000)\n> ");
                    importe = Program.PedirFloat();
                    if (importe >= 5000)
                    {
                        pF.importe = importe;
                        c.saldo -= importe;
                        do
                        {
                            bucle = Cliente.loginPIN(c);
                        } while (bucle);
                    }
                    else
                    {
                        bucle = true;
                        Console.Write("El monto no puede ser menor a $5000.\nPresione cualquier tecla para intentar de nuevo...");
                        Console.ReadKey();
                    }
                } while (bucle);
                c.pFijo = true;
                Console.Write("El plazo fijo ha sido asignado con éxito.\nPresione cualquier tecla para salir...");
            }
            else
            {
                Console.Write("La cuenta ya tiene un plazo fijo.\nPresione cualquier tecla para salir...");
            }
            Console.ReadKey();
            return pF;
        }
    }

    public class PagoEmpresa
    {
        public int nroEmpresa, nroEmpleado, dniEmpresa, dniEmpleado;
        public float aPagar;
        public DateTime ultimoPago;

        public PagoEmpresa()
        {
        }

        public static PagoEmpresa StringAPagoEmpresa(string str)
        {
            PagoEmpresa pE = new PagoEmpresa();
            string[] strlist = new string[6];
            strlist = str.Split('|');
            pE.nroEmpresa = Convert.ToInt32(strlist[0]);
            pE.dniEmpresa = Convert.ToInt32(strlist[1]);
            pE.nroEmpleado = Convert.ToInt32(strlist[2]);
            pE.dniEmpleado = Convert.ToInt32(strlist[3]);
            pE.aPagar = Convert.ToSingle(strlist[4]);
            pE.ultimoPago = Convert.ToDateTime(strlist[5]);
            return pE;
        }

        public static PagoEmpresa LlenarDatos(List<Cliente> c)
        {
            PagoEmpresa pE = new PagoEmpresa();
            Cliente empleado = new Cliente();
            Cliente empresa = new Cliente();
            bool bucle = new bool(), encontrado = new bool();
            float importe = new float();
            do
            {
                bucle = new bool();
                encontrado = new bool();
                Console.Write("¿A qué cuenta quiere asignar sus haberes?\n> ");
                int elec = Program.PedirInt();
                foreach (Cliente cl in c)
                {
                    if (elec == cl.nroCuenta)
                    {
                        encontrado = true;
                        empleado = cl;
                        break;
                    }
                    else
                    {
                        encontrado = false;
                    }
                }
                if (encontrado)
                {
                    bucle = false;
                } else
                {
                    Console.Write("N° de cuenta no encontrado. Intente de nuevo...");
                    Console.ReadKey();
                    Console.Clear();
                    bucle = true;
                }
            } while (bucle);
            do
            {
                bucle = new bool();
                encontrado = new bool();
                Console.Write("¿Qué cuenta será usada para el pago?\n> ");
                int elec = Program.PedirInt();
                foreach (Cliente cl in c)
                {
                    if (elec == cl.nroCuenta)
                    {
                        encontrado = true;
                        empresa = cl;
                        break;
                    }
                    else
                    {
                        encontrado = false;
                    }
                }
                if (encontrado)
                {
                    do
                    {
                        bucle = Cliente.loginPIN(empresa);
                    } while (bucle);
                }
                else
                {
                    Console.Write("N° de cuenta no encontrado. Intente de nuevo...");
                    Console.ReadKey();
                    Console.Clear();
                    bucle = true;
                }
            } while (bucle);
            do
            {
                bucle = new bool();
                encontrado = new bool();
                Console.Write("¿Cuál es el sueldo del empleado?\n> ");
                importe = Program.PedirFloat();
                if (importe > 0)
                {
                    pE.aPagar = importe;
                    bucle = false;
                }
                else
                {
                    Console.Write("El sueldo no puede ser menor a $0. Intente de nuevo...");
                    Console.ReadKey();
                    Console.Clear();
                    bucle = true;
                }
            } while (bucle);
            pE.nroEmpresa = empresa.nroCuenta;
            pE.nroEmpleado = empleado.nroCuenta;
            Console.Write("Los haberes han sido asignados con éxito:\n\t$" + pE.aPagar + " mensuales a " + empleado.nombre + " " + empleado.apellido + " (DNI " + empleado.dni + ", N° de cuenta " + empleado.nroCuenta +")\nPresione cualquier tecla para salir...");
            Console.ReadKey();
            Console.Clear();
            return pE;
        }

    }

    class Program
    {
        static List<Cliente> listaClientes = new List<Cliente> { };
        static List<PlazoFijo> listaPlazos = new List<PlazoFijo> { };
        static List<PagoEmpresa> listaHaberes = new List<PagoEmpresa> { };
        static string delim = "|";
        static string direccionArchivo = Directory.GetCurrentDirectory() + @"\";

        public static int PedirInt()
        {
            int n = new int();
            bool bucle;
            do
            {
                bucle = new bool();
                try
                {
                    n = Convert.ToInt32(Console.ReadLine().Trim());
                    bucle = false;
                }
                catch
                {
                    bucle = true;
                    Console.Write("Entrada no válida. Intente de nuevo...\n> ");
                }
            } while (bucle);
            return n;
        }

        public static float PedirFloat()
        {
            float n = new float();
            bool bucle = new bool();
            do
            {
                try
                {
                    n = Convert.ToSingle(Console.ReadLine().Trim());
                    bucle = false;
                }
                catch
                {
                    bucle = true;
                    Console.Write("Entrada no válida. Intente de nuevo...\n> ");
                }
            } while (bucle);
            return n;
        }

        public static char PedirChar()
        {
            char ch = new char();
            bool bucle = new bool();
            do
            {
                try
                {
                    ch = Convert.ToChar(Console.ReadLine().ToUpper());
                    bucle = false;
                }
                catch
                {
                    bucle = true;
                    Console.Write("Entrada no válida. Intente de nuevo...\n> ");
                }
            } while (bucle);
            return ch;
        }

        static void LeerArchivos()
        {
            int i = new int();
            int diaDeHoy = Convert.ToInt16(DateTime.Today.Day);
            string[] arrC = File.ReadAllLines(direccionArchivo + "clientes.txt");
            string[] arrPE = File.ReadAllLines(direccionArchivo + "haberes.txt");
            string[] arrPF = File.ReadAllLines(direccionArchivo + "plazosfijos.txt");
            foreach(string s in arrC)
            {
                if (s != string.Empty)
                {
                    Cliente c = Cliente.StringACliente(s);
                    listaClientes.Add(c);
                }
            }
            foreach (string s in arrPF)
            {
                if (s != string.Empty && s != "0|0|0|1/1/0001|1/1/0001")
                {
                    PlazoFijo pF = PlazoFijo.StringAPlazoFijo(s);
                    listaPlazos.Add(pF);
                }
            }
            foreach (string s in arrPE)
            {
                if (s != string.Empty)
                {
                    PagoEmpresa pE = PagoEmpresa.StringAPagoEmpresa(s);
                    listaHaberes.Add(pE);
                }
            }
            foreach(Cliente c in listaClientes)
            {
                for(i = 0; i < 3; i++)
                {
                    if ((c.servPagos[i] - DateTime.Today).TotalDays >= 30)
                    {
                        c.servAsoc[i, 1] = false;
                    }
                }
            }
            List<int> indicesABorrar = new List<int> { };
            List<int> listaDNIs = new List<int> { };
            for (i = 0; i < listaPlazos.Count(); i++)
            {
                PlazoFijo pF = listaPlazos[i];
                int j = new int(), indCuenta = new int();
                if(DateTime.Today > pF.fechaFin)
                {
                    for (j = 0; j < listaClientes.Count(); j++)
                    {
                        if(listaClientes[j].nroCuenta == pF.nroCuenta)
                        {
                            indicesABorrar.Add(i);
                            indCuenta = j;
                        }
                    }
                    listaClientes[indCuenta].saldo += pF.importe;
                }
                foreach (Cliente c in listaClientes)
                {
                    listaDNIs.Add(c.dni);
                }
                if (!listaDNIs.Contains(pF.dni))
                {
                    indicesABorrar.Add(i);
                }
            }
            List<int> indicesSinRepetir = indicesABorrar.Distinct().ToList();
            foreach (int n in indicesSinRepetir)
            {
                Console.Write(n);
                listaPlazos.RemoveAt(n);
            }
            foreach(PagoEmpresa pE in listaHaberes)
            {
                if(diaDeHoy == 20 && (DateTime.Today - pE.ultimoPago).TotalDays > 26)
                {
                    foreach(Cliente c in listaClientes)
                    {
                        if(pE.nroEmpleado == c.nroCuenta)
                        {
                            c.saldo += pE.aPagar;
                            pE.ultimoPago = DateTime.Today;
                        }
                        if(pE.nroEmpresa == c.nroCuenta)
                        {
                            c.saldo -= pE.aPagar;
                        }
                    }
                }
            }
            indicesABorrar = new List<int> { };
            listaDNIs = new List<int> { }; 
            for (i = 0; i < listaHaberes.Count(); i++)
            {
                PagoEmpresa pE = listaHaberes[i];
                foreach (Cliente c in listaClientes)
                {
                    listaDNIs.Add(c.dni);
                }
                bool cond1 = !listaDNIs.Contains(pE.dniEmpresa);
                bool cond2 = !listaDNIs.Contains(pE.dniEmpleado);
                if (cond1 || cond2)
                {
                    indicesABorrar.Add(i);
                }
            }
            foreach (int n in indicesABorrar)
            {
                listaHaberes.RemoveAt(n);
            }
        }

        static void EscribirArchivos()
        {
            List<string> listaValores = new List<string> { };
            string[] valores = { };
            using (StreamWriter sw = new StreamWriter(direccionArchivo + "clientes.txt"))
            {
                foreach (Cliente c in listaClientes)
                {
                    listaValores.Add(c.nroCuenta.ToString());
                    listaValores.Add(c.dni.ToString());
                    listaValores.Add(c.saldo.ToString());
                    listaValores.Add(c.nombre);
                    listaValores.Add(c.apellido);
                    listaValores.Add(c.direccion);
                    listaValores.Add(c.telefono);
                    listaValores.Add(c.email);
                    listaValores.Add(c.pin);
                    listaValores.Add(c.pFijo.ToString());
                    listaValores.Add(c.servAsoc[0, 0].ToString());
                    listaValores.Add(c.servAsoc[0, 1].ToString());
                    listaValores.Add(c.servAsoc[1, 0].ToString());
                    listaValores.Add(c.servAsoc[1, 1].ToString());
                    listaValores.Add(c.servAsoc[2, 0].ToString());
                    listaValores.Add(c.servAsoc[2, 1].ToString());
                    listaValores.Add(c.servPagos[0].Date.ToString("d"));
                    listaValores.Add(c.servPagos[1].Date.ToString("d"));
                    listaValores.Add(c.servPagos[2].Date.ToString("d"));
                    valores = listaValores.ToArray();
                    sw.WriteLine(string.Join(delim, valores));
                }
            }
            listaValores = new List<string> { };
            using (StreamWriter sw = new StreamWriter(direccionArchivo + "haberes.txt"))
            {
                foreach (PagoEmpresa pE in listaHaberes)
                {
                    listaValores.Add(pE.nroEmpresa.ToString());
                    listaValores.Add(pE.dniEmpresa.ToString());
                    listaValores.Add(pE.nroEmpleado.ToString());
                    listaValores.Add(pE.dniEmpleado.ToString());
                    listaValores.Add(pE.aPagar.ToString());
                    listaValores.Add(pE.ultimoPago.ToString("d"));
                    valores = listaValores.ToArray();
                    sw.WriteLine(string.Join(delim, valores));
                } 
            }
            listaValores = new List<string> { };
            using (StreamWriter sw = new StreamWriter(direccionArchivo + "plazosfijos.txt"))
            {
                foreach (PlazoFijo pF in listaPlazos)
                {
                    listaValores.Add(pF.importe.ToString());
                    listaValores.Add(pF.nroCuenta.ToString());
                    listaValores.Add(pF.dni.ToString());
                    listaValores.Add(pF.plazo.ToString());
                    listaValores.Add(pF.fechaComienzo.ToString("d"));
                    listaValores.Add(pF.fechaFin.ToString("d"));
                    valores = listaValores.ToArray();
                    sw.WriteLine(string.Join(delim, valores));
                }
            }
        }

        static bool MandarEmail(Cliente c, string asunto, string cuerpo)
        {
           try
            {
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress("bancoatm10@gmail.com", "Banco ATM", Encoding.UTF8);
                correo.To.Add(c.email);
                correo.Subject = asunto;
                correo.Body = cuerpo;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.Credentials = new NetworkCredential("bancoatm10@gmail.com", "BANCO365365");
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;
                smtp.Send(correo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void ImprimirCabecera(string tituloCabecera)
        {
            Console.Clear();
            Console.WriteLine("Cajero ATM | " + tituloCabecera + " | " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
        }

        static void MenuPrincipal()
        {
            bool bucle = new bool();
            do
            {
                ImprimirCabecera("Menú principal");
                Console.Write("\t1) Administración de cuentas\n\t2) Caja de ahorro\n\t3) Salir\n> ");
                int elec = PedirInt();
                switch (elec)
                {
                    case 1:
                        MenuAdmCuentas();
                        bucle = true;
                        break;
                    case 2:
                        MenuCajaAhorro();
                        bucle = true;
                        break;
                    case 3:
                        bucle = false;
                        break;
                    default:
                        bucle = true;
                        break;
                }
            } while (bucle);
        }

        static void MenuAdmCuentas()
        {
            bool bucle = new bool();
            do
            {
                ImprimirCabecera("Administración de cuentas");
                Console.Write("\t1) Crear cuenta\n\t2) Eliminar cuenta\n\t3) Modificar cuenta\n\t4) Configuración de haberes (para empresas)\n\t5) Salir\n> ");
                int elec = PedirInt();
                switch (elec)
                {
                    case 1:
                        CrearCuenta();
                        bucle = true;
                        break;
                    case 2:
                        EliminarCuenta();
                        bucle = true;
                        break;
                    case 3:
                        ModificarCuenta();
                        bucle = true;
                        break;
                    case 4:
                        ConfigHaberes();
                        bucle = true;
                        break;
                    case 5:
                        bucle = false;
                        break;
                    default:
                        bucle = true;
                        break;
                }
            } while (bucle);
        }

        static void CrearCuenta()
        {
            List<int> listaDNIs = new List<int> { };
            Cliente c = new Cliente();
            bool bucle = new bool();
            do {
                ImprimirCabecera("Crear cuenta");
                c = Cliente.LlenarDatos("Crear cuenta");
                bucle = new bool();
                foreach (Cliente cl in listaClientes)
                {
                    listaDNIs.Add(cl.dni);
                    bucle = false;
                }
                if (listaDNIs.Contains(c.dni))
                {
                    Console.Write("Una cuenta con ese DNI ya existe, intente con otro.");
                    Console.ReadKey();
                    bucle = true;
                }
            } while (bucle);
            c.nroCuenta = listaClientes.Count;
            listaClientes.Add(c);
            string asunto = ("Cuenta al nombre de " + c.apellido + ", " + c.nombre);
            string cuerpo = ("Cuenta bancaria creada al nombre de " + c.apellido + ", " + c.nombre + " (DNI " + c.dni + ", número de teléfono " + c.telefono + ")<br />N° de cuenta: " + c.nroCuenta + "<br /><br />Muchas gracias por confiar en nuestro sistema.<br /><br />- Banco ATM");
            bool email = MandarEmail(c, asunto, cuerpo);
            if (email)
            {
                Console.Write("Cuenta creada con éxito. E-mail de confirmación enviado.\nN° de cuenta: " + c.nroCuenta + "\nPresione cualquier tecla para salir...");
            }
            else {
                Console.Write("Cuenta creada con éxito. E-mail de confirmación no enviado (dirección incorrecta o inexistente).\nN° de cuenta: " + c.nroCuenta + "\nPresione cualquier tecla para salir...");
            }
            Console.ReadKey();
        }

        static void EliminarCuenta()
        {
            int elec = new int();
            bool bucle = new bool(), denegado = new bool();
            if(listaClientes.Count <= 0)
            {
                ImprimirCabecera("Eliminar cuenta");
                Console.Write("No hay cuentas para eliminar, intente de nuevo una vez que haya creado por lo menos una cuenta.");
            } else
            {
                do
                {
                    ImprimirCabecera("Eliminar cuenta");
                    bucle = new bool();
                    Console.Write("Ingrese el N° de la cuenta que desea eliminar:\n> ");
                    elec = PedirInt();
                    if(elec < -1 && elec > listaClientes.Count)
                    {
                        bucle = true;
                        Console.Write("Número fuera del rango de los índices. Intente de nuevo...");
                        Console.ReadKey();
                    } else
                    {
                        denegado = Cliente.loginPIN(listaClientes[elec]);
                        if (!denegado)
                        {
                            listaClientes.RemoveAt(elec);
                            Console.Write("Cuenta N° " + elec + " eliminada con éxito.\nPresione cualquier tecla para salir...");
                        }
                        else
                        {
                            Console.Write("La contraseña ingresada es incorrecta.\nPresione cualquier tecla para salir...");
                        }
                    }
                } while (bucle);
            }
            Console.ReadKey();
            Console.Clear();
        }

        static void ModificarCuenta()
        {
            int elec = new int();
            bool bucle = new bool(), denegado;
            if (listaClientes.Count <= 0)
            {
                ImprimirCabecera("Modificar cuenta");
                Console.Write("No hay cuentas para modificar, intente de nuevo una vez que haya creado por lo menos una cuenta.");
            }
            else
            {
                do
                {
                    bucle = new bool();
                    ImprimirCabecera("Modificar cuenta");
                    Console.Write("Ingrese su N° de cuenta\n> ");
                    elec = PedirInt();
                    if (elec < -1 || elec > listaClientes.Count)
                    {
                        Console.Write("Número fuera del rango de los índices. Intente de nuevo...");
                        Console.ReadKey();
                        bucle = true;
                    }
                    else
                    {
                        
                        denegado = Cliente.loginPIN(listaClientes[elec]);
                        if (!denegado)
                        {
                            listaClientes[elec] = Cliente.EditarDatos(listaClientes[elec], "Modificar cuenta");
                            Console.Write("Cuenta N° " + elec + " modificada con éxito.\nPresione cualquier tecla para salir...");
                        }
                        else
                        {
                            Console.Write("La contraseña ingresada es incorrecta.\nPresione cualquier tecla para salir...");
                        }

                    }
                } while (bucle);
            }
            Console.ReadKey();
            Console.Clear();
        }

        static void ConfigHaberes()
        {
            ImprimirCabecera("Configuración de haberes");
            if (listaClientes.Any())
            {
                PagoEmpresa pF = PagoEmpresa.LlenarDatos(listaClientes);
                if (!listaHaberes.Contains(pF))
                {
                    listaHaberes.Add(pF);
                }
            } else
            {
                Console.Write("No hay clientes. No se pueden configurar los haberes sin clientes.\nPresione cualquier tecla para volver al menú...");
                Console.ReadKey();
            }
        }

        static void MenuCajaAhorro()
        {
            bool bucle = new bool(), encontrado = new bool();
            int i = new int(), indCuenta = new int();
            if(listaClientes.Count() == 0)
            {
                ImprimirCabecera("Caja de ahorro");
                Console.Write("No hay cuentas como para entrar a la caja de ahorro.\nPresione cualquier tecla para salir...");
                Console.ReadKey();
            } else
            {
                bucle = new bool();
                encontrado = new bool();
                do
                {
                        ImprimirCabecera("Caja de ahorro");
                        Console.Write("Ingrese su número de cuenta:\n> ");
                        int elec = PedirInt();
                        for (i = 0; i < listaClientes.Count(); i++)
                        {
                            if (elec == listaClientes[i].nroCuenta)
                            {
                                encontrado = true;
                                indCuenta = i;
                                break;
                            }
                            else
                            {
                                encontrado = false;
                            }
                        }
                        if (encontrado)
                        {
                            bucle = false;
                        }
                        else
                        {
                            Console.Write("N° de cuenta no encontrado. Intente de nuevo...");
                            Console.ReadKey();
                            Console.Clear();
                            bucle = true;
                        }
                } while (bucle);
                do
                {
                    bucle = Cliente.loginPIN(listaClientes[indCuenta]);
                } while (bucle);
                do
                {
                    ImprimirCabecera("Caja de ahorro");
                    Console.Write("Bienvenido " + listaClientes[indCuenta].apellido + ", " + listaClientes[indCuenta].nombre + "\n\t1) Depositar dinero\n\t2) Extraer dinero\n\t3) Crear plazo fijo\n\t4) Pagar servicios\n\t5) Consultar saldo\n\t6) Salir\n> ");
                    int elec = PedirInt();
                    switch (elec)
                    {
                        case 1:
                            Deposito(indCuenta);
                            bucle = true;
                            break;
                        case 2:
                            TurnoExtraccion(indCuenta);
                            bucle = true;
                            break;
                        case 3:
                            CrearPlazoFijo(indCuenta);
                            bucle = true;
                            break;
                        case 4:
                            PagarServicios(indCuenta);
                            bucle = true;
                            break;
                        case 5:
                            Consulta(indCuenta);
                            bucle = true;
                            break;
                        case 6:
                            bucle = false;
                            break;
                        default:
                            bucle = true;
                            break;
                    }
                } while (bucle);
            }
        }

        static void Deposito (int n)
        {
            bool bucle = new bool();
            float monto = new float();
            do
            {
                ImprimirCabecera("Depositar dinero");
                Console.Write("Ingrese la cantidad de dinero que quiere añadir a la cuenta:\n> ");
                monto = PedirFloat();
                if (monto > 0)
                {
                    bucle = false;
                    listaClientes[n].saldo += monto;
                    Console.Write("Monto añadido con éxito. Presione cualquier tecla para salir...");
                } else
                {
                    bucle = true;
                    Console.WriteLine("El monto no puede ser menor a $0. Intente de nuevo...");
                    Console.ReadKey();
                }
            } while (bucle);
            Console.ReadKey();
        }

        static void TurnoExtraccion(int n)
        {
            ImprimirCabecera("Extraer dinero");
            bool denegado = Cliente.loginPIN(listaClientes[n]);
            if (!denegado)
            {
                Extraccion e = Extraccion.SacarTurno(listaClientes[n]);
                string a = ("Turno para " + e.cliente.apellido + ", " + e.cliente.nombre);
                string c = ("El cliente " + e.cliente.apellido + ", " + e.cliente.nombre + " ha sacado turno para extraer $" + e.monto + " de la cuenta N°" + e.cliente.nroCuenta + " el día " + e.fechaRetiro.ToString("D") + ".<br /><br />Muchas gracias por confiar en nuestro sistema.<br /><br />-Banco ATM");
                MandarEmail(listaClientes[n], a, c);
                Console.Write("Turno creado con éxito. E-mail de confirmación envíado.\nN° de cuenta: " + e.cliente.nroCuenta + "\nPresione cualquier tecla para salir...");
                Console.ReadKey();
            }
            else
            {
                Console.Write("Acceso denegado. Presione cualquier tecla para salir...");
                Console.ReadKey();
            }
        }

        static void CrearPlazoFijo(int n)
        {
            PlazoFijo pF = new PlazoFijo();
            if(listaClientes[n].pFijo == false)
            {
                pF = PlazoFijo.LlenarDatos(listaClientes[n]);
            }
            else
            {
                Console.Write("Ya hay un plazo fijo activo para esta cuenta. Solo puede haber un plazo fijo por cuenta.\nPresione cualquier tecla para salir...");
            }
            listaPlazos.Add(pF);
        }

        static void PagarServicios(int n)
        {
            listaClientes[n] = Cliente.PagarServicios(listaClientes[n]);
            Console.Write("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }

        static void Consulta(int n)
        {
            ImprimirCabecera("Consultar saldo");
            Cliente.Consulta(listaClientes[n]);
            Console.Write("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            LeerArchivos();
            MenuPrincipal();
            EscribirArchivos();
        }
    }
}