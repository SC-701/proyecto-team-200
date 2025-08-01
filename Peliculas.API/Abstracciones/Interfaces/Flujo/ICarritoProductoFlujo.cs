using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abstracciones.Modelos.CarritoProducto;

namespace Abstracciones.Interfaces.Flujo
{
	public interface ICarritoProductoFlujo
	{
		Task<List<CarritoProductoResponse>> ObtenerPorCarrito(Guid CarritoId);
		Task<CarritoProductoResponse> ObtenerPorID(Guid CarritoProductoId);


        Task<Guid> Agregar(Guid usuarioId, CarritoProductoRequest carritoProducto);


        Task<Guid> Editar(Guid CarritoProductoId, CarritoProductoRequest carritoproducto);

		Task<Guid> Eliminar(Guid CarritoProductoId);
	}
}
