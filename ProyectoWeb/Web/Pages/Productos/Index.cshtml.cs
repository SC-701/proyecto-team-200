using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Productos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Web.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private IConfiguracion _configuracion;
        public IList<Producto> productos { get; set; } = default!;
        [BindProperty]
        public Producto producto { get; set; }
        [BindProperty]
        public ProductoRequest productoCrear { get; set; }

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task OnGet()
        
        {
            string endpoint = _configuracion.ObtenerMetodo("EndPointsProductos", "ObtenerProductos");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                productos = JsonSerializer.Deserialize<List<Producto>>(resultado, opciones);
            }
        }
        
        

    }
}
