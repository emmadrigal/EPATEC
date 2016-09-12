using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DatabaseConnection
{
    public class Connection{
        private SqlConnection myConnection;
        private ErrorHandler.ErrorHandler err;
        string connectionString;
        /*
        private string username;
        private string password;
        private string serverurl;
        private string database;
        */

        //Constructor, inicializa la interfaz con la base de datos, crea el objeto SqlConnection que se encarga de la coneccion
        public Connection(){
            err = new ErrorHandler.ErrorHandler();
            XmlDocument doc = new XmlDocument();
            string path = System.Windows.Forms.Application.StartupPath;
            doc.Load(path + "\\config.xml");

            string server   =  doc["DataBase"]["server"].InnerText;
            string DBname   =  doc["DataBase"]["database"].InnerText;
            string username =  doc["DataBase"]["username"].InnerText;
            string password =  doc["DataBase"]["password"].InnerText;
            connectionString  = "Persist Security Info=False;"+
                                       "User ID="          + username +
                                       ";PWD="             + password+
                                       ";Initial Catalog=" + DBname +
                                       ";Data Source="     + server;
        }

        //Crea un nuevo cliente y lo agrega a la base de datos
        public void crear_Cliente(int cedula, string nombre, string apellidos, string residencia, string fechaNacimiento, int telefono){
            SqlParameter[] myparm = new SqlParameter[6];
            myparm[0] = new SqlParameter("@Cedula",          cedula);
            myparm[1] = new SqlParameter("@Nombre",          nombre);
            myparm[2] = new SqlParameter("@Apellidos",       apellidos);
            myparm[3] = new SqlParameter("@Residencia",      residencia);   
            myparm[4] = new SqlParameter("@FechaNacimiento", SqlDbType.DateTime);
            myparm[5] = new SqlParameter("@Telefono",        telefono);

            myparm[4].Value = fechaNacimiento;//Agrega la fecha bajo el formato correcto

            string command = "INSERT INTO CLIENTE (Cedula_Cliente, Nombre, Apellidos, Grado_de_Penalizacion, Lugar_de_Residencia, Fecha_de_Nacimiento, Telefono) VALUES (@Cedula, @Nombre, @Apellidos, 0, @Residencia, @FechaNacimiento, @Telefono); ";

            ExecuteCommandWrite(command, myparm);
        }

        public Company.Cliente get_Cliente(int cedula){
            SqlParameter myparm = new SqlParameter("@Cedula", cedula);

            string command = "SELECT * FROM CLIENTE WHERE Cedula_Cliente = @Cedula;";
            using(myConnection = new SqlConnection(connectionString)){
                myConnection.Open();
                
                using(SqlCommand comando = new SqlCommand(command, myConnection)){
                    comando.Parameters.Add(myparm);
                    using(SqlDataReader reader = comando.ExecuteReader())
                    {
                            if (reader.Read()){

                                Company.Cliente cliente = new Company.Cliente();
                                cliente.Cedula_Cliente = (long)    reader["Cedula_Cliente"];
                                cliente.Nombre         = (string) reader["Nombre"];
                                cliente.Apellidos      = (string) reader["Apellidos"];
                                cliente.Penalizacion   = (int)    reader["Grado_de_Penalizacion"];
                                cliente.Residencia     = (string) reader["Lugar_de_Residencia"];
                                cliente.Nacimiento     = (string) reader["Fecha_de_Nacimiento"];
                                cliente.Telefono       = (string) reader["Telefono"];
                                return cliente;
                        }
                        else{
                            return null;
                        }
                    }
                }
            }
        }

        public void update_Nombre_Cliente(int cedula, string nombre){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", nombre);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Nombre = @Nombre WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Apellido_Cliente(int cedula, string Apellido){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Apellido", Apellido);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Apellidos = @Apellido WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Penalizacion_Cliente(int cedula, byte penalizacion){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Cedula", cedula);
            myparm[1] = new SqlParameter("@Grado_de_Penalizacion", penalizacion);

            string command = "UPDATE CLIENTE SET Grado_de_Penalizacion = @Grado_de_Penalizacion WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Residencia_Cliente(int cedula, string Residencia){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Residencia", Residencia);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Lugar_de_Residencia = @Residencia WHERE Cedula_Cliente = @Cedula;";
            

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Nacimiento_Cliente(int cedula, string Nacimiento){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nacimiento", Nacimiento);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Fecha_de_Nacimiento = @Nacimiento WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Telefono_Cliente(int cedula, int Telefono){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Telefono", Telefono);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Telefono = @Telefono WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        //Elimina un cliente de la base de datos basado en su cedula
        public void eliminar_Cliente(int cedula){
            SqlParameter myparm = new SqlParameter("@Cedula", cedula);

            string command = "DELETE FROM CLIENTE WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWriteOneParam(command, myparm);
        }

        //Crea un nuevo Proovedor y lo agrega a la base de datos
        public void crear_Proovedor(int cedula, string nombre, string apellidos, string fechaNacimiento, string residencia){
            SqlParameter[] myparm = new SqlParameter[5];
            myparm[0] = new SqlParameter("@Cedula",          cedula);
            myparm[1] = new SqlParameter("@Nombre",          nombre);
            myparm[2] = new SqlParameter("@Apellidos",       apellidos);
            myparm[3] = new SqlParameter("@Residencia",      residencia);
            myparm[4] = new SqlParameter("@FechaNacimiento", SqlDbType.DateTime);

            myparm[4].Value = fechaNacimiento;//Agrega la fecha bajo el formato correcto

            string command = "INSERT INTO PROVEEDOR (Cedula_Proveedor, Nombre, Apellidos, Fecha_de_Nacimiento, Lugar_de_Residencia) VALUES (@Cedula, @Nombre, @Apellidos, @FechaNacimiento, @Residencia); ";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Nombre_Proovedor(int cedula, string Nombre){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", Nombre);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE PROVEEDOR SET Nombre = @Nombre WHERE Cedula_Proveedor = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Apellidos_Proovedor(int cedula, string Apellidos){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Apellidos", Apellidos);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE PROVEEDOR SET Apellidos = @Apellidos WHERE Cedula_Proveedor = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Nacimiento_Proovedor(int cedula, string Nacimiento){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nacimiento", SqlDbType.DateTime);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            myparm[0].Value = Nacimiento;//Agrega la fecha bajo el formato correcto


            string command = "UPDATE PROVEEDOR SET Fecha_de_Nacimiento = @Nacimiento WHERE Cedula_Proveedor = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Residencia_Proovedor(int cedula, string Residencia){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Residencia", Residencia);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE PROVEEDOR SET Lugar_de_Residencia = @Residencia WHERE Cedula_Proveedor = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public Company.Proovedor get_Provedor(int cedula){
            SqlParameter myparm = new SqlParameter("@Cedula", cedula);

            string command = "SELECT * FROM PROVEEDOR WHERE Cedula_Proveedor = @Cedula;";
            using(myConnection = new SqlConnection(connectionString)){
                myConnection.Open();
                
                using(SqlCommand comando = new SqlCommand(command, myConnection)){
                    comando.Parameters.Add(myparm);
                    using(SqlDataReader reader = comando.ExecuteReader())
                    {
                            if (reader.Read()){

                                Company.Proovedor provedor = new Company.Proovedor();
                                provedor.Cedula_Proovedor = (long)    reader["Cedula_Proveedor"];
                                provedor.Nombre         = (string) reader["Nombre"];
                                provedor.Apellidos      = (string) reader["Apellidos"];
                                DateTime myDateTime = (DateTime) reader["Fecha_de_Nacimiento"];
                                provedor.Nacimiento   = myDateTime.ToString("MM-dd-yyyy");
                                provedor.Residencia    = (string) reader["Lugar_de_Residencia"];
                                return provedor;
                        }
                        else{
                            return null;
                        }
                    }
                }
            }
        }

        //Elimina un producto de la base de datos basado en su nombre
        public void eliminar_Proovedor(int cedula_Proovedor){
            SqlParameter cedula = new SqlParameter("@Cedula", cedula_Proovedor);

            string command = "DELETE FROM PROVEEDOR WHERE Cedula_Proveedor = @Cedula;";

            ExecuteCommandWriteOneParam(command, cedula);
        }

        //Crea un nuevo Categoria y lo agrega a la base de datos
        public void crear_Categoria(string nombre, string descripcion){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Descripcion", descripcion);
            myparm[1] = new SqlParameter("@Nombre",      nombre);

            string command = "INSERT INTO Categoria (Nombre, Descripción) VALUES (@Nombre, @Descripcion); ";

            

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Descripcion_Categoria(string nombre, string Descripcion){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Descripcion", Descripcion);
            myparm[1] = new SqlParameter("@nombre",      nombre);

            string command = "UPDATE CATEGORIA SET Descripción = @Descripcion WHERE nombre = @nombre;";

            ExecuteCommandWrite(command, myparm);
        }

        public Company.Categoria get_Categoria(string nombre){
            SqlParameter myparm = new SqlParameter("@Nombre", nombre);

            string command = "SELECT * FROM CATEGORIA WHERE Nombre = @Nombre;";
            using(myConnection = new SqlConnection(connectionString)){
                myConnection.Open();
                
                using(SqlCommand comando = new SqlCommand(command, myConnection)){
                    comando.Parameters.Add(myparm);
                    using(SqlDataReader reader = comando.ExecuteReader())
                    {
                            if (reader.Read()){

                                Company.Categoria categoria = new Company.Categoria();
                                categoria.Nombre         = (string) reader["Nombre"];
                                categoria.Descripcion      = (string) reader["Descripción"];
                                return categoria;
                        }
                        else{
                            return null;
                        }
                    }
                }
            }
        }

        //Elimina un producto de la base de datos basado en su nombre
        public void eliminar_Categoria(string Nombre){
            SqlParameter nombre = new SqlParameter("@Nombre", Nombre);

            string command = "DELETE FROM CATEGORIA WHERE Nombre = @Nombre;";

            ExecuteCommandWriteOneParam(command, nombre);
        }

        //Crea una nueva sucursal y la agrega a la base de datos
        public void crear_Sucursal(int id_Sucursal){
            SqlParameter id = new SqlParameter("@id_Sucursal", id_Sucursal);

            string command = "INSERT INTO SUCURSAL (id_Sucursal) VALUES (@id); ";

            ExecuteCommandWriteOneParam(command, id);
        }

        //Elimina una sucursal de la base de datos basado en su id
        public void eliminar_Sucursal(int id_Sucursal){
            SqlParameter id = new SqlParameter("@id_Sucursal", id_Sucursal);

            string command = "DELETE FROM SUCURSAL WHERE id_Sucursal = @id;";
            
            ExecuteCommandWriteOneParam(command, id);
        }

        //Crea un nuevo Empleado y lo agrega a la base de datos
        public void crear_Empleado(int id, string nombre, int Sucursal, byte puesto){
            SqlParameter[] myparm = new SqlParameter[4];
            myparm[0] = new SqlParameter("@id", id);
            myparm[1] = new SqlParameter("@Nombre",      nombre);
            myparm[2] = new SqlParameter("@Sucursal", Sucursal);
            myparm[3] = new SqlParameter("@Puesto",      puesto);

            string command = "INSERT INTO EMPLEADO (Id_Empleado, Id_Sucursal, Nombre, Puesto) VALUES (@id, @Nombre, @Sucursal, @Puesto); ";

            ExecuteCommandWrite(command, myparm);
        }

        public Company.Empleado get_Empleado(int cedula){
            SqlParameter myparm = new SqlParameter("@Cedula", cedula);

            string command = "SELECT * FROM EMPLEADO WHERE Id_Empleado = @Id_Empleado;";
            using(myConnection = new SqlConnection(connectionString)){
                myConnection.Open();
                
                using(SqlCommand comando = new SqlCommand(command, myConnection)){
                    comando.Parameters.Add(myparm);
                    using(SqlDataReader reader = comando.ExecuteReader())
                    {
                            if (reader.Read()){

                                Company.Empleado empleado = new Company.Empleado();
                                empleado.id_Empleado         = (int) reader["Id_Empleado"];
                                empleado.id_Sucursal      = (int) reader["Id_Sucursal"];
                                empleado.Nombre       = (string) reader["Nombre"];
                                empleado.puesto       = (string) reader["Puesto"];
                                return empleado;
                            }
                        else{
                            return null;
                        }
                    }
                }
            }
        }

        public void update_Nombre_Empleado(int id, string Nombre){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", Nombre);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE EMPLEADO SET Nombre = @Nombre WHERE id = @id;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Sucursal_Empleado(int id, string Sucursal){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Sucursal", Sucursal);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE EMPLEADO SET Sucursal = @Sucursal WHERE id = @id;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Puesto_Empleado(int id, string Puesto){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Puesto", Puesto);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE EMPLEADO SET Puesto = @Puesto WHERE id = @id;";

            ExecuteCommandWrite(command, myparm);
        }

        //Elimina un Empleado de la base de datos basado en su id
        public void eliminar_Empleado(int id){
            SqlParameter id_Empleado = new SqlParameter("@id", id);

            string command = "DELETE FROM EMPLEADO WHERE Id_Empleado = @id;";

            ExecuteCommandWriteOneParam(command, id_Empleado);
        }

        //Crea un nuevo Producto/Material y lo agrega a la base de datos
        public void crear_Producto(int nombre, int sucursal, int proovedor, string categoria, string descripcion, bool exento, int cantidadDisponible){
            SqlParameter[] myparm = new SqlParameter[6];
            myparm[1] = new SqlParameter("@Nombre",      nombre);
            myparm[0] = new SqlParameter("@Sucursal",    sucursal);
            myparm[1] = new SqlParameter("@Proovedor",   proovedor);
            myparm[2] = new SqlParameter("@Categoria",   categoria);
            myparm[3] = new SqlParameter("@Descripcion", descripcion);
            myparm[4] = new SqlParameter("@Exento",      exento);
            myparm[5] = new SqlParameter("@Cantidad",    cantidadDisponible);

            string command = "INSERT INTO PRODUCTO (Nombre_Producto, Id_Sucursal, Cedula_Proovedor, Nombre_Categoría, Descripción, Exento, Cantidad_Disponible) VALUES (@Nombre, @Sucursal, @Proovedor, @Categoria, @Descripcion, @Exento, @Cantidad); ";

            ExecuteCommandWrite(command, myparm);
        }

        public Company.Producto get_Producto(string nombre){
            SqlParameter myparm = new SqlParameter("@Nombre", nombre);

            string command = "SELECT * FROM PRODUCTO WHERE Nombre_Producto = @Nombre;";
            using(myConnection = new SqlConnection(connectionString)){
                myConnection.Open();
                
                using(SqlCommand comando = new SqlCommand(command, myConnection)){
                    comando.Parameters.Add(myparm);
                    using(SqlDataReader reader = comando.ExecuteReader())
                    {
                            if (reader.Read()){

                                Company.Producto producto = new Company.Producto();
                                producto.nombre              = (string) reader["Nombre_Producto"];
                                producto.id_Sucursal         = (int)    reader["Id_Sucursal"];
                                producto.Cedula_Provedor     = (int)    reader["Cedula_Provedor"];
                                producto.categoria           = (string) reader["Nombre_Categoría"];
                                producto.Descripcion         = (string) reader["Descripción"];
                                producto.Exento              = (int)    reader["Exento"];
                                producto.Cantidad_Disponible = (int)    reader["Cantidad_Disponible"];
                                return producto;
                            }
                        else{
                            return null;
                        }
                    }
                }
            }
        }

        public void update_Nombre_Producto(int id, string Nombre){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", Nombre);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE PRODUCTO SET Nombre = @Nombre WHERE id = @id;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Descripcion_Producto(int id, string Descripcion){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Descripcion", Descripcion);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE PRODUCTO SET Descripción = @Descripcion WHERE id = @id;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Cantidad_Producto(int id, int Cantidad){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Cantidad", Cantidad);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE PRODUCTO SET Cantidad = @Cantidad WHERE id = @id;";

            ExecuteCommandWrite(command, myparm);
        }

        //Elimina un producto de la base de datos basado en su nombre
        public void eliminar_Producto(int Nombre){
            SqlParameter nombre = new SqlParameter("@Nombre", Nombre);

            string command = "DELETE FROM PRODUCTO WHERE Nombre = @Nombre;";

            ExecuteCommandWriteOneParam(command, nombre);
        }


        //Crea un nuevo Pedido y lo agrega a la base de datos
        public void crear_Pedido(int cedula_Cliente, int sucursal, int telefono_Preferido, int hora,List<string> Productos){
            SqlParameter[] myparm = new SqlParameter[4];
            myparm[0] = new SqlParameter("@Cedula",   cedula_Cliente);
            myparm[1] = new SqlParameter("@Sucursal", sucursal);
            myparm[2] = new SqlParameter("@Telefono", telefono_Preferido);
            myparm[3] = new SqlParameter("@Hora",     hora);

            string command = "INSERT INTO PEDIDO (Cedula_Cliente, Id_Sucursal, Telefono_Preferido, Hora_de_Creacion) VALUES (@Cedula, @Sucursal, @Telefono, @Hora); ";

            ExecuteCommandWrite(command, myparm);


            string command_Productos = "INSERT INTO CONTIENE (Nombre_Producto, Id_Pedido) VALUES (@Producto, @Pedido); ";
            SqlCommand comando = new SqlCommand  (command_Productos, myConnection);
            comando.Parameters.Add("@Pedido", SqlDbType.Int);
            comando.Parameters.Add("@Producto", SqlDbType.NVarChar);
            foreach (string producto in Productos){
                comando.Parameters["@Producto"].Value = producto;
                comando.ExecuteNonQuery();    
            }
        }

        public Company.Pedido get_Pedido(int id){
            SqlParameter myparm = new SqlParameter("@id", id);

            string command = "SELECT * FROM PEDIDO WHERE Id_Pedido = @id;";
            using(myConnection = new SqlConnection(connectionString)){
                myConnection.Open();
                
                using(SqlCommand comando = new SqlCommand(command, myConnection)){
                    comando.Parameters.Add(myparm);
                    using(SqlDataReader reader = comando.ExecuteReader())
                    {
                            if (reader.Read()){

                                Company.Pedido pedido = new Company.Pedido();
                                pedido.id_Pedido      = (int) reader["Id_Pedido"];
                                pedido.Cedula_Cliente = (int)    reader["Cedula_Cliente"];
                                pedido.id_Sucursal    = (int)    reader["Id_Sucursal"];
                                pedido.Telefono       = (string) reader["Telefono_Preferido"];
                                pedido.Hora           = (string) reader["Hora_de_Creacion"];

                                command = "SELECT PRODUCTO.Nombre_Producto PRODUCTO.Descripción FROM PRODUCTO JOIN CONTIENE ON PRODUCTO.Nombre_Producto = CONTIENE.Nombre_Producto JOIN PEDIDO ON CONTIENE.Id_Pedido = PEDIDO.Id_Pedido WHERE PEDIDO.Id_Pedido = @id;";

                                using(SqlCommand comando2 = new SqlCommand(command, myConnection)){
                                    comando2.Parameters.Add(myparm);
                                    using(SqlDataReader reader2 = comando2.ExecuteReader())
                                    {
                                            Company.Producto producto;
                                            if (reader2.HasRows){
                                                while (reader.Read()){
                                                    producto = new Company.Producto();
                                                    producto.nombre = (string )reader["Nombre_Producto"];
                                                    pedido.productos.Add(producto);
                                                    
                                                }
                                            }
                                            else{
                                                return null;
                                        }
                                    }
                                }
                                return pedido;
                            }
                        else{
                            return null;
                        }
                    }
                }
            }
        }


        //Elimina un Pedido de la base de datos basado en el cliente que realizó el pedido y la hora
        public void eliminar_Pedido(int cedula_Cliente, int hora){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Cliente", cedula_Cliente);
            myparm[1] = new SqlParameter("@Hora",    hora);

            //Elimina todas las solicitudes de producto relacionadas con este producto
            string command_Productos = "DELETE FROM PEDIDO WHERE IdPedido = (SELECT Id_Pedido FROM PEDIDO WHERE Cedula_Cliente = @Cliente AND Hora_de_Creacion = @Hora); ";

            ExecuteCommandWrite(command_Productos, myparm);

            //Elimina el Pedido indicado
            string command = "DELETE FROM PEDIDO WHERE Cedula_Cliente = @Cliente AND Hora_de_Creacion = @Hora;";

            ExecuteCommandWrite(command, myparm); 
        }

        public void ExecuteCommandWrite(string command, SqlParameter[] parms){
            using(myConnection = new SqlConnection(connectionString)){
                myConnection.Open();
                SqlCommand comando = new SqlCommand(command, myConnection);

                comando.Parameters.AddRange(parms);
                try {
                comando.ExecuteNonQuery();
                }
                catch (Exception ex) {
                    err.ErrorMessage = ex.Message.ToString();
                    throw;
                }
            }

        }

        public void ExecuteCommandWriteOneParam(string command, SqlParameter param){
            using(myConnection = new SqlConnection(connectionString)){
                myConnection.Open();
                SqlCommand comando = new SqlCommand(command, myConnection);

                comando.Parameters.Add(param);

                try {
                comando.ExecuteNonQuery();
                }
                catch (Exception ex) {
                    err.ErrorMessage = ex.Message.ToString();
                    throw;
                }
            }
        }

    public string GetException() {
        return err.ErrorMessage.ToString();
    }        
    }
}
