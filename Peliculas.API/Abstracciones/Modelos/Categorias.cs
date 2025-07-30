using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
	public class Categorias
	{
		public class CategoriasBase
		{
			public string Nombre { get; set; }
			public string Descripcion { get; set; }
			public DateTime FechaCreacion { get; set; }
		}
		public class CategoriasRequest : CategoriasBase
		{
			public int EstadoId { get; set; }
		}

		public class CategoriasResponse : CategoriasBase
		{
			public Guid CategoriasId { get; set; }
			public int EstadoId { get; set; }
		}
	}
}
