using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Categoria
{
    public class Categoria
    {
        public Guid categoriasId { get; set; }
        public Guid padreId { get; set; }
        public string estado { get; set; }
        public string nombre { get; set; }
        public string nombrePadre { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
