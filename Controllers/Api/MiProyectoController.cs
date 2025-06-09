using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;


[Route("mi-proyecto")]
public class MiProyectoController : ControllerBase{

    [HttpGet("integrante")]
    public IActionResult Integrantes() {
        Integrantes proyecto = new Integrantes
        {
            NombreIntegrante1 = "Santiago Abad Clemente Arredondo"
        };
        return Ok(proyecto);
    }

    [HttpGet("presentacion")]
    public IActionResult Presentacion()
    {
        var client = new MongoClient(CadenasConexion.MONGO_DB);
        var db = client.GetDatabase("Santiago Abad Clemente Arredondo|");
        var collection = database.GetCollection<Equipo>("Equipo");

        var Filter = FilterilterDefinition<Equipo>.Empty;
        var item = collection.Find(Filter). FirstOrDefault();

        return Ok(lista);
    }

}