using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;

[ApiController]
[Route("api/usuarios")]
public class ApiUsuarioController : ControllerBase{

    private readonly IMongoCollection<Usuario> collection;

    public ApiUsuarioController(){
        MongoClient client = new MongoClient(CadenasConexion.MONGO_DB);
        var db = client.GetDatabase("Escuela_clemente");
        this.collection = db.GetCollection<Usuario>("usuarios");
    }

    [HttpGet]
    public IActionResult ListarUsuarios(string? texto){
        var filter = FilterDefinition<Usuario>.Empty;
        if (!string.IsNullOrWhiteSpace(texto))
        {
            var filterNombre = Builders<Usuario>.Filter.Regex(u => u.Nombre, new BsonRegularExpression(texto, "i"));
            var filterCorreo = Builders<Usuario>.Filter.Regex(u => u.Correo, new BsonRegularExpression(texto, "i"));

            filter = Builders<Usuario>.Filter.Or(filterNombre, filterCorreo);
        }
        var list = this.collection.Find(filter).ToList();

        return Ok(list);
    }

    //CRUD
    //Create
    //Read
    //Update
    //Delete
    [HttpDelete("{id}")]
    public IActionResult Delete(string id){
        var filter = Builders<Usuario>.Filter.Eq(x => x.Id, id);
        var item = this.collection.Find(filter).FirstOrDefault();
        if(item != null){
            this.collection.DeleteOne(filter);
        }

        return NoContent();
    }

    [HttpPost]

    public  IActionResult Create (UsuarioRequest model)
    {
        // 1. Validar el modelo para que contenga datos
        if (string.IsNullOrWhiteSpace(model.Correo))
        {
         return BadRequest("El correo es requerido");
        }

        if (string.IsNullOrWhiteSpace(model.Password))
        {
         return BadRequest("El password es requerido");
        }

        if (string.IsNullOrWhiteSpace(model.Nombre))
        {
         return BadRequest("El nombre es requerido");
        }
        
        //Validar que el correo no exista
        var filter = Builders<Usuario>.Filter.Eq(x => x.Correo, model.Correo);
        var item = this.collection.Find(filter).FirstOrDefault();
        if(item != null){
            return BadRequest("El correo " + model.Correo + " ya existe en la base de datos");
        }
        Usuario bd = new Usuario();
        bd.Nombre = model.Nombre;
        bd.Correo = model.Correo;
        bd.Password = model.Password;

        this.collection.InsertOne(bd);

        return Ok();
    }

    //Read
    [HttpGet("{id}")]
    public IActionResult Read(string id){
        var filter = Builders<Usuario>.Filter.Eq(x => x.Id, id);
        var item = this.collection.Find(filter).FirstOrDefault();
        if(item == null){
            return NotFound("No existe un usuario con el id proporcionado");
        }

        return Ok(item);
    }
    //Update
    [HttpPut("{id}")]
    public IActionResult Update(string id,UsuarioRequest model){
        if(string.IsNullOrWhiteSpace(model.Nombre)){
            return BadRequest("El nombre es requerido");
        }

        if(string.IsNullOrWhiteSpace(model.Correo)){
            return BadRequest("El correo es requerido");
        }

        if(string.IsNullOrWhiteSpace(model.Password)){
            return BadRequest("El password es requerido");
        }

        var filter = Builders<Usuario>.Filter.Eq(x => x.Id, id);
        var item = this.collection.Find(filter).FirstOrDefault();
        if(item == null){
            return NotFound("No existe un usuario con el id proporcionado");
        }

        //Validar que el correo no exista
        var filterCorreo = Builders<Usuario>.Filter.Eq(x => x.Correo, model.Correo);
        var itemExistente = this.collection.Find(filterCorreo).FirstOrDefault();
        if(itemExistente != null && itemExistente.Id != id){
            return BadRequest("El correo " + model.Correo + " ya existe en la base de datos");
        }

        var updateOptions = Builders<Usuario>.Update
            .Set(x => x.Nombre, model.Nombre)
            .Set(x => x.Correo, model.Correo)
            .Set(x => x.Password, model.Password);

        this.collection.UpdateOne(filter, updateOptions);

        return Ok();
    }
 }