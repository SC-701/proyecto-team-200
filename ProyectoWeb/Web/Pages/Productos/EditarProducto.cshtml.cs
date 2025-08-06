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

        


        
    }
}
