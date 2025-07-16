using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Productos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class EditarProductoModel : PageModel
    {
        private IConfiguracion _configuracion;

        public EditarProductoModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
           
        }

        [BindProperty]
        public ProductoRequestEditar productoEditar { get; set; }


        public async Task<IActionResult> OnGetFormularioModalEditar(Guid? idProducto)
        {
            if (idProducto == Guid.Empty)
                return NotFound();
            string endpoint = _configuracion.ObtenerMetodo("EndPointsProductos", "ObtenerProducto");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, idProducto));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                productoEditar = JsonSerializer.Deserialize<ProductoRequestEditar>(resultado, opciones);
                productoEditar.IdProducto = idProducto;
                return Partial("_FormularioModalEditar", productoEditar);
            }
            else
            {
                return NotFound();
            }

        }
        public async Task<ActionResult> OnPostEditarProducto()
        {

            string endpoint = _configuracion.ObtenerMetodo("EndPointsProductos", "EditarProducto");
            var cliente = new HttpClient();
            var respuesta = await cliente.PutAsJsonAsync<ProductoRequestEditar>(string.Format(endpoint, productoEditar.IdProducto), productoEditar);
            respuesta.EnsureSuccessStatusCode();
            return new JsonResult(new { success = true });


        }
    }
}
