using Abstracciones.Modelos.Productos;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Carrito;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

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
                .FirstOrDefault(c => c.Type == "idUsuario")?.Value;

            string endpointBase = _configuracion.ObtenerMetodo("ApiEndPointsCarrito", "ObtenerCarritoPorUsuario");
            string endpoint = $"{endpointBase}{idUsuario}";

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Carrito = JsonSerializer.Deserialize<CarritoResponse>(resultado, opciones);
            }
            else
            {
                Carrito = new CarritoResponse { Productos = new List<CarritoProductoResponse>(), Total = 0 };
            }
        }

        public async Task<IActionResult> OnGetRefrescarAsync()
        {
            string? idUsuario = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

            string endpointBase = _configuracion.ObtenerMetodo("ApiEndPointsCarrito", "ObtenerCarritoPorUsuario");
            string endpoint = $"{endpointBase}{idUsuario}";

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Carrito = JsonSerializer.Deserialize<CarritoResponse>(resultado, opciones);

                return new JsonResult(new
                {
                    success = true,
                    productos = Carrito.Productos.Select(p => new {
                        id = p.CarritoProductoId,
                        stock = p.StockDisponible,
                    }),
                    total = Carrito.Total
                });
            }

            return new JsonResult(new { success = false });
        }
    }
}