using System.Net;
using System.Text.Json;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Categoria;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Categorias
{
    public class CategoriasModel : PageModel
    {

        private IConfiguracion _configuracion;
        public IList<Categoria> categorias { get; set; } = default!;

        public CategoriasModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPointsCategorias", "ObtenerCategoriasTotales");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                categorias = JsonSerializer.Deserialize<List<Categoria>>(resultado, opciones)!;
            }

        }
    }
}

