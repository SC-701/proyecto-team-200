using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using static Abstracciones.Modelos.CarritoProducto;

namespace Flujo
{
	public class CarritoProductoFlujo : ICarritoProductoFlujo
	{
		private readonly ICarritoProductoDA _carritoProductoDA;


		public CarritoProductoFlujo(ICarritoProductoDA carritoProductoDA)
		{
			_carritoProductoDA = carritoProductoDA;

		}
		public async Task<Guid> Agregar(CarritoProductoRequest CarritoId)
		{
			return await _carritoProductoDA.Agregar(CarritoId);
		}



		public async Task<Guid> Editar(Guid CarritoProductoId, CarritoProductoRequest carritoproducto)
		{
			return await _carritoProductoDA.Editar(CarritoProductoId, carritoproducto);
		}




		public async Task<Guid> Eliminar(Guid CarritoProductoId)
		{
			return await _carritoProductoDA.Eliminar(CarritoProductoId);
		}

		public async Task<List<CarritoProductoResponse>> ObtenerPorCarrito(Guid CarritoId)
		{
			return await _carritoProductoDA.ObtenerPorCarrito(CarritoId);
		}


		public async Task<CarritoProductoResponse> ObtenerPorID(Guid CarritoProductoId)
		{
			return await _carritoProductoDA.ObtenerPorID(CarritoProductoId);
		}


	}
}
