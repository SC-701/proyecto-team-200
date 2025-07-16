using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ProductosBase
    {
        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public string   ImagenUrl { get; set; }

        public DateTime FechaCreacion  { get; set; }

        

    }

    public class ProductosRequest : ProductosBase
    {
        public Guid IdUsuario { get; set; }
        public Guid IdCategoria { get; set; }
        public int IdEstado { get; set; }


    }

    public class ProductosResponse : ProductosBase
    {
        public Guid IdProducto { get; set; }
        public string NombreProveedor { get; set; }

        public string Categoria { get; set; }

        public string Estado { get; set; }

    }
    






}
