using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Interfaces.DA;
using Dapper;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Categorias;

namespace DA
{
	public class CategoriasDA : ICategoriasDA
	{

		private IRepositorioDapper _repositorioDapper;
		private SqlConnection _sqlConnection;


		public CategoriasDA(IRepositorioDapper repositorioDapper)
		{
			_repositorioDapper = repositorioDapper;
			_sqlConnection = _repositorioDapper.ObtenerRepositorio();
		}


		public async Task<Guid> Agregar(CategoriasRequest categorias)
		{
			string query = @"AGREGAR_CATEGORIA";
			var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
			{

				IdCategoria = Guid.NewGuid(),
				Nombre = categorias.Nombre,
				Descripcion = categorias.Descripcion,
				FechaCreacion = DateTime.Now,
				EstadoId = categorias.EstadoId

			});
			return resultadoConsulta;
		}

		public async Task<Guid> Editar(Guid IdCategoria, CategoriasRequest categorias)
		{
			await VerificarExistenciaCategoria(IdCategoria);


			string query = @"EDITAR_CATEGORIA";

			var resultado = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
			{
				IdCategoria = IdCategoria,
				Nombre = categorias.Nombre,
				Descripcion = categorias.Descripcion,
				FechaCreacion = DateTime.Now,
				EstadoId = categorias.EstadoId
			});

			return resultado;
		}



		public async Task<IEnumerable<CategoriasResponse>> Obtener()
		{
			string query = @"VER_CATEGORIAS";
			var resultadoConsulta = await _sqlConnection.QueryAsync<CategoriasResponse>(query);
			return resultadoConsulta;
		}

		public async Task<CategoriasResponse> ObtenerPorId(Guid IdCategoria)
		{
			string query = @"VER_CATEGORIA_POR_ID";
			var resultadoConsulta = await _sqlConnection.QueryAsync<CategoriasResponse>(query,
				new { IdCategoria = IdCategoria });
			return resultadoConsulta.FirstOrDefault();
		}


		private async Task VerificarExistenciaCategoria(Guid IdCategoria)
		{
			CategoriasResponse? resutadoConsultaProducto = await ObtenerPorId(IdCategoria);
			if (resutadoConsultaProducto == null)
				throw new Exception("la marca no esta registrada");
		}

	}
}
