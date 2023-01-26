namespace NecesseScraper.Persistence.Domain;

/// <summary>
///     This class represents a basic document that can be stored in MongoDb.
/// </summary>
public record BaseDocument(string BsonObjectId, DateTime AddedAtUtc)
{
    /// <summary>
    ///     Creates a new <see cref="BaseDocument" />
    /// </summary>
    protected BaseDocument() : this(string.Empty, DateTime.UtcNow)
    {
    }

    /// <summary>
    ///     Creates a new <see cref="BaseDocument" />
    /// </summary>
    /// <param name="bsonObjectId">The object id that will be used for the document.</param>
    public BaseDocument(string bsonObjectId) : this(bsonObjectId, DateTime.UtcNow)
    {
    }
}