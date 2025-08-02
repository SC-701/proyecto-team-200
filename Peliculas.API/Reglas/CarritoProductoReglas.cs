using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using static Abstracciones.Modelos.Carrito;
using static Abstracciones.Modelos.CarritoProducto;

namespace Reglas
{
    public class CarritoProductoReglas : ICarritoProductoReglas
    {
        private readonly ICarritoProductoDA _carritoProductoDA;
        private readonly ICarritoDA _carritoDA;

        public CarritoProductoReglas(ICarritoProductoDA carritoProductoDA, ICarritoDA carritoDA)
        {
            _carritoProductoDA = carritoProductoDA;
            _carritoDA = carritoDA;
        }


        public async Task<Guid> Agregar(Guid usuarioId, CarritoProductoRequest carritoProducto)
        {
            var carritoResponse = await _carritoDA.ObtenerPorUsuario(usuarioId);

            if (carritoResponse == null)
            {
                var nuevoCarrito = new CarritoBase
                {
                    CarritoId = Guid.NewGuid(),
                    UsuarioId = usuarioId,
                    FechaCreacion = DateTime.UtcNow,
                    Total = 0
                };

                await _carritoDA.Agregar(nuevoCarrito);


                carritoResponse = new CarritoResponse
                {
                    CarritoId = nuevoCarrito.CarritoId,
                    Total = nuevoCarrito.Total,
                    FechaCreacion = nuevoCarrito.FechaCreacion,
                    Productos = new List<CarritoProductoResponse>()
                };
            }
           

			var resultado = await _carritoProductoDA.Agregar(new CarritoProductoRequest
            {
                CarritoId = carritoResponse.CarritoId,
                ProductosId = carritoProducto.ProductosId,
                Cantidad = carritoProducto.Cantidad
            });
            
            await _carritoDA.ActualizarTotal(carritoResponse.CarritoId);


            return resultado;
        }

        public async Task<Guid> Editar(Guid carritoProductoId, CarritoProductoRequest carritoProducto)
        {
            if (carritoProducto.Cantidad == 0)
                return await _carritoProductoDA.Eliminar(carritoProductoId);
            var carritoId =  await _carritoProductoDA.Editar(carritoProductoId, carritoProducto);
			await _carritoProductoDA.ValidarStock(carritoProducto.ProductosId, carritoProducto.Cantidad);
			await _carritoDA.ActualizarTotal(carritoId);
            return carritoId;
        }

        public async Task<Guid> Eliminar(Guid carritoProductoId)
        {
            var carritoId = await _carritoProductoDA.Eliminar(carritoProductoId);

            await _carritoDA.ActualizarTotal(carritoId);

            return carritoId;
        }

		

		public async Task<bool> ValidarStock( Guid productoId, int cantidadSolicitada)
		{
			var stockDisponible = await _carritoProductoDA.ValidarStock( productoId, cantidadSolicitada);
			return stockDisponible;
		}

	}
}
