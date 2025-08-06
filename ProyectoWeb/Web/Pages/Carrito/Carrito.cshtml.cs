using Abstracciones.Modelos.Productos;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Carrito;
using System.Net.Http;

namespace Web.Pages.Carrito
{
    public class CarritoModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public string EndpointActualizarProducto { get; set; }
        public string EndpointObtenerCarritoPorId { get; set; }

        public string EndpointEliminarProducto { get; set; }

        public CarritoResponse Carrito { get; set; } = new CarritoResponse();

        public CarritoModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            EndpointActualizarProducto = _configuracion.ObtenerMetodo("ApiEndPointsCarrito", "ActualizarProducto");
            EndpointObtenerCarritoPorId = _configuracion.ObtenerMetodo("ApiEndPointsCarrito", "ObtenerCarritoPorId");
            EndpointEliminarProducto = _configuracion.ObtenerMetodo("ApiEndPointsCarrito", "EliminarProducto");

            string? idUsuario = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

            string endpointBase = _configuracion.ObtenerMetodo("ApiEndPointsCarrito", "ObtenerCarritoPorUsuario");
            string endpoint = $"{endpointBase}{idUsuario}";

            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Carrito = JsonSerializer.Deserialize<CarritoResponse>(resultado, opciones) ?? new CarritoResponse();
            }
        }
    }
}