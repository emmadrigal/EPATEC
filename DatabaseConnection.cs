﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using Company;

namespace DatabaseConnection
{
    public class Connection
    {
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
        public Connection()
        {
            err = new ErrorHandler.ErrorHandler();
            XmlDocument doc = new XmlDocument();
            string path = System.Windows.Forms.Application.StartupPath;
            doc.Load(path + "\\config.xml");

            string server = doc["DataBase"]["server"].InnerText;
            string DBname = doc["DataBase"]["database"].InnerText;
            string username = doc["DataBase"]["username"].InnerText;
            string password = doc["DataBase"]["password"].InnerText;
            connectionString = "Persist Security Info=False;" +
                                       "User ID=" + username +
                                       ";PWD=" + password +
                                       ";Initial Catalog=" + DBname +
                                       ";Data Source=" + server;
        }

        //Crea un nuevo cliente y lo agrega a la base de datos
        public void crear_Cliente(Company.Cliente cliente)
        {
            SqlParameter[] myparm = new SqlParameter[6];
            myparm[0] = new SqlParameter("@Cedula", cliente.Cedula_Cliente);
            myparm[1] = new SqlParameter("@Nombre", cliente.Nombre);
            myparm[2] = new SqlParameter("@Apellidos", cliente.Apellidos);
            myparm[3] = new SqlParameter("@Residencia", cliente.Residencia);
            myparm[4] = new SqlParameter("@FechaNacimiento", SqlDbType.DateTime);
            myparm[5] = new SqlParameter("@Telefono", cliente.Telefono);

            myparm[4].Value = cliente.Nacimiento;//Agrega la fecha bajo el formato correcto

            string command = "INSERT INTO CLIENTE (Cedula_Cliente, Nombre, Apellidos, Grado_de_Penalizacion, Lugar_de_Residencia, Fecha_de_Nacimiento, Telefono) VALUES (@Cedula, @Nombre, @Apellidos, 0, @Residencia, @FechaNacimiento, @Telefono); ";

            ExecuteCommandWrite(command, myparm);
        }

        public Company.Cliente get_Cliente(int cedula)
        {
            SqlParameter myparm = new SqlParameter("@Cedula", cedula);

            string command = "SELECT * FROM CLIENTE WHERE Cedula_Cliente = @Cedula;";
            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(myparm);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            Company.Cliente cliente = new Company.Cliente();
                            cliente.Cedula_Cliente = (long)reader["Cedula_Cliente"];
                            cliente.Nombre = (string)reader["Nombre"];
                            cliente.Apellidos = (string)reader["Apellidos"];
                            cliente.Penalizacion = (int)reader["Grado_de_Penalizacion"];
                            cliente.Residencia = (string)reader["Lugar_de_Residencia"];
                            cliente.Nacimiento = (string)reader["Fecha_de_Nacimiento"];
                            cliente.Telefono = (string)reader["Telefono"];
                            return cliente;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        internal List<Producto> get_AllProductsProv(string provedor)
        {
            string command = "SELECT * FROM PRODUCTO WHERE PRODUCTO.Cedula_Provedor = @cedula;";
            SqlParameter param = new SqlParameter("@cedula", provedor);

            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(param);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        Company.Producto producto;
                        if (reader.HasRows)
                        {
                            List<Company.Producto> Productos = new List<Company.Producto>();
                            while (reader.Read())
                            {
                                producto = new Company.Producto();
                                producto.nombre = (string)reader["Nombre_Producto"];
                                producto.Descripcion = (string)reader["Descripción"];
                                producto.Cantidad_Disponible = (int)reader["Cantidad_Disponible"];
                                producto.Exento = (int)reader["Exento"];
                                Productos.Add(producto);
                            }
                            return Productos;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        internal List<ProductosVentas> get_TopProductos()
        {
            string command = "SELECT TOP(10) PRODUCTO.Nombre_Producto, PRODUCTO.Id_Sucursal, COUNT(CONTIENE.Nombre_Producto) AS 'Cuenta' FROM (PRODUCTO LEFT JOIN CONTIENE ON CONTIENE.Nombre_Producto = PRODUCTO.Nombre_Producto)  GROUP BY PRODUCTO.Nombre_Producto, PRODUCTO.Id_Sucursal";

            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        Company.ProductosVentas producto;
                        if (reader.HasRows)
                        {
                            List<Company.ProductosVentas> Productos = new List<Company.ProductosVentas>();
                            while (reader.Read())
                            {
                                producto = new Company.ProductosVentas();
                                producto.Nombre = (string)reader["Nombre_Producto"];
                                producto.Sucursal= (long)reader["Id_Sucursal"];
                                producto.Cantidad = (int)reader["Cuenta"];
                                Productos.Add(producto);
                            }
                            return Productos;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        internal List<Producto> get_AllProducsCat(string categoria)
        {
            string command = "SELECT * FROM PRODUCTO WHERE PRODUCTO.Nombre_Categoría = @nombre;";
            SqlParameter param = new SqlParameter("@nombre", categoria);

            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(param);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        Company.Producto producto;
                        if (reader.HasRows)
                        {
                            List<Company.Producto> Productos = new List<Company.Producto>();
                            while (reader.Read())
                            {
                                producto = new Company.Producto();
                                producto.nombre = (string)reader["Nombre_Producto"];
                                producto.Descripcion = (string)reader["Descripción"];
                                producto.Cantidad_Disponible = (int)reader["Cantidad_Disponible"];
                                producto.Exento = (int)reader["Exento"];
                                Productos.Add(producto);
                            }
                            return Productos;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        internal List<SucursalVentas> get_VentasSucursal(string sucursal)
        {
            string command = "SELECT SUCURSAL.Id_Sucursal, COUNT(PRODUCTO.Nombre_Producto) As Cuenta FROM SUCURSAL LEFT JOIN PRODUCTO ON PRODUCTO.Id_Sucursal = SUCURSAL.Id_Sucursal LEFT JOIN CONTIENE ON CONTIENE.Nombre_Producto = PRODUCTO.Nombre_Producto GROUP BY SUCURSAL.Id_Sucursal";

            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        Company.SucursalVentas Sucursal;
                        if (reader.HasRows)
                        {
                            List<Company.SucursalVentas> Sucursales = new List<Company.SucursalVentas>();
                            while (reader.Read())
                            {
                                Sucursal = new Company.SucursalVentas();
                                Sucursal.Sucursal = (long)reader["Id_Sucursal"];
                                Sucursal.CantVentas = (int)reader["Cuenta"];
                                Sucursales.Add(Sucursal);
                            }
                            return Sucursales;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        internal List<ProductosVentas> get_TopProductosSuc(string sucursal)
        {
            string command = "SELECT TOP(10) PRODUCTO.Nombre_Producto, PRODUCTO.Id_Sucursal, COUNT(CONTIENE.Nombre_Producto) AS 'Cuenta' FROM (PRODUCTO LEFT JOIN CONTIENE ON CONTIENE.Nombre_Producto = PRODUCTO.Nombre_Producto) WHERE PRODUCTO.Id_Sucursal = @id  GROUP BY PRODUCTO.Nombre_Producto, PRODUCTO.Id_Sucursal;";

            SqlParameter param = new SqlParameter("@id", sucursal);

            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(param);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        Company.ProductosVentas producto;
                        if (reader.HasRows)
                        {
                            List<Company.ProductosVentas> Productos = new List<Company.ProductosVentas>();
                            while (reader.Read())
                            {
                                producto = new Company.ProductosVentas();
                                producto.Nombre = (string)reader["Nombre_Producto"];
                                producto.Sucursal = (long)reader["Id_Sucursal"];
                                producto.Cantidad = (int)reader["Cuenta"];
                                Productos.Add(producto);
                            }
                            return Productos;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        internal List<Pedido> get_AllPedidosSuc(string sucursal)
        {
            string command = "SELECT * FROM PEDIDO WHERE PEDIDO.Id_Sucursal = @id;";
            SqlParameter param = new SqlParameter("@id", sucursal);

            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(param);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        Company.Pedido pedido;
                        if (reader.HasRows)
                        {
                            List<Company.Pedido> pedidos = new List<Company.Pedido>();
                            while (reader.Read())
                            {
                                pedido = new Company.Pedido();
                                pedido.id_Pedido = (long)reader["Id_Pedido"];
                                pedido.Telefono = (string)reader["Telefono_Preferido"];
                                pedido.Hora = ((DateTime)reader["Hora_de_Creación"]).ToString();
                                pedidos.Add(pedido);
                            }
                            return pedidos;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public void update_Nombre_Cliente(int cedula, string nombre)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", nombre);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Nombre = @Nombre WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Apellido_Cliente(int cedula, string Apellido)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Apellido", Apellido);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Apellidos = @Apellido WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Penalizacion_Cliente(int cedula, byte penalizacion)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Cedula", cedula);
            myparm[1] = new SqlParameter("@Grado_de_Penalizacion", penalizacion);

            string command = "UPDATE CLIENTE SET Grado_de_Penalizacion = @Grado_de_Penalizacion WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Residencia_Cliente(int cedula, string Residencia)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Residencia", Residencia);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Lugar_de_Residencia = @Residencia WHERE Cedula_Cliente = @Cedula;";


            ExecuteCommandWrite(command, myparm);
        }

        public void update_Nacimiento_Cliente(int cedula, string Nacimiento)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nacimiento", Nacimiento);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Fecha_de_Nacimiento = @Nacimiento WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Telefono_Cliente(int cedula, int Telefono)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Telefono", Telefono);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE CLIENTE SET Telefono = @Telefono WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        //Elimina un cliente de la base de datos basado en su cedula
        public void eliminar_Cliente(int cedula)
        {
            SqlParameter myparm = new SqlParameter("@Cedula", cedula);

            string comando2 = "DELETE FROM CONTIENE JOIN PEDIDO ON CONTIENE.Id_Pedido = PEDIDO.Id_Pedido WHERE PEDIDO.Cedula_Cliente = @Cedula;";

            ExecuteCommandWriteOneParam(comando2, myparm);

            string comando3 = "DELETE FROM PEDIDO WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWriteOneParam(comando3, myparm);

            string command = "DELETE FROM CLIENTE WHERE Cedula_Cliente = @Cedula;";

            ExecuteCommandWriteOneParam(command, myparm);
        }

        //Crea un nuevo Proovedor y lo agrega a la base de datos
        public void crear_Provedor(Company.Proovedor provedor)
        {
            SqlParameter[] myparm = new SqlParameter[5];
            myparm[0] = new SqlParameter("@Cedula", provedor.Cedula_Proveedor);
            myparm[1] = new SqlParameter("@Nombre", provedor.Nombre);
            myparm[2] = new SqlParameter("@Apellidos", provedor.Apellidos);
            myparm[3] = new SqlParameter("@Residencia", provedor.Residencia);
            myparm[4] = new SqlParameter("@FechaNacimiento", SqlDbType.DateTime);

            myparm[4].Value = provedor.Nacimiento;//Agrega la fecha bajo el formato correcto

            string command = "INSERT INTO PROVEEDOR (Cedula_Proveedor, Nombre, Apellidos, Fecha_de_Nacimiento, Lugar_de_Residencia) VALUES (@Cedula, @Nombre, @Apellidos, @FechaNacimiento, @Residencia); ";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Nombre_Proovedor(int cedula, string Nombre)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", Nombre);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE PROVEEDOR SET Nombre = @Nombre WHERE Cedula_Proveedor = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Apellidos_Proovedor(int cedula, string Apellidos)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Apellidos", Apellidos);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE PROVEEDOR SET Apellidos = @Apellidos WHERE Cedula_Proveedor = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Nacimiento_Proovedor(int cedula, string Nacimiento)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nacimiento", SqlDbType.DateTime);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            myparm[0].Value = Nacimiento;//Agrega la fecha bajo el formato correcto


            string command = "UPDATE PROVEEDOR SET Fecha_de_Nacimiento = @Nacimiento WHERE Cedula_Proveedor = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Residencia_Proovedor(int cedula, string Residencia)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Residencia", Residencia);
            myparm[1] = new SqlParameter("@Cedula", cedula);

            string command = "UPDATE PROVEEDOR SET Lugar_de_Residencia = @Residencia WHERE Cedula_Proveedor = @Cedula;";

            ExecuteCommandWrite(command, myparm);
        }

        public Company.Proovedor get_Provedor(int cedula)
        {
            SqlParameter myparm = new SqlParameter("@Cedula", cedula);

            string command = "SELECT * FROM PROVEEDOR WHERE Cedula_Proveedor = @Cedula;";
            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(myparm);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            Company.Proovedor provedor = new Company.Proovedor();
                            provedor.Cedula_Proveedor = (long)reader["Cedula_Proveedor"];
                            provedor.Nombre = (string)reader["Nombre"];
                            provedor.Apellidos = (string)reader["Apellidos"];
                            DateTime myDateTime = (DateTime)reader["Fecha_de_Nacimiento"];
                            provedor.Nacimiento = myDateTime.ToString("MM-dd-yyyy");
                            provedor.Residencia = (string)reader["Lugar_de_Residencia"];
                            return provedor;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        //Elimina un producto de la base de datos basado en su nombre
        public void eliminar_Proovedor(int cedula_Proovedor)
        {

            SqlParameter cedula1 = new SqlParameter("@Cedula", cedula_Proovedor);
            string command = "SELECT COUNT(*) AS Cuenta FROM PRODUCTO WHERE PRODUCTO.Nombre_Producto = @Cedula;";
            int cuenta;

            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(cedula1);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                            cuenta = (int)reader["Cuenta"];
                        else
                            cuenta = 0;
                    }
                }
            }

            if (cuenta == 0)
            {
                SqlParameter cedula2 = new SqlParameter("@Cedula", cedula_Proovedor);

                string command2 = "DELETE FROM PROVEEDOR WHERE Cedula_Proveedor = @Cedula;";

                ExecuteCommandWriteOneParam(command2, cedula2);
            }

        }

        //Crea un nuevo Categoria y lo agrega a la base de datos
        public void crear_Categoria(Company.Categoria categoria)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Descripcion", categoria.Descripcion);
            myparm[1] = new SqlParameter("@Nombre", categoria.Nombre);

            string command = "INSERT INTO Categoria (Nombre, Descripción) VALUES (@Nombre, @Descripcion); ";



            ExecuteCommandWrite(command, myparm);
        }

        public void update_Descripcion_Categoria(string nombre, string Descripcion)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Descripcion", Descripcion);
            myparm[1] = new SqlParameter("@nombre", nombre);

            string command = "UPDATE CATEGORIA SET Descripción = @Descripcion WHERE nombre = @nombre;";

            ExecuteCommandWrite(command, myparm);
        }

        public Company.Categoria get_Categoria(string nombre)
        {
            SqlParameter myparm = new SqlParameter("@Nombre", nombre);

            string command = "SELECT * FROM CATEGORIA WHERE Nombre = @Nombre;";
            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(myparm);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            Company.Categoria categoria = new Company.Categoria();
                            categoria.Nombre = (string)reader["Nombre"];
                            categoria.Descripcion = (string)reader["Descripción"];
                            return categoria;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        //Elimina un producto de la base de datos basado en su nombre
        public void eliminar_Categoria(string Nombre)
        {
            SqlParameter nombre = new SqlParameter("@Nombre", Nombre);
            int cuenta = 0;

            string command = "SELECT COUNT(*) AS Cuenta FROM PRODUCTO WHERE PRODUCTO.Nombre_Categoría = @Nombre;";

            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(nombre);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                            cuenta = (int)reader["Cuenta"];
                        else
                            cuenta = 0;
                    }
                }
            }

            if (cuenta == 0)
            {
                SqlParameter nombre2 = new SqlParameter("@Nombre", Nombre);
                string command2 = "DELETE FROM CATEGORIA WHERE Nombre = @Nombre;";

                ExecuteCommandWriteOneParam(command2, nombre2);
            }

        }

        //Crea una nueva sucursal y la agrega a la base de datos
        public void crear_Sucursal(int id_Sucursal)
        {
            SqlParameter id = new SqlParameter("@id_Sucursal", id_Sucursal);

            string command = "INSERT INTO SUCURSAL (id_Sucursal) VALUES (@id); ";

            ExecuteCommandWriteOneParam(command, id);
        }

        //Elimina una sucursal de la base de datos basado en su id
        public void eliminar_Sucursal(int id_Sucursal)
        {
            SqlParameter id = new SqlParameter("@id_Sucursal", id_Sucursal);

            string command = "DELETE FROM SUCURSAL WHERE id_Sucursal = @id;";

            ExecuteCommandWriteOneParam(command, id);
        }

        //Crea un nuevo Empleado y lo agrega a la base de datos
        public void crear_Empleado(Company.Empleado empleado)
        {
            SqlParameter[] myparm = new SqlParameter[4];
            myparm[0] = new SqlParameter("@id", empleado.id_Empleado);
            myparm[1] = new SqlParameter("@Nombre", empleado.Nombre);
            myparm[2] = new SqlParameter("@Sucursal", empleado.id_Sucursal);
            myparm[3] = new SqlParameter("@Puesto", empleado.puesto);

            string command = "INSERT INTO EMPLEADO (Id_Empleado, Id_Sucursal, Nombre, Puesto) VALUES (@id, @Nombre, @Sucursal, @Puesto); ";

            ExecuteCommandWrite(command, myparm);
        }

        public Company.Empleado get_Empleado(int cedula)
        {
            SqlParameter myparm = new SqlParameter("@Cedula", cedula);

            string command = "SELECT * FROM EMPLEADO WHERE Id_Empleado = @Id_Empleado;";
            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(myparm);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Company.Empleado empleado = new Company.Empleado();
                            empleado.id_Empleado = (int)reader["Id_Empleado"];
                            empleado.id_Sucursal = (int)reader["Id_Sucursal"];
                            empleado.Nombre = (string)reader["Nombre"];
                            empleado.puesto = (string)reader["Puesto"];
                            return empleado;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public void update_Nombre_Empleado(int id, string Nombre)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", Nombre);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE EMPLEADO SET Nombre = @Nombre WHERE id = @id;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Sucursal_Empleado(int id, string Sucursal)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Sucursal", Sucursal);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE EMPLEADO SET Sucursal = @Sucursal WHERE id = @id;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Puesto_Empleado(int id, string Puesto)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Puesto", Puesto);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE EMPLEADO SET Puesto = @Puesto WHERE id = @id;";

            ExecuteCommandWrite(command, myparm);
        }

        //Elimina un Empleado de la base de datos basado en su id
        public void eliminar_Empleado(int id)
        {
            SqlParameter id_Empleado = new SqlParameter("@id", id);

            string command = "DELETE FROM EMPLEADO WHERE Id_Empleado = @id;";

            ExecuteCommandWriteOneParam(command, id_Empleado);
        }

        //Crea un nuevo Producto/Material y lo agrega a la base de datos
        public void crear_Producto(Company.Producto producto)
        {
            SqlParameter[] myparm = new SqlParameter[7];

            myparm[0] = new SqlParameter("@Nombre", producto.nombre);
            myparm[1] = new SqlParameter("@Sucursal", producto.id_Sucursal);
            myparm[2] = new SqlParameter("@Proovedor", producto.Cedula_Provedor);
            myparm[3] = new SqlParameter("@Categoria", producto.categoria);
            myparm[4] = new SqlParameter("@Descripcion", producto.Descripcion);
            myparm[5] = new SqlParameter("@Exento", producto.Exento);
            myparm[6] = new SqlParameter("@Cantidad", producto.Cantidad_Disponible);

            string command = "INSERT INTO PRODUCTO (Nombre_Producto, Id_Sucursal, Cedula_Provedor, Nombre_Categoría, Descripción, Exento, Cantidad_Disponible) VALUES (@Nombre, @Sucursal, @Proovedor, @Categoria, @Descripcion, @Exento, @Cantidad); ";

            ExecuteCommandWrite(command, myparm);
        }

        public Company.Producto get_Producto(string nombre)
        {
            SqlParameter myparm = new SqlParameter("@Nombre", nombre);

            string command = "SELECT * FROM PRODUCTO WHERE Nombre_Producto = @Nombre;";
            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(myparm);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Company.Producto producto = new Company.Producto();
                            producto.nombre = (string)reader["Nombre_Producto"];
                            producto.id_Sucursal = (long)reader["Id_Sucursal"];
                            producto.Cedula_Provedor = (long)reader["Cedula_Provedor"];
                            producto.categoria = (string)reader["Nombre_Categoría"];
                            producto.Descripcion = (string)reader["Descripción"];
                            producto.Exento = (int)reader["Exento"];
                            producto.Cantidad_Disponible = (int)reader["Cantidad_Disponible"];
                            return producto;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public void update_Nombre_Producto(int id, string Nombre)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Nombre", Nombre);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE PRODUCTO SET Nombre = @Nombre WHERE id = @id;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Descripcion_Producto(int id, string Descripcion)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Descripcion", Descripcion);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE PRODUCTO SET Descripción = @Descripcion WHERE id = @id;";

            ExecuteCommandWrite(command, myparm);
        }

        public void update_Cantidad_Producto(int id, int Cantidad)
        {
            SqlParameter[] myparm = new SqlParameter[2];
            myparm[0] = new SqlParameter("@Cantidad", Cantidad);
            myparm[1] = new SqlParameter("@id", id);

            string command = "UPDATE PRODUCTO SET Cantidad = @Cantidad WHERE id = @id;";

            ExecuteCommandWrite(command, myparm);
        }

        //Elimina un producto de la base de datos basado en su nombre
        public void eliminar_Producto(string Nombre)
        {
            SqlParameter nombre = new SqlParameter("@Nombre", Nombre);
            SqlParameter nombre2 = new SqlParameter("@Nombre", Nombre);

            string command2 = "DELETE FROM CONTIENE WHERE Nombre_Producto = @Nombre;";

            ExecuteCommandWriteOneParam(command2, nombre);

            string command = "DELETE FROM PRODUCTO WHERE Nombre_Producto = @Nombre;";

            ExecuteCommandWriteOneParam(command, nombre2);
        }


        //Crea un nuevo Pedido y lo agrega a la base de datos
        public void crear_Pedido(Company.Pedido pedido)
        {
            string horaActual = DateTime.Now.ToString();
            //Crea el nuevo pedidoi
            SqlParameter[] myparm = new SqlParameter[4];
            myparm[0] = new SqlParameter("@Cedula", pedido.Cedula_Cliente);
            myparm[1] = new SqlParameter("@Sucursal", pedido.id_Sucursal);
            myparm[2] = new SqlParameter("@Telefono", pedido.Telefono);
            myparm[3] = new SqlParameter("@Hora", horaActual);

            string command = "INSERT INTO PEDIDO (Cedula_Cliente, Id_Sucursal, Telefono_Preferido, Hora_de_Creación) VALUES (@Cedula, @Sucursal, @Telefono, @Hora); ";

            ExecuteCommandWrite(command, myparm);


            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();
                try
                {
                    //Obtiene el id creado por la base de datos
                    SqlParameter[] myparm2 = new SqlParameter[2];
                    myparm2[0] = new SqlParameter("@Cedula", pedido.Cedula_Cliente);
                    myparm2[1] = new SqlParameter("@Hora", horaActual);

                    string comandoid = "SELECT PEDIDO.Id_Pedido FROM PEDIDO WHERE Cedula_Cliente = @Cedula AND Hora_de_Creación = @Hora;";
                    SqlCommand commandID = new SqlCommand(comandoid, myConnection);
                    commandID.Parameters.AddRange(myparm2);
                    long id = 0;

                    using (SqlDataReader reader = commandID.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id = (long)reader["Id_Pedido"];
                        }
                    }

                    //Inserta cada uno de las relaciones entre Pedido y Producto que establece esto
                    string command_Productos = "INSERT INTO CONTIENE (Nombre_Producto, Id_Pedido) VALUES (@Producto, @Pedido); ";
                    SqlCommand comando = new SqlCommand(command_Productos, myConnection);
                    comando.Parameters.Add("@Pedido", SqlDbType.BigInt);
                    comando.Parameters.Add("@Producto", SqlDbType.NVarChar);

                    foreach (Company.Producto producto in pedido.productos)
                    {
                        try
                        {
                            comando.Parameters["@Producto"].Value = producto.nombre;
                            comando.Parameters["@Pedido"].Value = id;
                            comando.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            err.ErrorMessage = ex.Message.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    err.ErrorMessage = ex.Message.ToString();
                }
            }
        }

        public Company.Pedido get_Pedido(int id)
        {
            SqlParameter myparm = new SqlParameter("@id", id);
            Company.Pedido pedido = new Company.Pedido();

            string command = "SELECT * FROM PEDIDO WHERE Id_Pedido = @id;";
            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(myparm);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pedido.id_Pedido = (long)reader["Id_Pedido"];
                            pedido.Cedula_Cliente = (long)reader["Cedula_Cliente"];
                            pedido.id_Sucursal = (long)reader["Id_Sucursal"];
                            pedido.Telefono = (string)reader["Telefono_Preferido"];
                            pedido.Hora = ((DateTime)reader["Hora_de_Creación"]).ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            command = "SELECT PRODUCTO.Nombre_Producto, PRODUCTO.Descripción FROM PRODUCTO JOIN CONTIENE ON PRODUCTO.Nombre_Producto = CONTIENE.Nombre_Producto JOIN PEDIDO ON CONTIENE.Id_Pedido = PEDIDO.Id_Pedido WHERE PEDIDO.Id_Pedido = @id;";
            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();
                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    SqlParameter myparm2 = new SqlParameter("@id", id);
                    comando.Parameters.Add(myparm2);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            pedido.productos = new List<Company.Producto>();
                            if (reader.Read())
                            {
                                Company.Producto producto = new Company.Producto();
                                producto.nombre = (string)reader["Nombre_Producto"];
                                pedido.productos.Add(producto);
                            }
                        }
                    }
                }
            }
            return pedido;
        }

        public List<Company.Pedido> get_PedidoCliente(int cedula)
        {
            SqlParameter myparm = new SqlParameter("@cedula", cedula);

            string command = "SELECT * FROM PEDIDO WHERE Cedula_Cliente = @cedula;";

            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(myparm);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        Company.Pedido pedido;
                        if (reader.HasRows)
                        {
                            List<Company.Pedido> PedidosCliente = new List<Company.Pedido>();
                            while (reader.Read())
                            {
                                pedido = new Company.Pedido();
                                long id = (long)reader["Id_Pedido"];
                                pedido.id_Pedido = id;
                                pedido.Cedula_Cliente = (long)reader["Cedula_Cliente"];
                                pedido.id_Sucursal = (long)reader["Id_Sucursal"];
                                pedido.Telefono = (string)reader["Telefono_Preferido"];
                                pedido.Hora = ((DateTime)reader["Hora_de_Creación"]).ToString();

                                using (SqlConnection myConnection2 = new SqlConnection(connectionString))
                                {
                                    myConnection2.Open();
                                    string command2 = "SELECT * FROM PRODUCTO JOIN CONTIENE ON PRODUCTO.Nombre_Producto = CONTIENE.Nombre_Producto WHERE CONTIENE.Id_Pedido = @id";
                                    using (SqlCommand comando2 = new SqlCommand(command2, myConnection2))
                                    {
                                        comando2.Parameters.AddWithValue("@id", id);
                                        using (SqlDataReader reader2 = comando2.ExecuteReader())
                                        {
                                            Company.Producto producto;
                                            if (reader2.HasRows)
                                            {
                                                while (reader2.Read())
                                                {
                                                    producto = new Company.Producto();
                                                    producto.nombre = (string)reader2["Nombre_Producto"];
                                                    producto.Descripcion = (string)reader2["Descripción"];
                                                    pedido.productos.Add(producto);
                                                }
                                            }
                                        }
                                    }
                                }
                                PedidosCliente.Add(pedido);
                            }
                            return PedidosCliente;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }


        public List<Company.Cliente> get_AllClientes()
        {
            string command = "SELECT * FROM CLIENTE;";


            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        Company.Cliente cliente;
                        if (reader.Read())
                        {
                            List<Company.Cliente> Clientes = new List<Company.Cliente>();
                            while (reader.Read())
                            {
                                cliente = new Company.Cliente();
                                cliente.Cedula_Cliente = (long)reader["Cedula_Cliente"];
                                cliente.Nombre = (string)reader["Nombre"];
                                cliente.Apellidos = (string)reader["Apellidos"];
                                cliente.Residencia = (string)reader["Lugar_de_Residencia"];
                                cliente.Nacimiento = (string)reader["Fecha_de_Nacimiento"];
                                cliente.Telefono = (string)reader["Telefono"];
                                Clientes.Add(cliente);
                            }
                            return Clientes;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<Company.Producto> get_AllProducts()
        {

            string command = "SELECT * FROM PRODUCTO";


            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        Company.Producto producto;
                        if (reader.HasRows)
                        {
                            List<Company.Producto> Productos = new List<Company.Producto>();
                            while (reader.Read())
                            {
                                producto = new Company.Producto();
                                producto.nombre = (string)reader["Nombre_Producto"];
                                producto.Cantidad_Disponible = (int)reader["Cantidad_Disponible"];
                                producto.categoria = (string)reader["Nombre_Categoría"];
                                Productos.Add(producto);
                            }
                            return Productos;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<Company.Producto> get_ProveedorProducto(int cedula_Producto)
        {
            SqlParameter myparm = new SqlParameter("@id", cedula_Producto);

            string command = "SELECT * FROM PRODUCTO WHERE Cedula_Provedor = @id;";


            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(myparm);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        Company.Producto producto;
                        if (reader.Read())
                        {
                            List<Company.Producto> Productos = new List<Company.Producto>();
                            while (reader.Read())
                            {
                                producto = new Company.Producto();
                                producto.nombre = (string)reader["Nombre_Producto"];
                                producto.Cantidad_Disponible = (int)reader["Cantidad_Disponible"];
                                producto.categoria = (string)reader["Nombre_Categoría"];
                                Productos.Add(producto);
                            }
                            return Productos;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }


        public List<Company.Pedido> get_PedidoSucursal(int id_Empleado)
        {
            SqlParameter myparm = new SqlParameter("@id", id_Empleado);


            string command = "SELECT PEDIDO.Id_Pedido, PEDIDO.Cedula_Cliente, PEDIDO.Telefono_Preferido, PEDIDO.Hora_de_Creación FROM PEDIDO JOIN SUCURSAL ON PEDIDO.Id_Sucursal = SUCURSAL.Id_Sucursal JOIN EMPLEADO ON SUCURSAL.ID_Sucursal = EMPLEADO.ID_EMPLEADO WHERE EMPLEADO.Id_Empleado = @id;";


            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();

                using (SqlCommand comando = new SqlCommand(command, myConnection))
                {
                    comando.Parameters.Add(myparm);
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        Company.Pedido pedido;
                        if (reader.Read())
                        {
                            List<Company.Pedido> Productos = new List<Company.Pedido>();
                            while (reader.Read())
                            {
                                pedido = new Company.Pedido();
                                pedido.id_Pedido = (int)reader["Id_Pedido"];
                                pedido.Cedula_Cliente = (int)reader["Cedula_Cliente"];
                                pedido.Telefono = (string)reader["Telefono_Preferido"];
                                pedido.Hora = ((DateTime)reader["Hora_de_Creación"]).ToString();
                                Productos.Add(pedido);
                            }
                            return Productos;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }


        //Elimina un Pedido de la base de datos basado en el cliente que realizó el pedido y la hora
        public void eliminar_Pedido(int id)
        {
            SqlParameter myparm = new SqlParameter("@id", id);

            //Elimina todas las solicitudes de producto relacionadas con este producto
            string command_Productos = "DELETE FROM CONTIENE WHERE Id_Pedido = @id;";

            ExecuteCommandWriteOneParam(command_Productos, myparm);

            //Elimina el Pedido indicado
            string command = "DELETE FROM PEDIDO WHERE PEDIDO.Id_Pedido = @id;";
            SqlParameter myparm2 = new SqlParameter("@id", id);

            ExecuteCommandWriteOneParam(command, myparm2);
        }

        public void ExecuteCommandWrite(string command, SqlParameter[] parms)
        {
            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();
                SqlCommand comando = new SqlCommand(command, myConnection);

                comando.Parameters.AddRange(parms);
                try
                {
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    err.ErrorMessage = ex.Message.ToString();
                }
            }

        }

        public void ExecuteCommandWriteOneParam(string command, SqlParameter param)
        {
            using (myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();
                SqlCommand comando = new SqlCommand(command, myConnection);

                comando.Parameters.Add(param);

                try
                {
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    err.ErrorMessage = ex.Message.ToString();
                }
            }
        }

        public string GetException()
        {
            return err.ErrorMessage.ToString();
        }
    }
}
