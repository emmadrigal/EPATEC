using System;
using System.Net;
using System.Text;
using System.Threading;

class principal
{
    static void Main()
    {
        //Clase encargado de recibir datos por metodos http
        HttpListener listener = new HttpListener();

        listener.Prefixes.Add("http://*:21005/");
        listener.Prefixes.Add("http://+:21005/");
        listener.Start();
        Console.Write("Listening on ...");//Indica en consola el puerto
        for (;;)//Esperapor nuevas conexiones y crea conexiones de acuerdo a ello
        {
            HttpListenerContext ctx = listener.GetContext();
            new Thread(new Director(ctx).ProcessRequest).Start();
        }
        Console.Read();
    }


    //Este método permite obtener el ip actual de la computadora
    public static string GetLocalIPAddress()
    {
        string externalip = new WebClient().DownloadString("http://icanhazip.com");
        return externalip;
    }

    //Clase mediante la cual se manejan las conexiones
    public class Director
    {
        private HttpListenerContext context;
        private Service.Service servicio = new Service.Service();

        //Constructor con un contexto que representa el request hecho por el cliente
        public Director(HttpListenerContext context)
        {
            this.context = context;
        }

        //Método encargado de manejar las solicitudes
        public void ProcessRequest()
        {
            string method = context.Request.HttpMethod;
            string response;

            //Segun el método se evalua de distinta manera
            if (method == "GET")
            {
                string type;
                if (context.Request.Url.Segments.Length > 1)//Checkea si hay al menos un dato mediante el cual se pueda obtener el proceso a realizar
                    type = context.Request.Url.Segments[1];
                else
                    type = "";//Al asignarlo de esta manera se evita evaluar otro dato

                //Evalua la solicitud de acuerdo a los datos dados, en caso de que no se provean todos los datos o se provean más de los necesarios se envia un mensaje de error
                if (type == "Cliente/")
                {
                    if (context.Request.Url.Segments.Length == 3)
                        response = servicio.get_Cliente(context.Request.Url.Segments[2]);
                    else
                        response = "Incorrect number of attributes";
                }
                else if (type == "Producto/")
                {
                    if (context.Request.Url.Segments.Length == 3)
                        response = servicio.get_Producto(context.Request.Url.Segments[2]);
                    else
                        response = "Incorrect number of attributes";
                }
                else if (type == "Categoria/")
                {
                    if (context.Request.Url.Segments.Length == 3)
                        response = servicio.get_Categoria(context.Request.Url.Segments[2]);
                    else
                        response = "Incorrect number of attributes";
                }
                else if (type == "Empleado/")
                {
                    if (context.Request.Url.Segments.Length == 3)
                        response = servicio.get_Empleado(context.Request.Url.Segments[2]);
                    else
                        response = "Incorrect number of attributes";
                }
                else if (type == "Proveedor/")
                {
                    if (context.Request.Url.Segments.Length == 3)
                        response = servicio.get_Provedor(context.Request.Url.Segments[2]);
                    else
                        response = "Incorrect number of attributes";
                }
                else if (type == "Pedido/")
                {
                    if (context.Request.Url.Segments.Length == 3)
                        response = servicio.get_Pedido(context.Request.Url.Segments[2]);
                    else
                        response = "Incorrect number of attributes";
                }
                else if (type == "PedidoCliente/")
                {
                    if (context.Request.Url.Segments.Length == 3)
                        response = servicio.get_PedidoCliente(context.Request.Url.Segments[2]);
                    else
                        response = "Incorrect number of attributes";
                }
                else if (type == "getAllClients")
                {
                    response = servicio.get_AllClients();
                }
                else if (type == "getAllProducts")
                {
                    response = servicio.get_AllProducts();
                }
                else if (type == "getAllCategories")
                {
                    response = servicio.get_AllCategories();
                }
                else if (type == "getAllProviders")
                {
                    response = servicio.get_AllProviders();
                }
                else if (type == "getAllEmployees")
                {
                    response = servicio.get_AllEmployees();
                }
                else if (type == "getAllProductsCat/")
                {
                    if (context.Request.Url.Segments.Length == 3)
                        response = servicio.get_AllProductsCat(context.Request.Url.Segments[2]);
                    else
                        response = "Incorrect number of attributes";
                }
                else if (type == "getAllPedidosSuc/")
                {
                    if (context.Request.Url.Segments.Length == 3)
                        response = servicio.get_AllPedidosSuc(context.Request.Url.Segments[2]);
                    else
                        response = "Incorrect number of attributes";
                }
                else if (type == "getAllProductosProveedor/")
                {
                    if (context.Request.Url.Segments.Length == 3)
                        response = servicio.get_AllPedidosProv(context.Request.Url.Segments[2]);
                    else
                        response = "Incorrect number of attributes";
                }
                //Para el modulo de estadística
                else if (type == "getProductosMasVendidos")
                {
                    response = servicio.get_TopPedidos();
                }

                else if (type == "getTopProductosSucursal/")
                {
                    if (context.Request.Url.Segments.Length == 3)
                        response = servicio.getTopProductosSuc(context.Request.Url.Segments[2]);
                    else
                        response = "Incorrect number of attributes";
                }
                else if (type == "getVentasSucursal/")
                {
                    if (context.Request.Url.Segments.Length == 3)
                        response = servicio.get_VentasSucursal(context.Request.Url.Segments[2]);
                    else
                        response = "Incorrect number of attributes";
                }
                else
                {
                    response = "Objeto no identificado";
                }
            }
            else if (method == "POST")
            {
                if (context.Request.Url.Segments.Length == 3)//Se asegura que se obtengan todos los datos necesarios
                {
                    string type = context.Request.Url.Segments[1];
                    response = "Solicitud de creación procesada";
                    //Evalua la solicitud de acuerdo a los datos dados, en caso de que no se provean todos los datos o se provean más de los necesarios se envia un mensaje de error
                    if (type == "Cliente/")
                    {
                        servicio.crear_Cliente(context.Request.Url.Segments[2]);
                    }
                    else if (type == "Producto/")
                    {
                        servicio.crear_Producto(context.Request.Url.Segments[2]);
                    }
                    else if (type == "Categoria/")
                    {
                        servicio.crear_Categoria(context.Request.Url.Segments[2]);
                    }
                    else if (type == "Empleado/")
                    {
                        servicio.crear_Empleado(context.Request.Url.Segments[2]);
                    }
                    else if (type == "Proveedor/")
                    {
                        servicio.crear_Provedor(context.Request.Url.Segments[2]);
                    }
                    else if (type == "Pedido/")
                    {
                        servicio.crear_Pedido(context.Request.Url.Segments[2]);
                    }
                    else
                    {
                        response = "Objeto no identificado";
                    }
                }
                else
                {
                    response = "Incorrect number of attributes";
                }

            }
            else if (method == "PUT")
            {
                Console.WriteLine(context.Request.RawUrl);
                if (context.Request.Url.Segments.Length == 5)//Se asegura que se obtengan todos los datos necesarios
                {
                    string type = context.Request.Url.Segments[1];
                    response = "Solicitud de update procesada";
                    //Evalua la solicitud de acuerdo a los datos dados, en caso de que no se provean todos los datos o se provean más de los necesarios se envia un mensaje de error
                    if (type == "Cliente/")
                    {
                        servicio.update_Cliente(context.Request.Url.Segments[2], context.Request.Url.Segments[3], context.Request.Url.Segments[4]);
                    }
                    else if (type == "Producto/")
                    {
                        servicio.update_Producto(context.Request.Url.Segments[2], context.Request.Url.Segments[3], context.Request.Url.Segments[4]);
                    }
                    else if (type == "Categoria/")
                    {
                        servicio.update_Categoria(context.Request.Url.Segments[2], context.Request.Url.Segments[3], context.Request.Url.Segments[4]);
                    }
                    else if (type == "Empleado/")
                    {
                        servicio.update_Empleado(context.Request.Url.Segments[2], context.Request.Url.Segments[3], context.Request.Url.Segments[4]);
                    }
                    else if (type == "Proveedor/")
                    {
                        servicio.update_Provedor(context.Request.Url.Segments[2], context.Request.Url.Segments[3], context.Request.Url.Segments[4]);
                    }
                    else if (type == "Pedido/")
                    {
                        servicio.update_Pedido(context.Request.Url.Segments[2], context.Request.Url.Segments[3], context.Request.Url.Segments[4]);
                    }
                    else
                    {
                        response = "Objeto no identificado";
                    }
                }
                else
                {
                    response = "Incorrect number of attributes";
                }

            }
            else if (method == "DELETE")
            {
                if (context.Request.Url.Segments.Length == 3)//Se asegura que se obtengan todos los datos necesarios
                {
                    string type = context.Request.Url.Segments[1];
                    response = "Solicitud de eliminacion procesada";
                    //Evalua la solicitud de acuerdo a los datos dados, en caso de que no se provean todos los datos o se provean más de los necesarios se envia un mensaje de error
                    if (type == "Cliente/")
                    {
                        servicio.eliminar_Cliente(context.Request.Url.Segments[2]);
                    }
                    else if (type == "Producto/")
                    {
                        servicio.eliminar_Producto(context.Request.Url.Segments[2]);
                    }
                    else if (type == "Categoria/")
                    {
                        servicio.eliminar_Categoria(context.Request.Url.Segments[2]);
                    }
                    else if (type == "Empleado/")
                    {
                        servicio.eliminar_Empleado(context.Request.Url.Segments[2]);
                    }
                    else if (type == "Proveedor/")
                    {
                        servicio.eliminar_Proovedor(context.Request.Url.Segments[2]);
                    }
                    else if (type == "Pedido/")
                    {
                        servicio.eliminar_Pedido(context.Request.Url.Segments[2]);
                    }
                    else
                    {
                        response = "Objeto no identificado";
                    }
                }
                else
                {
                    response = "Incorrect number of attributes";
                }

            }
            else
            {
                response = "Non supported HTML Method";
            }

            //Se envia la respuesta de vuelta al cliente, en caso de ser un GET, se devuelve un json, si este falla o es alguno de los otros métodos se envia un mensaje indicando el éxito 
            byte[] b = Encoding.UTF8.GetBytes(response);
            context.Response.ContentLength64 = b.Length;
            context.Response.OutputStream.Write(b, 0, b.Length);
            context.Response.OutputStream.Close();
        }
    }

}