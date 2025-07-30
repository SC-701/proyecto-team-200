using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Modelos;
using static Abstracciones.Modelos.Categorias;

namespace Abstracciones.Interfaces.DA
{
	public interface ICategoriasDA
	{
		Task<IEnumerable<CategoriasResponse>> Obtener();
		Task<CategoriasResponse> ObtenerPorId(Guid IdCategoria);

        Task<Guid> AgregarPadre(CategoriasRequestPadre categorias);
        Task<Guid> AgregarHija(CategoriasRequestHija categorias);

        Task<Guid> Editar(Guid IdCategoria, CategoriasRequestPadre categorias);

        Task<Guid> Desactivar(Guid IdCategoria);

        Task<IEnumerable<CategoriasResponse>> ObtenerHijas(Guid idPadre);
        Task<IEnumerable<CategoriasResponse>> ObtenerHijasRecursivo(Guid idPadre);
    }
}
