using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Productos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Productos
{
    public class AgregarProductoModel : PageModel
    {
        private IConfiguracion _configuracion;
        [BindProperty]
        public ProductoRequest productoCrear { get; set; }
        public AgregarProductoModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task<IActionResult> OnPostCrearProducto()
        {

            string endpoint = _configuracion.ObtenerMetodo("EndPointsProductos", "AgregarProducto");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Post, endpoint);
            var respuesta = await cliente.PostAsJsonAsync(endpoint, productoCrear);

            if (!respuesta.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al guardar el producto.");
                return Partial("_FormularioModal", new ProductoRequest());
            }

            return new JsonResult(new { success = true });
        }
        public IActionResult OnGetFormularioModal()
        {
            return Partial("_FormularioModal", new ProductoRequest());
        }
    }
}
