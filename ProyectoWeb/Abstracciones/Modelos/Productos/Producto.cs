namespace Abstracciones.Modelos.Productos
{
    public class Producto
    {
        public Guid IdProducto { get; set; }
        public string NombreProveedor { get; set; }
        public string Categoria { get; set; } 
        public string Estado { get; set; } 
        public string Nombre { get; set; } 
        public decimal Precio { get; set; }
        public string Descripcion { get; set; } 
        public int Stock { get; set; }
        public string ImagenUrl { get; set; } 

    }
    public class ProductoRequest
    {
        

        public Guid IdUsuario { get; set; }
        public Guid IdCategoria { get; set; }
        public int IdEstado { get; set; }
        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public string ImagenUrl { get; set; }

    }
    public class ProductoRequestEditar
    {
        public Guid? IdProducto { get; set; }
        

        public Guid IdUsuario { get; set; }
        public Guid IdCategoria { get; set; }
        public int IdEstado { get; set; }
        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public string ImagenUrl { get; set; }

    }
}
