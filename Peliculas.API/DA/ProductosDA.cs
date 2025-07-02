using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class ProductosDA : IProductosDA
    {
        #region Constructor
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;


        public ProductosDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }


        public async Task<Guid> Agregar(ProductosRequest productos)
        {
            string query = @"AgregarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdProducto = Guid.NewGuid(),
                IdUsuario = productos.IdUsuario,
                IdCategoria = productos.IdCategoria,
                IdEstado = productos.IdEstado,
                Nombre = productos.Nombre,
                Precio = productos.Precio,
                Descripcion = productos.Descripcion,
                Stock = productos.Stock,
                ImagenUrl = productos.ImagenUrl,
                FechaCreacion = DateTime.Now
            });
            return resultadoConsulta;
        }

        public Task<Guid> Editar(Guid Id, ProductosRequest productos)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductosResponse>> Obtener()
        {
            throw new NotImplementedException();
        }

        public Task<ProductosResponse> ObtenerPorId(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
