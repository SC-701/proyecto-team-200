using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Abstracciones.Modelos.Categorias;

namespace Abstracciones.Interfaces.API
{
	public interface ICategoriasController
	{
		Task<IActionResult> Obtener();
		Task<IActionResult> ObtenerPorId(Guid IdCategoria);

		Task<IActionResult> Agregar(CategoriasRequest categorias);

		Task<IActionResult> Editar(Guid IdCategoria, CategoriasRequest categorias);


	}
}
