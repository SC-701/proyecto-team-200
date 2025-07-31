using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Carrito
    {


        public class CarritoBase
        {
            public Guid CarritoId { get; set; }
            public Guid UsuarioId { get; set; }
            public DateTime FechaCreacion { get; set; }
            public decimal Total { get; set; }
        }

        /*public class CarritoRequest
        {

        }*/
        //en teoria no se ocupa porque viene en claims

        public class CarritoResponse
        {
            public Guid CarritoId { get; set; }
            public decimal Total { get; set; }
            public DateTime FechaCreacion { get; set; }
            public List<CarritoProducto.CarritoProductoResponse> Productos { get; set; }
        }













    }
}
