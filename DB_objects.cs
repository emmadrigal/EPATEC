// Author - Anshu Dutta
// Contact - anshu.dutta@gmail.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Contiene las clases para el manejo temporal de los datos en memoria
namespace Company
{
    public class Cliente {

        private long   cedula_Cliente;
        private string nombre;
        private string apellidos;
        private int penalizacion;
        private string residencia;
        private string nacimiento;
        private string    telefono;

        /// Propiedad de Cedula
        public long Cedula_Cliente{
            get { return cedula_Cliente; }
            set { cedula_Cliente = value; }
        }

        /// Propiedad de Nombre
        public string Nombre{
            get { return nombre; }
            set { nombre = value; }
        }

        /// Propiedad de Apellidos
        public string Apellidos{
            get { return apellidos; }
            set { apellidos = value; }
        }

        /// Propiedad de Nombre
        public int Penalizacion{
            get { return penalizacion; }
            set { penalizacion = value; }
        }

        /// Propiedad de Nombre
        public string Residencia{
            get { return residencia; }
            set { residencia = value; }
        }

        /// Propiedad de Nombre
        public string Nacimiento{
            get { return nacimiento; }
            set { nacimiento = value; }
        }

        /// Propiedad de Nombre
        public string Telefono{
            get { return telefono; }
            set { telefono = value; }
        }

    }

    public class Sucursal{

        private int Id;
        
        public Sucursal()
        { }

        /// Propiedad de id
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
    }

    public class Producto{
        private string Nombre;
        private int sucursal;
        private int cedula_Provedor;
        private string Categoria;
        private string descripcion;
        private int exento;
        private int cantidad_Disponible;

        /// Propiedad de nombre
        public string nombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        /// Propiedad de nombre
        public int id_Sucursal
        {
            get { return sucursal; }
            set { sucursal = value; }
        }

        /// Propiedad de nombre
        public int Cedula_Provedor
        {
            get { return cedula_Provedor; }
            set { cedula_Provedor = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public int Exento
        {
            get { return exento; }
            set { exento = value; }
        }

        public int Cantidad_Disponible
        {
            get { return cantidad_Disponible; }
            set { cantidad_Disponible = value; }
        }

        public string categoria
        {
            get { return Categoria; }
            set { Categoria = value; }
        }

    }

    public class Proovedor {
        private long cedula_Provedor;
        private string nombre;
        private string apellidos;
        private string residencia;
        private string nacimiento;

        public Proovedor()
        { }

        /// Propiedad de Cedula
        public long Cedula_Proovedor
        {
            get { return cedula_Provedor; }
            set { cedula_Provedor = value; }
        }

        /// Propiedad de Nombre
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        /// Propiedad de Apellidos
        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }

        /// Propiedad de Nombre
        public string Residencia
        {
            get { return residencia; }
            set { residencia = value; }
        }

        /// Propiedad de Nombre
        public string Nacimiento
        {
            get { return nacimiento; }
            set { nacimiento = value; }
        }

    }

    public class Pedido {
        private int id;
        private int cedula; 
        private int sucursal;
        private string telefono;
        private string hora;
        private List<Producto> Productos;


        /// Propiedad de Cedula
        public int id_Pedido {
            get { return id; }
            set { id = value; }
        }
        
        /// Propiedad de Nombre
        public int Cedula_Cliente{
            get { return cedula; }
            set { cedula = value; }
        }

        /// Propiedad de Apellidos
        public int id_Sucursal{
            get { return sucursal; }
            set { sucursal = value; }
        }

        /// Propiedad de Nombre
        public string Telefono{
            get { return telefono; }
            set { telefono = value; }
        }

        /// Propiedad de Nombre
        public string Hora{
            get { return hora; }
            set { hora = value; }
        }

        public List<Producto> productos{
            get { return Productos; }
            set { Productos = value; }
        }

    }

    public class Empleado{
        private int id;
        private int sucursal;
        private string nombre;
        private string Puesto;

        /// Propiedad de id
        public int id_Empleado{
            get { return id; }
            set { id = value; }
        }

        /// Propiedad de id
        public int id_Sucursal{
            get { return sucursal; }
            set { sucursal = value; }
        }

        /// Propiedad de id
        public string Nombre{
            get { return nombre; }
            set { nombre = value; }
        }

        /// Propiedad de id
        public string puesto{
            get { return Puesto; }
            set { Puesto = value; }
        }
    }

    public class Categoria{
        private string nombre;
        private string descripcion;

        /// Propiedad de id
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        /// Propiedad de id
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }

}
