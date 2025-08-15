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
        public ProductoPaginado ProductosPaginados { get; set; } = default!;
       
        

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task OnGet(int PagesIndex = 1, int PageSize = 5)
        {


            string endpoint = _configuracion.ObtenerMetodo("EndPointsProductos", "ObtenerProductosPaginados");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, PagesIndex, PageSize));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                ProductosPaginados = JsonSerializer.Deserialize<ProductoPaginado>(resultado, opciones);

                productos = ProductosPaginados.Items;

            }
        }
        public async Task OnPost(int PagesIndex = 1, int PageSize = 5)
        {


            string endpoint = _configuracion.ObtenerMetodo("EndPointsProductos", "ObtenerProductosPaginados");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, PagesIndex, PageSize));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                ProductosPaginados = JsonSerializer.Deserialize<ProductoPaginado>(resultado, opciones);

                productos = ProductosPaginados.Items;

            }
        }



    }
}
