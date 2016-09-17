using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class principal
{
    static void Main()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:21005/");
        listener.Start();
        string msg = "Listening on " + GetLocalIPAddress() + "...";
        Console.WriteLine(msg);
        for (;;)
        {
            HttpListenerContext ctx = listener.GetContext();
            new Thread(new Director(ctx).ProcessRequest).Start();
        }
    }


    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("Local IP Address Not Found!");
    }

    public class Director 
    {
        private HttpListenerContext context;
        private Service.Service servicio = new Service.Service();
        public Director(HttpListenerContext context)
        {
            this.context = context;
        }
        public void ProcessRequest()
        {
            string method = context.Request.HttpMethod;
            if (method == "GET")
            {       
                string response;
                string type = context.Request.Url.Segments[1];
                if (type == "Cliente/")
                {
                    response = servicio.get_Cliente(context.Request.Url.Segments[2]);
                }
                else if (type == "Producto/")
                {
                    response = servicio.get_Producto(context.Request.Url.Segments[2]);
                }
                else if (type == "Categoria/")
                {
                    response = servicio.get_Categoria(context.Request.Url.Segments[2]);
                }
                else if (type == "Empleado/")
                {
                    response = servicio.get_Empleado(context.Request.Url.Segments[2]);
                }
                else if (type == "Proveedor/")
                {
                    response = servicio.get_Provedor(context.Request.Url.Segments[2]);
                }
                else if (type == "Pedido/")
                {
                    response = servicio.get_Pedido(context.Request.Url.Segments[2]);
                }
                else if (type == "PedidoCliente/")
                {
                    response = servicio.get_PedidoCliente(context.Request.Url.Segments[2]);
                }
                else if (type == "getAllClients")
                {
                    response = servicio.get_AllClients();
                }
                else if (type == "getAllProducts")
                {
                    response = servicio.get_AllProducts();
                }
                else if (type == "getAllProductsCat/")
                {
                    response = servicio.get_AllProductsCat(context.Request.Url.Segments[2]);
                }
                else if (type == "getAllPedidosSuc/")
                {
                    response = servicio.get_AllPedidosSuc(context.Request.Url.Segments[2]);
                }
                else if (type == "getAllProductosProveedor/")
                {
                    response = servicio.get_AllPedidosProv(context.Request.Url.Segments[2]);
                }
                //Para el modulo de estadística
                else if (type == "getProductosMasVendidos")
                {
                    response = servicio.get_TopPedidos();
                }

                else if (type == "getTopProductosSucursal/")
                {
                    response = servicio.getTopProductosSuc(context.Request.Url.Segments[2]);
                }
                else if (type == "getVentasSucursal/")
                {
                    response = servicio.get_VentasSucursal(context.Request.Url.Segments[2]);
                }

                else
                {
                    response = "Objeto no identificado";
                }

                byte[] b = Encoding.UTF8.GetBytes(response);
                context.Response.ContentLength64 = b.Length;
                context.Response.OutputStream.Write(b, 0, b.Length);
                context.Response.OutputStream.Close();

            }
            else if (method == "POST")
            {
                
                string type = context.Request.Url.Segments[1];
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
            }
            else if (method == "PUT")
            {
                string type = context.Request.Url.Segments[1];
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
            }
            else if (method == "DELETE")
            {
                string type = context.Request.Url.Segments[1];
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
            }
            else
            {
                byte[] b = Encoding.UTF8.GetBytes("Non supported HTML Method");
                context.Response.ContentLength64 = b.Length;
                context.Response.OutputStream.Write(b, 0, b.Length);
                context.Response.OutputStream.Close();
            }
        }
    }

}