using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using static Abstracciones.Modelos.Categorias;

namespace Flujo
{
	public class CategoriasFlujo : ICategoriasFlujo
	{
		private readonly ICategoriasDA _categoriasDA;


		public CategoriasFlujo(ICategoriasDA categoriasDA)
		{
			_categoriasDA = categoriasDA;

		}
		public async Task<Guid> Agregar(CategoriasRequest categorias)
		{
			return await _categoriasDA.Agregar(categorias);
		}

		public async Task<Guid> Editar(Guid IdProducto, CategoriasRequest categorias)
		{
			return await _categoriasDA.Editar(IdProducto, categorias);
		}


		public async Task<IEnumerable<CategoriasResponse>> Obtener()
		{
			return await _categoriasDA.Obtener();
		}

		public async Task<CategoriasResponse> ObtenerPorId(Guid IdCategoria)
		{
			return await _categoriasDA.ObtenerPorId(IdCategoria);
		}
	}
}
