using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abstracciones.Modelos.Categorias;

namespace Abstracciones.Interfaces.DA
{
	public interface ICategoriasDA
	{
		Task<IEnumerable<CategoriasResponse>> Obtener();
		Task<CategoriasResponse> ObtenerPorId(Guid IdCategoria);

		Task<Guid> Agregar(CategoriasRequest categorias);

		Task<Guid> Editar(Guid IdCategoria, CategoriasRequest categorias);

	}
}
