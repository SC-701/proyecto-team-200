using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Proveedores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Proveedor
{
	public class MostrarProveedoresModel : PageModel
	{
		private readonly IConfiguracion _configuracion;
		public IList<ProveedoresBase> proveedores { get; set; } = new List<ProveedoresBase>();

		public MostrarProveedoresModel(IConfiguracion configuracion)
		{
			_configuracion = configuracion;
		}

		public async Task OnGet()
		{
			string endpoint = _configuracion.ObtenerMetodo("ApiEndPointsProveedores", "ObtenerProveedores");
			using var http = new HttpClient();
			var resp = await http.GetAsync(endpoint);
			resp.EnsureSuccessStatusCode();
			if (resp.StatusCode == HttpStatusCode.OK)
			{
				var json = await resp.Content.ReadAsStringAsync();
				proveedores = JsonSerializer.Deserialize<List<ProveedoresBase>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ProveedoresBase>();
			}
		}

		// GET: devuelve el partial del modal para crear
		public IActionResult OnGetFormularioModal()
		{
			var m = new ProveedoresBase
			{
				PROVEEDOR_ID = Guid.NewGuid(),   // tu SP necesita ID creado
				ESTADO_ID = 1,                   // por defecto Activo
				Fecha_Registro = DateTime.UtcNow // requerido por tu modelo
			};
			return Partial("_FormularioModalProveedor", m);
		}

		// POST: guarda proveedor (llama a la API)
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> OnPostAgregarProveedor(ProveedoresBase proveedor)
		{
			if (!ModelState.IsValid)
				return Partial("_FormularioModalProveedor", proveedor);

			var endpoint = _configuracion.ObtenerMetodo("ApiEndPointsProveedores", "AgregarProveedor");
			using var http = new HttpClient();

			var content = new StringContent(JsonSerializer.Serialize(proveedor), Encoding.UTF8, "application/json");
			var resp = await http.PostAsync(endpoint, content);

			if (resp.IsSuccessStatusCode)
				return new JsonResult(new { ok = true });

			// si falla, devuelve el partial con error global (se verá en el resumen)
			var body = await resp.Content.ReadAsStringAsync();
			ModelState.AddModelError(string.Empty, $"Error API: {body}");
			return Partial("_FormularioModalProveedor", proveedor);
		}
	}
}
