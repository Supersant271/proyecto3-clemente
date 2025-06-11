using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class usuario 
{
    [BsonId, BsonRepresentation(BsonTyp.Objectd)]

    public string? Id {get; set; }

    [BsonElement("nombre")]
    public string nombre { get; set;} = string.Empty;

    [BsonElement("password")]
    public string password { get; set;} = string.Empty;

    [BsonElement("correo")]
    public string correo { get; set;} = string.Empty;
}