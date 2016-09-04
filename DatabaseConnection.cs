using System.Data.SqlClient;

namespace DatabaseConnection
{
    class Connection{
        private SqlConnection myConnection;
        private const username = "username";
        private const password = "password";
        private const serverurl = "localhost";
        private const database = "database";

        public void Connection(){
            myConnection = new SqlConnection("Server="  + serverurl + ";
                                              Database="+ database  + ";
                                              User Id=" + username  + ";
                                              Password="+ password  + ";");
            try{
                myConnection.Open();
            }
            catch(Exception e){
                Console.WriteLine(e.ToString());
            }
        }

        //Crea un nuevo cliente y lo agrega a la base de datos
        public bool crear_Cliente(int cedula, string nombre, string apellidos, string residencia, string fechaNacimiento, int telefono){
            SqlParameter[] myparm = new SqlParameter[6];
            myparm[0] = new SqlParameter("@Cedula",          cedula);
            myparm[1] = new SqlParameter("@Nombre",          nombre);
            myparm[2] = new SqlParameter("@Apellidos",       apellidos);
            myparm[3] = new SqlParameter("@Residencia",      residencia);
            myparm[4] = new SqlParameter("@FechaNacimiento", fechaNacimiento);
            myparm[5] = new SqlParameter("@Telefono",        telefono);

            string comando = "INSERT INTO CLIENTE (Cedula_Cliente, Nombres, Apellidos, Grado_de_Penalizacion, Lugar_de_Residencia, Fecha_de_Nacimiento, Telefono)
VALUES ('@Cedula', '@Nombre', '@Apellidos', 0, '@Residencia', '@FechaNacimiento', '@Telefono'); ";
        }

        //Crea un nuevo Producto/Material y lo agrega a la base de datos
        public bool crear_Producto(int nombre, int sucursal, int proovedor, int categoria, string descripcion, bool exento, int cantidadDisponible){
            SqlParameter[] myparm = new SqlParameter[6];
            myparm[1] = new SqlParameter("@Nombre",      nombre);
            myparm[0] = new SqlParameter("@Sucursal",    sucursal);
            myparm[1] = new SqlParameter("@Proovedor",   proovedor);
            myparm[2] = new SqlParameter("@Categoria",   categoria);
            myparm[3] = new SqlParameter("@Descripcion", descripcion);
            myparm[4] = new SqlParameter("@Exento",      exento);
            myparm[5] = new SqlParameter("@Cantidad",    cantidadDisponible);

            string comando = "INSERT INTO PRODUCTO (Nombre, Sucursal, Proovedor, Categoria, Descripcion, Exento, Cantidad)
VALUES ('@Nombre', '@Sucursal', '@Proovedor', '@Categoria', '@Descripcion', '@Exento', '@Cantidad'); ";
        }


        //Crea un nuevo Pedido y lo agrega a la base de datos
        public bool crear_Pedido(int cedula_Cliente, int sucursal, int telefono_Preferido, int hora){
            SqlParameter[] myparm = new SqlParameter[4];
            myparm[0] = new SqlParameter("@Cedula",   cedula_Cliente);
            myparm[1] = new SqlParameter("@Sucursal", sucursal);
            myparm[2] = new SqlParameter("@Telefono", telefono_Preferido);
            myparm[3] = new SqlParameter("@Hora",     hora);

            string comando = "INSERT INTO PEDIDO (Cedula_Cliente, Id_Sucursal, Telefono_Preferido, Hora_de_Creacion)
VALUES ('@Cedula', '@Sucursal', '@Telefono', '@Hora'); ";
        }

        //Crea un nuevo Proovedor y lo agrega a la base de datos
        public bool crear_Proovedor(int cedula, string nombre, string apellidos, string fechaNacimiento, string residencia){
            SqlParameter[] myparm = new SqlParameter[5];
            myparm[0] = new SqlParameter("@Cedula",          cedula);
            myparm[1] = new SqlParameter("@Nombre",          nombre);
            myparm[2] = new SqlParameter("@Apellidos",       apellidos);
            myparm[3] = new SqlParameter("@Residencia",      residencia);
            myparm[4] = new SqlParameter("@FechaNacimiento", fechaNacimiento);

            string comando = "INSERT INTO PROVEEDOR (Cedula_Proveedor, Nombre, Apellidos, Fecha_de_Nacimiento, Lugar_de_Residencia)
VALUES ('@Cedula', '@Nombre', '@Apellidos', '@FechaNacimiento', '@Residencia'); ";
        }

        //Crea un nuevo Categoria y lo agrega a la base de datos
        public bool crear_Categoria(string nombre, string descripcion){
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Descripcion", descripcion);
            myparm[1] = new SqlParameter("@Nombre",      nombre);

            string comando = "INSERT INTO CLIENTE (Nombre, Descripcion)
VALUES ('@Nombre', '@Descripcion'); ";
        }

        
    }
}