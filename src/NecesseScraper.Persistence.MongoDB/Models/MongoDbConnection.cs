using System.Text;

namespace NecesseScraper.Persistence.MongoDB.Models;

public record MongoDbConnection(
    string Host,
    string Port,
    string Database,
    string UserName,
    string Password
)
{
    /// <summary>
    ///     Build the connection string with the available settings.
    /// </summary>
    /// <returns>
    ///     A connecting string.
    /// </returns>
    public string GetConnectionString(bool isMongoDbAtlas = false)
    {
        if (string.IsNullOrWhiteSpace(Host)) throw new ArgumentNullException(Host);
        if (string.IsNullOrWhiteSpace(Database)) throw new ArgumentNullException(Database);
        if (!isMongoDbAtlas && string.IsNullOrWhiteSpace(Port)) throw new ArgumentNullException(Port);

        var csBuilder = new StringBuilder();
        csBuilder.Append($"{(isMongoDbAtlas ? "mongodb+srv" : "mongodb")}://");
        if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
        {
            csBuilder.Append($"{UserName}:{Password}@");
        }

        csBuilder.Append($"{Host}");
        if (!isMongoDbAtlas)
        {
            csBuilder.Append($":{Port}");
        }

        csBuilder.Append($"/{Database}");
        csBuilder.Append("?retryWrites=true&w=majority&compressors=zlib,zstd&maxPoolSize=1024");

        return csBuilder.ToString();
    }

    /// <summary>
    ///     Build the connection string with the available settings.
    /// </summary>
    /// <returns>
    ///     A connecting string.
    /// </returns>
    public override string ToString()
    {
        return GetConnectionString();
    }
}