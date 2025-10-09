using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace My.Functions;

public class EventGridBlobTrigger1
{
    private readonly ILogger<EventGridBlobTrigger1> _logger;

    public EventGridBlobTrigger1(ILogger<EventGridBlobTrigger1> logger)
    {
        _logger = logger;
    }

    [Function(nameof(EventGridBlobTrigger1))]
    public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "c309ea_STORAGE")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        _logger.LogInformation("C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}", name, content);
    }
}