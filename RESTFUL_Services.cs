using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RestWebService
{
    public class Service:IHttpHandler
    {
        private DatabaseConnection.Connection DBConnection;
        private string connString;
        private ErrorHandler.ErrorHandler errHandler;

        //This called to determine whether this instance of the HTTP handler can be reused for fulfilling other requests of the same type
        bool IHttpHandler.IsReusable{
            get { throw new NotImplementedException(); }
        }

        //Handles requests calling the correct method
        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            try
            {                
                string url = Convert.ToString(context.Request.Url);                
                DBConnection = new DatabaseConnection.Connection();
                errHandler = new ErrorHandler.ErrorHandler();

                //Handling CRUD
                switch (context.Request.HttpMethod)
                {
                    case "GET":
                        //Perform READ Operation                   
                        READ(context);
                        break;
                    case "POST":
                        //Perform CREATE Operation
                        CREATE(context);
                        break;
                    case "PUT":
                        //Perform UPDATE Operation
                        UPDATE(context);
                        break;
                    case "DELETE":
                        //Perform DELETE Operation
                        DELETE(context);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                errHandler.ErrorMessage = ex.Message.ToString();
                context.Response.Write(errHandler.ErrorMessage);                
            }
        }


        /// <summary>
        /// GET Operation
        /// </summary>
        /// <param name="context"></param>
        private void READ( HttpContext context)
        {
            //HTTP Request - //http://server.com/virtual directory/employee?id={id}
            //http://localhost/RestWebService/employee
            try
            {
                string type = Convert.ToString(context.Request["object"]);
                string id   = Convert.ToString(context.Request["id"]);

                string Output;
                //HTTP Request Type - GET"
                //Performing Operation - READ"
                //Data sent via query string
                //POST - Data sent as name value pair and resides in the <form section> of the browser
                if (type.Equals("empleado")){
                    Company.Empleado emp = DBConnection.get_Empleado(Int32.Parse(id));
                    if (emp == null)
                    {
                        context.Response.Write(id + "Empleado no encontrado");
                    }
                    Output = JsonConvert.SerializeObject(emp);
                }
                else if (type.Equals("categoria"))
                {
                    Company.Categoria categoria = DBConnection.get_Categoria(id);
                    if (categoria == null)
                    {
                        context.Response.Write(id + "Categoria no encontrada");
                    }
                    Output = JsonConvert.SerializeObject(categoria);
                }
                else if (type.Equals("pedido")){
                    Company.Pedido pedido = DBConnection.get_Pedido(Int32.Parse(id));
                    if (pedido == null)
                    {
                        context.Response.Write(id + "Pedido no encontrado");
                    }
                    Output = JsonConvert.SerializeObject(pedido);
                }
                else if (type.Equals("provedor"))
                {
                    Company.Proovedor provedor = DBConnection.get_Provedor(Int32.Parse(id));
                    if (provedor == null)
                    {
                        context.Response.Write(id + "Provedor no encontrado");
                    }
                    Output = JsonConvert.SerializeObject(provedor);
                }
                else if (type.Equals("producto"))
                {
                    Company.Producto producto = DBConnection.get_Producto(id);
                    if (producto == null)
                    {
                        context.Response.Write(id + "Empleado no encontrado");
                    }
                    Output = JsonConvert.SerializeObject(producto);
                }
                else if (type.Equals("cliente")){
                    Company.Cliente cliente = DBConnection.get_Cliente(Int32.Parse(id));
                    if (cliente == null)
                    {
                        context.Response.Write(id + "Empleado no encontrado");
                    }
                    Output = JsonConvert.SerializeObject(cliente);
                }
                else {
                    context.Response.Write(type + "Tipo no soportado");
                    Output = "";
                }

                context.Response.ContentType = "application/json";
                WriteResponse(Output);
            }
            catch (Exception ex)
            {
                WriteResponse("Error in READ");
                errHandler.ErrorMessage = DBConnection.GetException();
                errHandler.ErrorMessage = ex.Message.ToString();                
            }            
        }
        
        
        /// <summary>
        /// POST Operation
        /// </summary>
        /// <param name="context"></param>
        private void CREATE(HttpContext context)
        {
            try
            {
                // Extract the content of the Request and make a employee class
                // The message body is posted as bytes. read the bytes
                byte[] PostData = context.Request.BinaryRead(context.Request.ContentLength);
                //Convert the bytes to string using Encoding class
                string str = Encoding.UTF8.GetString(PostData);

                //Obtiene la primera llave, en este caso corresponde a el objeto a ser insertado
                JObject employee = JObject.Parse(str);//Convierte json a employee
                string tipo = employee.Properties().Select(p => p.Name).FirstOrDefault();




                if (tipo.Equals("empleado"))
                {
                    JToken results = employee["empleado"].First;//Obtiene el descendiente de empleado
                    Company.Empleado Empleado = JsonConvert.DeserializeObject<Company.Empleado>("{" + results.ToString() + "}");//Deserializa el dato a un objeto
                    DBConnection.crear_Empleado(Empleado);
                }
                else if (tipo.Equals("categoria"))
                {
                    JToken results = employee["empleado"].First;//Obtiene el descendiente de empleado
                    Company.Categoria categoria = JsonConvert.DeserializeObject<Company.Categoria>("{" + results.ToString() + "}");//Deserializa el dato a un objeto
                    DBConnection.crear_Categoria(categoria);
                }
                else if (tipo.Equals("pedido"))
                {
                    JToken results = employee["empleado"].First;//Obtiene el descendiente de empleado
                    Company.Pedido pedido = JsonConvert.DeserializeObject<Company.Pedido>("{" + results.ToString() + "}");//Deserializa el dato a un objeto
                    DBConnection.crear_Pedido(pedido);
                }
                else if (tipo.Equals("provedor"))
                {
                    JToken results = employee["empleado"].First;//Obtiene el descendiente de empleado
                    Company.Proovedor proovedor = JsonConvert.DeserializeObject<Company.Proovedor>("{" + results.ToString() + "}");//Deserializa el dato a un objeto
                    DBConnection.crear_Empleado(proovedor);
                }
                else if (tipo.Equals("producto"))
                {
                    JToken results = employee["empleado"].First;//Obtiene el descendiente de empleado
                    Company.Producto producto = JsonConvert.DeserializeObject<Company.Producto>("{" + results.ToString() + "}");//Deserializa el dato a un objeto
                    DBConnection.crear_Empleado(producto);
                }
                else if (tipo.Equals("cliente"))
                {
                    JToken results = employee["empleado"].First;//Obtiene el descendiente de empleado
                    Company.Cliente cliente = JsonConvert.DeserializeObject<Company.Cliente>("{" + results.ToString() + "}");//Deserializa el dato a un objeto
                    DBConnection.crear_Empleado(cliente);
                }
            }

            catch (Exception ex){

                WriteResponse("Error in CREATE");
                errHandler.ErrorMessage = DBConnection.GetException();
                errHandler.ErrorMessage = ex.Message.ToString();                
            }
        }
        /// <summary>
        /// PUT Operation
        /// </summary>
        /// <param name="context"></param>
        private void UPDATE(HttpContext context)
        {
            //The PUT Method
            try
            {
                // context.Response.Write("Update");
                // Read the data in the message body of the PUT method
                // The code expects the employee class as data to be updated

                byte[] PUTRequestByte = context.Request.BinaryRead(context.Request.ContentLength);
                context.Response.Write(PUTRequestByte);

                // Deserialize Employee
                //Company.Employee emp = Deserialize(PUTRequestByte);
                //DBConnection.UpdateEmployee(emp);
                //context.Response.Write("Employee Updtated Sucessfully");
                WriteResponse("Employee Updated Sucessfully");
            }
            catch (Exception ex)
            {

                WriteResponse("Error in CREATE");
                errHandler.ErrorMessage = DBConnection.GetException();
                errHandler.ErrorMessage = ex.Message.ToString();                
            }
        }
        /// <summary>
        /// DELETE Operation
        /// </summary>
        /// <param name="context"></param>
        private void DELETE(HttpContext context)
        {
            try
            {
                int EmpCode = Convert.ToInt16(context.Request["id"]);
                //DBConnection.DeleteEmployee(EmpCode);
                WriteResponse("Employee Deleted Successfully");
            }
            catch (Exception ex)
            {
                
                WriteResponse("Error in CREATE");
                errHandler.ErrorMessage = DBConnection.GetException();
                errHandler.ErrorMessage = ex.Message.ToString();                
            }
        }


        /// <summary>
        /// Method - Writes into the Response stream
        /// </summary>
        /// <param name="strMessage"></param>
        private static void WriteResponse(string strMessage)
        {
            HttpContext.Current.Response.Write(strMessage);            
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

    }
}
