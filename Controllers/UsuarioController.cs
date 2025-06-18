using Microsoft.AspNetCore.Mvc;

[Route("usuario")]
public class UsuarioController : Controller
{
    public IActionResult Index(){
        return View();
    }
    [Route("editar/{id?}")]
    public IActionResult Editar(string? id)
   {
    ViewBag.ID = id;
    return View();
   }

}