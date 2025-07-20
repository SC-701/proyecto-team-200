using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IProductosController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> ObtenerPorId(Guid IdProducto);

        Task<IActionResult> Agregar(ProductosRequest productos);

        Task<IActionResult> Editar(Guid IdProducto, ProductosRequest productos);

        Task<IActionResult> Eliminar(Guid IdProducto);
        Task<IActionResult> ListarProductosPaginado(int pageIndex, int pageSize);
    }
}
