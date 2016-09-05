using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DatabaseConnection
{
    class Connection{
        private SqlConnection myConnection;
        private const string username = "username";
        private const string password = "password";
        private const string serverurl = "localhost";
        private const string database = "database";

        //Constructor, inicializa la interfaz con la base de datos, crea el objeto SqlConnection que se encarga de la coneccion
        public Connection(){
            myConnection = new SqlConnection("Server="  + serverurl + ";"+
                                              "Database="+ database  + ";"+
                                              "User Id=" + username  + ";"+
                                              "Password="+ password  + ";");
            try{
                myConnection.Open();
            }
            catch(Exception e){
                Console.WriteLine(e.ToString());
            }
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

            string comando = "INSERT INTO CLIENTE (Cedula_Cliente, Nombre, Apellidos, Grado_de_Penalizacion, Lugar_de_Residencia, Fecha_de_Nacimiento, Telefono) VALUES ('@Cedula', '@Nombre', '@Apellidos', 0, '@Residencia', '@FechaNacimiento', '@Telefono'); ";
        }

        public void update_Nombre_Cliente(int cedula, string nombre){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", nombre);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Nombre = @Nombre WHERE Cedula_Cliente = @Cedula;";
        }

        public void update_Apellido_Cliente(int cedula, string Apellido){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Apellido", Apellido);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Apellidos = @Apellido WHERE Cedula_Cliente = @Cedula;";
        }

        public void update_Penalizacion_Cliente(int cedula, string penalizacion){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", nombre);
            myparm[1] = new SqlParameter("@Grado_de_Penalizacion", penalizacion);

            string command = "UPDATE CLIENTE SET Grado_de_Penalizacion = @Grado_de_Penalizacion WHERE Cedula_Cliente = @Cedula;";
        }

        public void update_Residencia_Cliente(int cedula, string Residencia){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Residencia", Residencia);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Residencia = @Nombre Residencia Cedula_Cliente = @Cedula;";
        }

        public void update_Nacimiento_Cliente(int cedula, string Nacimiento){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nacimiento", Nacimiento);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Nacimiento = @Nacimiento WHERE Cedula_Cliente = @Cedula;";
        }

        public void update_Telefono_Cliente(int cedula, string Telefono){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Telefono", Telefono);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Telefono = @Telefono WHERE Cedula_Cliente = @Cedula;";
        }

        //Elimina un cliente de la base de datos basado en su cedula
        public void eliminar_Cliente(int cedula){
            SqlParameter myparm = new SqlParameter("@Cedula", cedula);

            string comando = "DELETE FROM CLIENTE WHERE Cedula_Cliente = @Cedula;";
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

            string comando = "INSERT INTO PROVEEDOR (Cedula_Proveedor, Nombre, Apellidos, Fecha_de_Nacimiento, Lugar_de_Residencia) VALUES ('@Cedula', '@Nombre', '@Apellidos', '@FechaNacimiento', '@Residencia'); ";
        }

        public void update_Nombre_Proovedor(int cedula, string Nombre){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", Nombre);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE PROOVEDOR SET Nombre = @Nombre WHERE Cedula_Proveedor = @Cedula;";
        }

        public void update_Apellidos_Proovedor(int cedula, string Apellidos){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Apellidos", Apellidos);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE PROOVEDOR SET Apellidos = @Apellidos WHERE Cedula_Proveedor = @Cedula;";
        }

        public void update_Nacimiento_Proovedor(int cedula, string Nacimiento){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nacimiento", SqlDbType.DateTime);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            myparm[0].Value = Nacimiento;//Agrega la fecha bajo el formato correcto


            string command = "UPDATE PROOVEDOR SET Nacimiento = @Nacimiento WHERE Cedula_Proveedor = @Cedula;";
        }

        public void update_Residencia_Proovedor(int cedula, string Residencia){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Residencia", Residencia);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE PROOVEDOR SET Residencia = @Residencia WHERE Cedula_Proveedor = @Cedula;";
        }

        //Elimina un producto de la base de datos basado en su nombre
        public void eliminar_Proovedor(int cedula_Proovedor){
            SqlParameter cedula = new SqlParameter("@Cedula", cedula_Proovedor);

            string comando = "DELETE FROM CLIENTE WHERE Cedula_Proveedor = @Cedula;";
        }

        //Crea un nuevo Categoria y lo agrega a la base de datos
        public void crear_Categoria(string nombre, string descripcion){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Descripcion", descripcion);
            myparm[1] = new SqlParameter("@Nombre",      nombre);

            string comando = "INSERT INTO CLIENTE (Nombre, Descripcion) VALUES ('@Nombre', '@Descripcion'); ";
        }

        public void update_Descripcion_Categoria(int cedula, string Descripcion){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Descripcion", Descripcion);
            myparm[1] = new SqlParameter("@Nombre",      Nombre);

            string command = "UPDATE CATEGORIA SET Descripcion = @Descripcion WHERE Nombre = @Nombre;";
        }

        public void update_Nombre_Categoria(int cedula, string Nombre){
            SqlParameter myparm = new SqlParameter("@Nombre", Nombre);

            string command = "UPDATE CATEGORIA SET Nombre = @Nombre WHERE Nombre = @Nombre;";
        }

        //Elimina un producto de la base de datos basado en su nombre
        public void eliminar_Categoria(string Nombre){
            SqlParameter nombre = new SqlParameter("@Nombre", Nombre);

            string comando = "DELETE FROM CATEGORIA WHERE Nombre = @Nombre;";
        }

        //Crea una nueva sucursal y la agrega a la base de datos
        public void crear_Sucursal(int id_Sucursal){
            SqlParameter id = new SqlParameter("@id_Sucursal", id_Sucursal);

            string comando = "INSERT INTO SUCURSAL (id_Sucursal) VALUES ('@id'); ";
        }

        //Elimina una sucursal de la base de datos basado en su id
        public void eliminar_Sucursal(int id_Sucursal){
            SqlParameter id = new SqlParameter("@id_Sucursal", id_Sucursal);

            string comando = "DELETE FROM SUCURSAL WHERE id_Sucursal = @id;";
        }

        //Crea un nuevo Empleado y lo agrega a la base de datos
        public void crear_Empleado(int id, string nombre, int Sucursal, byte puesto){
            SqlParameter[] myparm = new SqlParameter[4];
            myparm[0] = new SqlParameter("@id", id);
            myparm[1] = new SqlParameter("@Nombre",      nombre);
            myparm[2] = new SqlParameter("@Sucursal", Sucursal);
            myparm[3] = new SqlParameter("@Puesto",      puesto);

            string comando = "INSERT INTO EMPLEADO (Id_Empleado, Id_Sucursal, Nombre, Puesto) VALUES ('@id', '@Nombre', '@Sucursal', '@Puesto'); ";
        }

        public void update_Nombre_Empleado(int id, string Nombre){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", Nombre);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE EMPLEADO SET Nombre = @Nombre WHERE id = @id;";
        }

        public void update_Sucursal_Empleado(int id, string Sucursal){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Sucursal", Sucursal);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE EMPLEADO SET Sucursal = @Sucursal WHERE id = @id;";
        }

        public void update_Puesto_Empleado(int id, string Puesto){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Puesto", Puesto);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE EMPLEADO SET Puesto = @Puesto WHERE id = @id;";
        }

        

        //Elimina un Empleado de la base de datos basado en su id
        public void eliminar_Empleado(int id){
            SqlParameter id_Empleado = new SqlParameter("@id", id);

            string comando = "DELETE FROM EMPLEADO WHERE Id_Empleado = @id;";
        }



        //Crea un nuevo Producto/Material y lo agrega a la base de datos
        public void crear_Producto(int nombre, int sucursal, int proovedor, int categoria, string descripcion, bool exento, int cantidadDisponible){
            SqlParameter[] myparm = new SqlParameter[6];
            myparm[1] = new SqlParameter("@Nombre",      nombre);
            myparm[0] = new SqlParameter("@Sucursal",    sucursal);
            myparm[1] = new SqlParameter("@Proovedor",   proovedor);
            myparm[2] = new SqlParameter("@Categoria",   categoria);
            myparm[3] = new SqlParameter("@Descripcion", descripcion);
            myparm[4] = new SqlParameter("@Exento",      exento);
            myparm[5] = new SqlParameter("@Cantidad",    cantidadDisponible);

            string comando = "INSERT INTO PRODUCTO (Nombre, Sucursal, Proovedor, Categoria, Descripcion, Exento, Cantidad_Disponible) VALUES ('@Nombre', '@Sucursal', '@Proovedor', '@Categoria', '@Descripcion', '@Exento', '@Cantidad'); ";
        }

        public void update_Nombre_Producto(int id, string Nombre){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", Nombre);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE PRODUCTO SET Nombre = @Nombre WHERE id = @id;";
        }

        public void update_Descripcion_Producto(int id, string Descripcion){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Descripcion", Descripcion);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE PRODUCTO SET Descripcion = @Descripcion WHERE id = @id;";
        }

        public void update_Cantidad_Producto(int id, int Cantidad){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Cantidad", Cantidad);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE PRODUCTO SET Cantidad = @Cantidad WHERE id = @id;";
        }



        //Elimina un producto de la base de datos basado en su nombre
        public void eliminar_Producto(int Nombre){
            SqlParameter nombre = new SqlParameter("@Nombre", Nombre);

            string comando = "DELETE FROM PRODUCTO WHERE Nombre = @Nombre;";
        }


        //Crea un nuevo Pedido y lo agrega a la base de datos
        public void crear_Pedido(int cedula_Cliente, int sucursal, int telefono_Preferido, int hora,List<string> Productos){
            SqlParameter[] myparm = new SqlParameter[4];
            myparm[0] = new SqlParameter("@Cedula",   cedula_Cliente);
            myparm[1] = new SqlParameter("@Sucursal", sucursal);
            myparm[2] = new SqlParameter("@Telefono", telefono_Preferido);
            myparm[3] = new SqlParameter("@Hora",     hora);

            string comando = "INSERT INTO PEDIDO (Cedula_Cliente, Id_Sucursal, Telefono_Preferido, Hora_de_Creacion) VALUES ('@Cedula', '@Sucursal', '@Telefono', '@Hora'); ";


            string comando_Productos = "INSERT INTO CONTIENE (Nombre_Producto, Id_Pedido) VALUES ('@Producto', '@Pedido'); ";
            SqlCommand   command = new SqlCommand  (comando_Productos, myConnection);
            command.Parameters.Add("@Pedido", SqlDbType.Int);
            command.Parameters.Add("@Producto", SqlDbType.NVarChar);
            foreach (string producto in Productos){
                command.Parameters["@Producto"].Value = producto;
                //Se ejecuta el comando para el producto dado    
            }
        }

        //Elimina un Pedido de la base de datos basado en el cliente que realizó el pedido y la hora
        public void eliminar_Pedido(int cedula_Cliente, int hora){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Cliente", cedula_Cliente);
            myparm[1] = new SqlParameter("@Hora",    hora);

            //Elimina todas las solicitudes de producto relacionadas con este producto
            string comando_Productos = "DELETE FROM PEDIDO WHERE IdPedido = (SELECT Id_Pedido FROM PEDIDO WHERE Cedula_Cliente = @Cliente AND Hora_de_Creacion = @Hora); ";

            //Elimina el Pedido indicado
            string comando = "DELETE FROM PEDIDO WHERE Cedula_Cliente = @Cliente AND Hora_de_Creacion = @Hora;";

            
        }

        
        

        
    }
}