using System;
using System.Collections.Generic;
using System.Linq;


//Lab 3 POO Manuel Serra SM
//RUT 199578057
namespace Lab3MSSM
{
    class Persona
    {
        //Defino atributos.
        public string nombre;
        public string apellido;
        public string rut;
        public string nacionalidad;
        public int diaNac;
        public int mesNac;
        public int añoNac;

        //Constructor de objeto tipo persona. 
        public Persona(string name, string lastName, string aRut, string nac, int dNac, int mNac, int aNac)
        {
            nombre = name; apellido = lastName; rut = aRut; nacionalidad = nac; diaNac = dNac; mesNac = mNac; añoNac = aNac;
        }


    }

    //Hago esto empleando herencia para simplificar la pega. 

    class Cliente : Persona
    {
        public int clientNumber;

        public Cliente(string nombre, string apellido, string rut, string nacionalidad, int diaNac, int mesNac, int añoNac, int numCliente) : base(nombre, apellido, rut, nacionalidad, diaNac, mesNac, añoNac)
        {
            clientNumber = numCliente;
        }


        public string getData() {
            string data = "Nombre; " + nombre + " Apellido: " + apellido + " Rut: " + rut + " Nacionalidad: " + nacionalidad + " Dia Nac: " + diaNac + " Mes Nac: " + mesNac + " Año Nac: " + añoNac;
            return data;
        }
    }

    class Empleado : Persona
    {
        // Un empleado tiene un numero identificador unico, un cargo y un sueldo.
        // al tener un horario, este tiene horario de entrada y de salida, los pido como parametros 
        // al construir el objeto y por comodidad, los guardo en un array de strings llamado Horario. 

        public int employeeNumber;
        public string cargo;
        public int sueldo;
        public string[] Horario = new string[3];
        public Empleado(string nombre, string apellido, string rut, string nacionalidad, int diaNac, int mesNac, int añoNac, int numEmp, string role, int pay, string startTime, string endTime) : base(nombre, apellido, rut, nacionalidad, diaNac, mesNac, añoNac)
        {
            employeeNumber = numEmp;
            cargo = role;
            sueldo = pay;
            Horario[1] = startTime;
            Horario[2] = endTime;
        }


        public string getData()
        {
            string data = "Nombre; " + nombre + " Apellido: " + apellido + " Rut: " + rut + " Nacionalidad: " + nacionalidad + " Dia Nac: " + diaNac + " Mes Nac: " + mesNac + " Año Nac: " + añoNac + " Numero: " + employeeNumber + " Cargo: " + cargo + " Sueldo: " + sueldo + " Horario " + Horario[1] + "-"+ Horario[2];
            return data;
        }



    }

    class Producto
    {
        // Defino atributos propios a un producto. 
        public string nombre;
        public int precio;
        public string marca;

        public bool haSidoComprado; 

        // Ahora el constructor. 
        public Producto(string n, int p, string m)
        {
            nombre = n; precio = p; marca = m;
            haSidoComprado = false;
        }

        public string getInfoString ()
        {
            string info = " Nombre producto: " + nombre + " Marca: " + marca + " Precio " + precio;
            return info;

        }
    }

    class Compra
    {
        // Una compra tiene atributos de fecha, un cliente que compra los productos 
        // a cada compra, le entrego una lista de productos. 

        public Cliente buyer;
        public Empleado cajero;
        public int year;
        public int month;
        public int day;
        public int hour;
        public List<Producto> listaCompra = new List<Producto>();

        int montoTotal;

        // Constructor de una compra: Ingreso datos y compra parte siendo vacia. 
        public Compra(Cliente buyer, Empleado cajero, int year, int month, int day, int hour)
        {
            this.buyer = buyer; this.year = year; this.month = month; this.day = day;
            this.hour = hour; this.cajero = cajero;
            montoTotal = 0;
        }
        // Funcion para agregar un producto a la compra.
        // Me fijo que todos los objetos que propongo comprar no hayan sido comprados antes.
        // Sumo precio de este producto a monto de compra. 
        public bool addProduct(Producto p)
        {
            if (p.haSidoComprado == false) {
                listaCompra.Add(p);
                montoTotal += p.precio;
                p.haSidoComprado = true;
            }
            if (p.haSidoComprado == true)
            {
                return false;
            }

            return true;
        }

        public string genBoleta()
        {
            string bol = "Año: " + year + " Mes: " + month + " Dia: " + day + " Hora: " + hour + " Monto tot: " + montoTotal + " Cajero: " + cajero.nombre + cajero.apellido;

            int z = 0;
            while (z < listaCompra.Count())
            {
                string data = listaCompra[z].getInfoString();
                bol = bol + " " + data;
                z++;
            }
            return bol;
        }
    }

    class Gestor
    {
        // Genero listas con todos los productos, todos los clientes y todos los empleados.
        public List<Producto> todoStock;
        public List<Cliente> todosClientes;
        public List<Empleado> todosEmpleados;
        public List<Compra> todasCompras;

        public List<string> productTypes;
        public List<int> productCount;
    
        // Constructor del gestor me genera listas vacias de productos, clientes, empleados y compras. 
        public Gestor()
        {
            this.todoStock = new List<Producto>(); this.todosClientes = new List<Cliente>(); 
            this.todosEmpleados = new List<Empleado>(); this.todasCompras = new List<Compra>();
            this.productTypes = new List<String>(); this.productCount = new List<int>();
        } 
        public void addClient (Cliente c)
        {
            todosClientes.Add(c);
        }
        public void addEmployee (Empleado e)
        {
            todosEmpleados.Add(e);
        }
        public void agregarProd(Producto p)
        {
            todoStock.Add(p);
        }
        public void agregarCompra (Compra c)
        {
            todasCompras.Add(c);
        }


        public void verListaProductos()
        {
            int z = 0;
            while (z < todoStock.Count())
            {
                if (todoStock[z].haSidoComprado == false)
                {
                    Console.WriteLine(z + ") Nombre: " + todoStock[z].nombre + " Precio: " + todoStock[z].precio);
                }
                z++;
            }
        }



        public bool verProductos()
        {
            //Reseteo lista.

            productTypes.Clear();
            productCount.Clear();

            // Primero extraigo los nombres de los productos. 

            int i = 0;
            while (i < todoStock.Count()) {
                string locName = todoStock[i].nombre;

                if (todoStock[i].haSidoComprado == false)
                {
                    if (!productTypes.Contains(locName))
                    {
                        productTypes.Add(locName);
                        productCount.Add(0);
                    }
                    i++;
                }
            }

            // Ahora veo cuantos de cada uno hay.

            i = 0;
            while(i < productTypes.Count())
            {
                int k = 0;
                while (k < todoStock.Count())
                {
                    if (string.Compare(productTypes[i], todoStock[k].nombre) == 0 & todoStock[k].haSidoComprado == false)
                        productCount[i] += 1; 
                    k++;
                }
                i++;
            }

            // Se las muestro al usuario.

            int n = 0;
            while (n < productTypes.Count())
            {
                Console.WriteLine(productTypes[n]);
                Console.WriteLine(productCount[n]);
                n++;
            }
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Probemos todo esto:

            //Creo gestor. 

            Gestor super = new Gestor();

            //Creo algunos productos, clientes y empleados para jugar un poco. 

            Producto Pan1 = new Producto("Pan", 500, "Soprole"); super.agregarProd(Pan1);
            Producto Pan2 = new Producto("Pan", 500, "Soprole"); super.agregarProd(Pan2);
            Producto Pisco1 = new Producto("Pisco", 5000, "Alto"); super.agregarProd(Pisco1);
            Producto Pan3 = new Producto("Pan", 500, "Soprole"); super.agregarProd(Pan3);
            Producto Pan4 = new Producto("Pan", 500, "Soprole"); super.agregarProd(Pan4);
            Producto Pisco2 = new Producto("Pisco", 5000, "Alto"); super.agregarProd(Pisco2);
            Producto Pisco3 = new Producto("Pisco", 5000, "Alto"); super.agregarProd(Pisco3);

            Cliente C1 = new Cliente("Manuel", "Serra", "199578057", "Chileno", 26, 06, 98, 1); super.addClient(C1);
            Cliente C2 = new Cliente("Pedro", "Sanchez", "199578058", "Chileno", 26, 07, 99, 2); super.addClient(C1);

            Empleado E1 = new Empleado("Lucho", "Perez", "189578078", "Chileno", 22, 03, 90, 1, "Cajero", 5800, "10:00", "18:00"); super.addEmployee(E1);
            Empleado E2 = new Empleado("Pedro", "Garcia", "199578058", "Chileno", 26, 07, 99, 1, "Cajero", 5000, "16:00", "22:00"); super.addEmployee(E2);

      

           
            //Primero defino algunos contadores que me seran utiles. 

            int clientNum = 0;
            int employeeNum = 0;

            // MENU.

            int userChoice = 0;
            while (userChoice == 0)
            {
                Console.WriteLine("Bienvenido Usuario !!");
                Console.WriteLine("Ingrese 1 para agregar cliente.");
                Console.WriteLine("Ingrese 2 para agregar empleado.");
                Console.WriteLine("Ingrese 3 para agregar producto.");
                Console.WriteLine("Ingrese 4 para agregar compra.");
                Console.WriteLine("Ingrese 5 para salir.");

                userChoice = Convert.ToInt32(Console.ReadLine());
                //1. Creo nuevo cliente. 

                while (userChoice == 1)
                {

                    Console.WriteLine("Clientes Actuales: ");
                    int k = 0; 
                    while (k < super.todosClientes.Count())
                    {
                        Console.WriteLine(k + "."); Console.WriteLine(super.todosClientes[k].getData());
                        k++;
                    }

                    Console.WriteLine("");

                    Console.WriteLine("Ingrese nombre: "); string nombre = Console.ReadLine();
                    Console.WriteLine("Ingrese apellido: "); string apellido = Console.ReadLine();
                    Console.WriteLine("Ingrese RUT: "); string rut = Console.ReadLine();
                    Console.WriteLine("Ingrese Nacionalidad: "); string nacionalidad = Console.ReadLine();
                    Console.WriteLine("Ingrese dia nacimiento: "); int diaNac = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ingrese mes nacimiento: "); int mesNac = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ingrese año nacimiento: "); int yearNac = Convert.ToInt32(Console.ReadLine());

                    
                    Cliente newClient = new Cliente(nombre, apellido, rut, nacionalidad, diaNac, mesNac, yearNac, clientNum);
                    clientNum += 1;

                    super.addClient(newClient);


                    Console.WriteLine("Ingrese 0 para salir");
                    userChoice = Convert.ToInt32(Console.ReadLine());
                    
                }

                //2. Creo nuevo empleado.

                while (userChoice == 2)
                {

                    Console.WriteLine("Empleados Actuales: ");
                    int k = 0;
                    while (k < super.todosEmpleados.Count())
                    {
                        Console.WriteLine(k + "."); Console.WriteLine(super.todosEmpleados[k].getData());
                        k++;
                    }

                    Console.WriteLine("Ingrese nombre: "); string nombre = Console.ReadLine();
                    Console.WriteLine("Ingrese apellido: "); string apellido = Console.ReadLine();
                    Console.WriteLine("Ingrese RUT: "); string rut = Console.ReadLine();
                    Console.WriteLine("Ingrese Nacionalidad: "); string nacionalidad = Console.ReadLine();
                    Console.WriteLine("Ingrese dia nacimiento: "); int diaNac = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ingrese mes nacimiento: "); int mesNac = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ingrese año nacimiento: "); int yearNac = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ingrese rol: "); string role = Console.ReadLine();
                    Console.WriteLine("Ingrese sueldo: "); int pay = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ingrese horario entrada: "); string startTime = Console.ReadLine();
                    Console.WriteLine("Ingrese horario salida: "); string endTime = Console.ReadLine();

                    Empleado newEmployee = new Empleado(nombre, apellido, rut, nacionalidad, diaNac, mesNac, yearNac, employeeNum, role, pay, startTime, endTime);
                    employeeNum += 1;

                    super.addEmployee(newEmployee);

                    Console.WriteLine("Ingrese 0 para salir");
                    userChoice = Convert.ToInt32(Console.ReadLine());
                }

                while(userChoice == 3)
                {

                    Console.WriteLine("Productos existentes: ");

                    super.verProductos();

                    Console.WriteLine("Ingrese nombre: "); string n = Console.ReadLine();
                    Console.WriteLine("Ingrese precio: "); int p = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ingrese marca: "); string m = Console.ReadLine();
      
                    Producto newProduct = new Producto(n, p, m);
                    super.agregarProd(newProduct);

                    Console.WriteLine("Ingrese 0 para salir");
                    userChoice = Convert.ToInt32(Console.ReadLine());

                }

                while (userChoice == 4)
                {
                    list<Producto> pc = new list<Producto>();

                    //Primero genero compra. 

                    Console.WriteLine("Ingrese 1 para generar compra, 0 para salir y 2 para ver compras.");
                    int locChoice = Convert.ToInt32(Console.ReadLine());

                    if (locChoice == 0)
                    {
                        userChoice = locChoice;
                    }

                    bool verCompras = false;

                    if(locChoice == 2)
                    {
                        verCompras = true;
                    }

                    while (verCompras == true)
                    {
                        int u = 0;
                        while (u < super.todasCompras.Count())
                        {
                            Console.WriteLine(super.todasCompras[u].genBoleta());
                            Console.WriteLine(Environment.NewLine);
                            u++;
                        }
                        verCompras = false;
                    }

                    bool compraNueva = true;
                    while (compraNueva == true & locChoice == 1) {

                        Console.WriteLine("Ingrese Num Cliente: "); int numCl = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Ingrese Num Empleado: "); int numCa = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Ingrese año "); int y = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Ingrese mes "); int m = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Ingrese dia: "); int d = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Ingrese hora: "); int h = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("");
                        Console.WriteLine("Productos disponibles: ");
                        super.verProductos();
                        Compra newC = new Compra(super.todosClientes[numCl], super.todosEmpleados[numCa], y, m, d, h);
                        super.agregarCompra(newC);

                        Console.WriteLine("Compra generada con exito.");
                        Console.WriteLine("Ingrese 1 para agregar nuevo producto: ");
                        Console.WriteLine("Ingrese 0 para salir");

                        Console.WriteLine(newC.genBoleta());

                        bool comprando = true;
                        while (comprando == true)
                        {
                            super.verListaProductos();
                            Console.WriteLine("Ingrese numero de prod. a agregar: "); int pChoice = Convert.ToInt32(Console.ReadLine());

                            newC.addProduct(super.todoStock[pChoice]);
                            Console.WriteLine("Prod. agregado con exito a compra.");
                            Console.WriteLine(newC.genBoleta());

                            Console.WriteLine("Ingrese 1 para agregar otro prod, 2 para salir:");
                            int sigoComprando = Convert.ToInt32(Console.ReadLine());
                            if (sigoComprando == 2)
                            {
                                locChoice = 0;
                                comprando = false;
                            }
                        }
                    }
                }
                // Opcion para terminar programa si asi lo deseo. 
                if (userChoice == 5)
                {
                    Environment.Exit(1);
                }
            }
           


        }
    }
}
