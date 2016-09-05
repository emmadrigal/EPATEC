// Author - Anshu Dutta
// Contact - anshu.dutta@gmail.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Contiene las clases para el manejo temporal de los datos en memoria
namespace Company
{
    public class Cliente 
    {
        private int Cedula_Cliente;
        private string Nombre;
        private string Apellidos;
        private byte Penalizacion;
        private string Residencia;
        private string Nacimiento;
        private int Telefono;

        public Employee()
        { }

        /// Propiedad de Cedula
        public int Cedula_Cliente
        {
            get { return Cedula_Cliente; }
            set { Cedula_Cliente = value; }
        }

        /// Propiedad de Nombre
        public string Nombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        /// Propiedad de Apellidos
        public string Apellidos
        {
            get { return Apellidos; }
            set { Apellidos = value; }
        }

        /// Propiedad de Nombre
        public byte Penalizacion
        {
            get { return Penalizacion; }
            set { Penalizacion = value; }
        }

        /// Propiedad de Nombre
        public string Residencia
        {
            get { return Residencia; }
            set { Residencia = value; }
        }

        /// Propiedad de Nombre
        public string Nacimiento
        {
            get { return Nacimiento; }
            set { Nacimiento = value; }
        }

        /// Propiedad de Nombre
        public string Telefono
        {
            get { return Telefono; }
            set { Telefono = value; }
        }

    }

    public class Sucursal{
        private int id;
        
        public Sucursal()
        { }

        /// Propiedad de id
        public int id
        {
            get { return id; }
            set { id = value; }
        }
    }

    public class Producto{
        private string nombre;
        private int id_Sucursal;
        private int Cedula_Provedor;
        private int id_Categoria;
        private string Descripcion;
        private bool Exento;
        private int Cantidad_Disponible;
        
        public Producto()
        { }

        /// Propiedad de nombre
        public string nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        /// Propiedad de nombre
        public int id_Sucursal
        {
            get { return id_Sucursal; }
            set { id_Sucursal = value; }
        }

        /// Propiedad de nombre
        public int Cedula_Provedor
        {
            get { return Cedula_Provedor; }
            set { Cedula_Provedor = value; }
        }

        public string Descripcion
        {
            get { return Descripcion; }
            set { Descripcion = value; }
        }

        public bool Exento
        {
            get { return Exento; }
            set { Exento = value; }
        }

        public int Cantidad_Disponible
        {
            get { return Cantidad_Disponible; }
            set { Cantidad_Disponible = value; }
        }

    }

    public class Proovedor 
    {
        private int Cedula_Proovedor;
        private string Nombre;
        private string Apellidos;
        private string Residencia;
        private string Nacimiento;

        public Proovedor()
        { }

        /// Propiedad de Cedula
        public int Cedula_Proovedor
        {
            get { return Cedula_Proovedor; }
            set { Cedula_Proovedor = value; }
        }

        /// Propiedad de Nombre
        public string Nombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        /// Propiedad de Apellidos
        public string Apellidos
        {
            get { return Apellidos; }
            set { Apellidos = value; }
        }

        /// Propiedad de Nombre
        public string Residencia
        {
            get { return Residencia; }
            set { Residencia = value; }
        }

        /// Propiedad de Nombre
        public string Nacimiento
        {
            get { return Nacimiento; }
            set { Nacimiento = value; }
        }

    }

    public class Pedido 
    {
        private int id_Pedido;
        private int Cedula_Cliente;
        private int id_Sucursal;
        private string Telefono;
        private string Hora;
        private List<Producto> productos;

        public Pedido()
        { }

        /// Propiedad de Cedula
        public int id_Pedido
        {
            get { return id_Pedido; }
            set { id_Pedido = value; }
        }

        /// Propiedad de Nombre
        public int Cedula_Cliente
        {
            get { return Cedula_Cliente; }
            set { Cedula_Cliente = value; }
        }

        /// Propiedad de Apellidos
        public int id_Sucursal
        {
            get { return id_Sucursal; }
            set { id_Sucursal = value; }
        }

        /// Propiedad de Nombre
        public string Telefono
        {
            get { return Telefono; }
            set { Telefono = value; }
        }

        /// Propiedad de Nombre
        public string Hora
        {
            get { return Hora; }
            set { Hora = value; }
        }

    }

    public class Empleado{
        private int id_Empleado;
        private int id_Sucursal;
        private string Nombre;
        private string puesto;
        
        public Empleado()
        { }

        /// Propiedad de id
        public int id_Empleado
        {
            get { return id_Empleado; }
            set { id_Empleado = value; }
        }

        /// Propiedad de id
        public int id_Sucursal
        {
            get { return id_Sucursal; }
            set { id_Sucursal = value; }
        }

        /// Propiedad de id
        public string Nombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        /// Propiedad de id
        public string puesto
        {
            get { return puesto; }
            set { puesto = value; }
        }
    }

    public class Categoria{
        private int id;
        private string Nombre;
        private string Descripcion;
        
        public Categoria()
        { }

        /// Propiedad de id
        public int id
        {
            get { return id; }
            set { id = value; }
        }

        /// Propiedad de id
        public string Nombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        /// Propiedad de id
        public string Descripcion
        {
            get { return Descripcion; }
            set { Descripcion = value; }
        }
    }

}
