using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SelfHostedService
{
	
    [ServiceContract]
    public interface IService 
    {
        //GET METHODS
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Cliente/{id}",           ResponseFormat = WebMessageFormat.Json)]
        string get_Cliente(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Producto/{id}",           ResponseFormat = WebMessageFormat.Json)]
        string get_Producto(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Categoria/{id}",           ResponseFormat = WebMessageFormat.Json)]
        string get_Categoria(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Empleado/{id}",           ResponseFormat = WebMessageFormat.Json)]
        string get_Empleado(string id);
        
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Provedor/{id}",           ResponseFormat = WebMessageFormat.Json)]
        string get_Provedor(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Pedido/{id}",           ResponseFormat = WebMessageFormat.Json)]
        string get_Pedido(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/PedidoCliente/{id}",           ResponseFormat = WebMessageFormat.Json)]
        string get_PedidoCliente(string id);


        //POST METHODS
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Cliente/Create/{json}",
   RequestFormat = WebMessageFormat.Json)]
        void crear_Cliente(string json);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Producto/Create/{json}",
   RequestFormat = WebMessageFormat.Json)]
        void crear_Producto(string json);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Categoria/Create/{json}",
   RequestFormat = WebMessageFormat.Json)]
        void crear_Categoria(string json);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Empleado/Create/{json}",
   RequestFormat = WebMessageFormat.Json)]
        void crear_Empleado(string json);
        
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Provedor/Create/{json}",
   RequestFormat = WebMessageFormat.Json)]
        void crear_Provedor(string json);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Pedido/Create/{json}",
   RequestFormat = WebMessageFormat.Json)]
        void crear_Pedido(string json);

        //PUT METHODS
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/Cliente/Update/{id}/{campo}/{newvalue}")]
        void update_Cliente(string id, string campo, string newvalue);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/Producto/Update/{id}/{campo}/{newvalue}")]
        void update_Producto(string id, string campo, string newvalue);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/Categoria/Update/{id}/{campo}/{newvalue}")]
        void update_Categoria(string id, string campo, string newvalue);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/Empleado/Update/{id}/{campo}/{newvalue}")]
        void update_Empleado(string id, string campo, string newvalue);
        
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/Provedor/Update/{id}/{campo}/{newvalue}")]
        void update_Provedor(string id, string campo, string newvalue);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/Pedido/Update/{id}/{campo}/{newvalue}")]
        void update_Pedido(string id, string campo, string newvalue);

        //PUT METHODS
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/Cliente/Delete/{id}")]
        void eliminar_Cliente(string id);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/Producto/Delete/{id}")]
        void eliminar_Producto(string id);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/Categoria/Delete/{id}")]
        void eliminar_Categoria(string id);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/Empleado/Delete/{id}")]
        void eliminar_Empleado(string id);
        
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/Provedor/Delete/{id}")]
        void eliminar_Proovedor(string id);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/Pedido/Delete/{id}/{hora}")]
        void eliminar_Pedido(string id, string hora);

    }
	
	
	
    public class Service : IService
    {
        //Function of the GET methods
        public string get_Cliente(string id){
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Cliente cliente = DBConnection.get_Cliente(Int32.Parse(id));
            if (cliente == null)
            {
                return "Empleado no encontrado";
            }
            return JsonConvert.SerializeObject(cliente);
        }

        public string get_Producto(string id){
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Producto producto = DBConnection.get_Producto(id);
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

        //Function of the POST methods
        public void crear_Cliente(string json)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Cliente cliente = JsonConvert.DeserializeObject<Company.Cliente>(json);//Deserializa el dato a un objeto
            DBConnection.crear_Cliente(cliente);
        }

        public void crear_Producto(string json)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Producto producto = JsonConvert.DeserializeObject<Company.Producto>(json);//Deserializa el dato a un objeto
            DBConnection.crear_Producto(producto);
        }

        public void crear_Categoria(string json)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Categoria categoria = JsonConvert.DeserializeObject<Company.Categoria>(json);//Deserializa el dato a un objeto
            DBConnection.crear_Categoria(categoria);
        }

        public void crear_Empleado(string json)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Empleado empleado = JsonConvert.DeserializeObject<Company.Empleado>(json);//Deserializa el dato a un objeto
            DBConnection.crear_Empleado(empleado);
        }

        public void crear_Provedor(string json)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Proovedor provedor = JsonConvert.DeserializeObject<Company.Proovedor>(json);//Deserializa el dato a un objeto
            DBConnection.crear_Provedor(provedor);
        }

        public void crear_Pedido(string json)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            Company.Pedido pedido = JsonConvert.DeserializeObject<Company.Pedido>(json);//Deserializa el dato a un objeto
            DBConnection.crear_Pedido(pedido);
        }

        //Function of the PUT methods
        public void update_Cliente(string id, string campo, string newvalue)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            if (campo == "nombre"){
                DBConnection.update_Nombre_Cliente(Int32.Parse(id), newvalue);
            }
            else if (campo == "apellido"){
                DBConnection.update_Apellido_Cliente(Int32.Parse(id), newvalue);
            }
            else if (campo == "penalizacion"){
                DBConnection.update_Penalizacion_Cliente(Int32.Parse(id), Byte.Parse(id));
            }
            else if (campo == "residencia"){
                DBConnection.update_Residencia_Cliente(Int32.Parse(id), newvalue);
            }
            else if (campo == "nacimiento"){
                DBConnection.update_Nacimiento_Cliente(Int32.Parse(id), newvalue);
            }
            else if (campo == "telefono"){
                DBConnection.update_Telefono_Cliente(Int32.Parse(id), Int32.Parse(newvalue));
            }
        }

        public void update_Producto(string id, string campo, string newvalue)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            if (campo == "nombre"){
                DBConnection.update_Nombre_Producto(Int32.Parse(id), newvalue);
            }
            else if(campo == "descripcion"){
                DBConnection.update_Descripcion_Producto(Int32.Parse(id), newvalue);
            }
            else if(campo == "cantidad"){
                DBConnection.update_Cantidad_Producto(Int32.Parse(id), Int32.Parse(newvalue));
            }
        }

        public void update_Categoria(string id, string campo, string newvalue)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            if (campo == "descripcion"){
                DBConnection.update_Descripcion_Categoria(id, newvalue);
            }
        }

        public void update_Empleado(string id, string campo, string newvalue)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            if (campo == "nombre"){
                DBConnection.update_Nombre_Empleado(Int32.Parse(id), newvalue);
            }
            else if(campo == "sucursal"){
                DBConnection.update_Sucursal_Empleado(Int32.Parse(id), newvalue);
            }
            else if(campo == "puesto"){
                DBConnection.update_Puesto_Empleado(Int32.Parse(id), newvalue);
            }
        }

        public void update_Provedor(string id, string campo, string newvalue)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            if (campo == "nombre"){
                DBConnection.update_Nombre_Proovedor(Int32.Parse(id), newvalue);
            }
            else if(campo == "apellidos"){
                DBConnection.update_Apellidos_Proovedor(Int32.Parse(id), newvalue);
            }
            else if(campo == "nacimiento"){
                DBConnection.update_Nacimiento_Proovedor(Int32.Parse(id), newvalue);
            }
            else if(campo == "residencia"){
                DBConnection.update_Residencia_Proovedor(Int32.Parse(id), newvalue);
            }
        }

        public void update_Pedido(string id, string campo, string newvalue){
            //No hay metodo implementado para cambiar los datos de un pedido
        }


        //Function of the DELETE methods
        public void eliminar_Cliente(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            DBConnection.eliminar_Cliente(Int32.Parse(id));
        }

        public void eliminar_Producto(string id)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            DBConnection.eliminar_Producto(Int32.Parse(id));
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

        public void eliminar_Pedido(string id, string hora)
        {
            DatabaseConnection.Connection DBConnection = new DatabaseConnection.Connection();
            DBConnection.eliminar_Pedido(Int32.Parse(id), Int32.Parse(hora));
        }


    }
	
	
}