using Microsoft.AspNetCore.Mvc;
using proyecto3_clemente.Models;

namespace proyecto3_clemente.Controllers.Api
{
    [ApiController]
    [Route("proyecto3-clemente")]
    public class MiProyectoController : ControllerBase
    {
        [HttpGet("integrantes")]
        public ActionResult<MiProyecto> Integrantes()
        {
            var proyecto = new MiProyecto
            {
                NombreIntegrante1 = "Clemente Santiago"
            };

            return Ok(proyecto);
        }
    }
}