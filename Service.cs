using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;

namespace Service
{
    public class Service
    {
        private ErrorHandler.ErrorHandler err;//Used to store error messages in case of desired debugging


        public Service()
        {
            err = new ErrorHandler.ErrorHandler();
        }

        /*Function of the GET methods
		These functions receive the id of the desired member and the call the database,
		if no member is found it returns a null, so this class parses that into an appropiate msg
		
		In order to construct a json to send to the DB this classes use the Json.NET library which knowing the attributes of aclass create the corresponding json string
		*/
        public string get_Cliente(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Cliente cliente = DBConnection.get_Cliente(Int32.Parse(id));
            if (cliente == null)
            {
                return "Empleado no encontrado";
            }
            return JsonConvert.SerializeObject(cliente);
        }

        public string get_AllClients()
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.Cliente> clientes = DBConnection.get_AllClientes();
            if (clientes == null)
            {
                return "No hay clientes en existencia";
            }
            return JsonConvert.SerializeObject(clientes);
        }

        public string get_Producto(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.Producto> producto = DBConnection.get_Producto(id);
            if (producto == null)
            {
                return "Empleado no encontrado";
            }
            return JsonConvert.SerializeObject(producto);
        }

        public string get_Categoria(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Categoria categoria = DBConnection.get_Categoria(id);

            if (categoria == null)
            {
                return "Categoria no encontrada";
            }

            string Output = JsonConvert.SerializeObject(categoria);
            return Output;
        }

        public string get_Empleado(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Empleado emp = DBConnection.get_Empleado(Int32.Parse(id));
            if (emp == null)
            {
                return "Empleado no Encontrado";
            }
            string Output = JsonConvert.SerializeObject(emp);
            return Output;

        }

        public string get_Provedor(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Proovedor provedor = DBConnection.get_Provedor(Int32.Parse(id));
            if (provedor == null)
            {
                return "Provedor no encontrado";
            }
            return JsonConvert.SerializeObject(provedor);
        }

        internal string get_AllProductsCat(string v)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.Producto> productos = DBConnection.get_AllProducsCat(v);
            if (productos == null)
            {
                return "No hay productos en esta categoria";
            }
            return JsonConvert.SerializeObject(productos);
        }

        internal string get_TopPedidos()
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.ProductosVentas> productos = DBConnection.get_TopProductos();
            if (productos == null)
            {
                return "No hay productos";
            }
            return JsonConvert.SerializeObject(productos);
        }

        internal string get_AllPedidosProv(string provedor)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.Producto> productos = DBConnection.get_AllProductsProv(provedor);
            if (productos == null)
            {
                return "No hay productos";
            }
            return JsonConvert.SerializeObject(productos);
        }

        internal string get_VentasSucursal(string sucursal)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.SucursalVentas> productos = DBConnection.get_VentasSucursal(sucursal);
            if (productos == null)
            {
                return "No hay productos";
            }
            return JsonConvert.SerializeObject(productos);
        }

        internal string getTopProductosSuc(string sucursal)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.ProductosVentas> productos = DBConnection.get_TopProductosSuc(sucursal);
            if (productos == null)
            {
                return "No hay productos";
            }
            return JsonConvert.SerializeObject(productos);
        }

        internal string get_AllEmployees()
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.Empleado> employees = DBConnection.get_AllEmployees();
            if (employees == null)
            {
                return "No hay categorias";
            }
            return JsonConvert.SerializeObject(employees);
        }

        internal string get_AllCategories()
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.Categoria> categories = DBConnection.get_AllCategories();
            if (categories == null)
            {
                return "No hay categorias";
            }
            return JsonConvert.SerializeObject(categories);
        }

        internal string get_AllProviders()
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.Proovedor> providers = DBConnection.get_AllProviders();
            if (providers == null)
            {
                return "No hay provedores";
            }
            return JsonConvert.SerializeObject(providers);
        }

        internal string get_AllProducts()
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.Producto> productos = DBConnection.get_AllProducts();
            if (productos == null)
            {
                return "No hay productos";
            }
            return JsonConvert.SerializeObject(productos);
        }

        internal string get_AllPedidosSuc(string v)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.Pedido> pedidos = DBConnection.get_AllPedidosSuc(v);
            if (pedidos == null)
            {
                return "No hay pedidos en esta sucursal";
            }
            return JsonConvert.SerializeObject(pedidos);
        }

        public string get_Pedido(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Pedido pedido = DBConnection.get_Pedido(Int32.Parse(id));
            if (pedido == null)
            {
                return "Pedido no encontrado";
            }
            return JsonConvert.SerializeObject(pedido);
        }

        public string get_PedidoCliente(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            List<Company.Pedido> pedidos = DBConnection.get_PedidoCliente(Int32.Parse(id));
            if (pedidos == null)
            {
                return "Cliente no encontrado";
            }
            return JsonConvert.SerializeObject(pedidos);
        }

        /*Function of the POST methods
		These methods recieve a json of the appropiate type, this class tries to parse it back into the appropiate type, if it failes it sends a msg 
		Since the json's are recieved through the url and not the Body, there us also a need to decode the URL enconding.
		
		This methods don't have a response
		
		*/
        public void crear_Cliente(string json)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            try
            {
                json = HttpUtility.UrlDecode(json);
                Company.Cliente cliente = JsonConvert.DeserializeObject<Company.Cliente>(json);//Deserializa el dato a un objeto
                DBConnection.crear_Cliente(cliente);
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
            }
        }

        public void crear_Producto(string json)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            try
            {
                json = HttpUtility.UrlDecode(json);
                Company.Producto producto = JsonConvert.DeserializeObject<Company.Producto>(json);//Deserializa el dato a un objeto
                DBConnection.crear_Producto(producto);
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
            }

        }

        public void crear_Categoria(string json)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            try
            {
                json = HttpUtility.UrlDecode(json);
                Company.Categoria categoria = JsonConvert.DeserializeObject<Company.Categoria>(json);//Deserializa el dato a un objeto
                DBConnection.crear_Categoria(categoria);
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
            }

        }

        public void crear_Empleado(string json)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            try
            {
                json = HttpUtility.UrlDecode(json);
                Company.Empleado empleado = JsonConvert.DeserializeObject<Company.Empleado>(json);//Deserializa el dato a un objeto
                DBConnection.crear_Empleado(empleado);
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
            }

        }

        public void crear_Provedor(string json)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            try
            {
                json = HttpUtility.UrlDecode(json);
                Company.Proovedor provedor = JsonConvert.DeserializeObject<Company.Proovedor>(json);//Deserializa el dato a un objeto
                DBConnection.crear_Provedor(provedor);
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
            }

        }

        public void crear_Pedido(string json)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            try
            {
                json = HttpUtility.UrlDecode(json);
                Company.Pedido pedido = JsonConvert.DeserializeObject<Company.Pedido>(json);//Deserializa el dato a un objeto

                DBConnection.crear_Pedido(pedido);
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
            }

        }


        /*Function of the PUT methods
		These methods are in charge of the update of records on the DB
		They recieve all of the data from the webApp as stablished by the procotol
		*/
		//Update for the user table
        public void update_Cliente(string id, string campo, string newvalue)
        {
            id = id.Remove(id.Length - 1);//Deletes the last \ in order to only have the useful data.
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();

            if (campo == "nombre/")
            {
                DBConnection.update_Nombre_Cliente(Int32.Parse(id), newvalue);
            }
            else if (campo == "apellido/")
            {
                DBConnection.update_Apellido_Cliente(Int32.Parse(id), newvalue);
            }
            else if (campo == "penalizacion/")
            {
                DBConnection.update_Penalizacion_Cliente(Int32.Parse(id), Byte.Parse(newvalue));
            }
            else if (campo == "residencia/")
            {
                DBConnection.update_Residencia_Cliente(Int32.Parse(id), newvalue);
            }
            else if (campo == "nacimiento/")
            {
                DBConnection.update_Nacimiento_Cliente(Int32.Parse(id), newvalue);
            }
            else if (campo == "telefono/")
            {
                DBConnection.update_Telefono_Cliente(Int32.Parse(id), Int32.Parse(newvalue));
            }
        }
		
		//Update for the producto table
        public void update_Producto(string name, string campo, string newvalue)
        {
            name = name.Remove(name.Length - 1);
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            if (campo == "nombre/")
            {
                DBConnection.update_Nombre_Producto(name, newvalue);
            }
            else if (campo == "descripcion/")
            {
                DBConnection.update_Descripcion_Producto(name, newvalue);
            }
            else if (campo == "cantidad/")
            {
                DBConnection.update_Cantidad_Producto(name, Int32.Parse(newvalue));
            }
        }

		//Update for the categoria table
        public void update_Categoria(string id, string campo, string newvalue)
        {
            id = id.Remove(id.Length - 1);
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            if (campo == "descripcion/")
            {
                DBConnection.update_Descripcion_Categoria(id, newvalue);
            }
        }

		//Update for the employee table
        public void update_Empleado(string id, string campo, string newvalue)
        {
            id = id.Remove(id.Length - 1);
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            if (campo == "nombre/")
            {
                DBConnection.update_Nombre_Empleado(Int32.Parse(id), newvalue);
            }
            else if (campo == "sucursal/")
            {
                DBConnection.update_Sucursal_Empleado(Int32.Parse(id), newvalue);
            }
            else if (campo == "puesto/")
            {
                DBConnection.update_Puesto_Empleado(Int32.Parse(id), newvalue);
            }
        }

		//Update for the supplier table
        public void update_Provedor(string id, string campo, string newvalue)
        {
            id = id.Remove(id.Length - 1);
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            if (campo == "nombre/")
            {
                DBConnection.update_Nombre_Proovedor(Int32.Parse(id), newvalue);
            }
            else if (campo == "apellidos/")
            {
                DBConnection.update_Apellidos_Proovedor(Int32.Parse(id), newvalue);
            }
            else if (campo == "nacimiento/")
            {
                DBConnection.update_Nacimiento_Proovedor(Int32.Parse(id), newvalue);
            }
            else if (campo == "residencia/")
            {
                DBConnection.update_Residencia_Proovedor(Int32.Parse(id), newvalue);
            }
        }

		//Update for the pedido table
        public void update_Pedido(string id, string campo, string newvalue)
        {
            //No hay metodo implementado para cambiar los datos de un pedido
        }


        /*Function of the DELETE methods
		It recieves the primary key of the object that is to be destroyed
		*/
        public void eliminar_Cliente(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            DBConnection.eliminar_Cliente(Int32.Parse(id));
        }

        public void eliminar_Producto(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            DBConnection.eliminar_Producto(id);
        }

        public void eliminar_Categoria(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            DBConnection.eliminar_Categoria(id);
        }

        public void eliminar_Empleado(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            DBConnection.eliminar_Empleado(Int32.Parse(id));
        }

        public void eliminar_Proovedor(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            DBConnection.eliminar_Proovedor(Int32.Parse(id));
        }

        public void eliminar_Pedido(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            DBConnection.eliminar_Pedido(Int32.Parse(id));
        }

    }


}