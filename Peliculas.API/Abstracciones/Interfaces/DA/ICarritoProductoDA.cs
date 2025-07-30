using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abstracciones.Modelos.CarritoProducto;

namespace Abstracciones.Interfaces.DA
{
	public interface ICarritoProductoDA
	{
		Task<List<CarritoProductoResponse>> ObtenerPorCarrito(Guid CarritoId);
		Task<CarritoProductoResponse> ObtenerPorID(Guid CarritoProductoId);



		Task<Guid> Agregar(CarritoProductoRequest carritoproducto);

		Task<Guid> Editar(Guid CarritoProductoId, CarritoProductoRequest carritoproducto);

		Task<Guid> Eliminar(Guid CarritoProductoId);
	}

}
