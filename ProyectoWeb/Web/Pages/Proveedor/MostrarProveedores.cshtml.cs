using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Proveedores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;



namespace Web.Pages.Proveedor
{
    public class MostrarProveedoresModel : PageModel
    {
		private IConfiguracion _configuracion;
		public IList<ProveedoresBase> proveedores { get; set; } = default!;

		public MostrarProveedoresModel(IConfiguracion configuracion)
		{
			_configuracion = configuracion;
		}
		public async Task OnGet()
		{
			string endpoint = _configuracion.ObtenerMetodo("ApiEndPointsProveedores", "ObtenerProveedores");
			var cliente = new HttpClient();
			var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint));

			var respuesta = await cliente.SendAsync(solicitud);
			respuesta.EnsureSuccessStatusCode();
			if (respuesta.StatusCode == HttpStatusCode.OK)
			{
				var resultado = await respuesta.Content.ReadAsStringAsync();
				var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
				proveedores = JsonSerializer.Deserialize<List<ProveedoresBase>>(resultado, opciones)!;
			}

		}
		

	}
}
