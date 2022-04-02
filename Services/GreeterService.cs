using Grpc.Core;
using dotnetcore_grpc_server_example;
using Azure.Storage.Blobs;

namespace dotnetcore_grpc_server_example.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public record Sale(string Country, string Region);
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        IConfiguration Configuration = new ConfigurationBuilder()
                              .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                              .AddEnvironmentVariables()
                              .Build();
        GetFilesFromBlobStorage(Configuration);
        var file = Path.Combine("files", "1MSales.csv");
        var message = "";
        
        using (var streamReader = System.IO.File.OpenText(file))
        {
          Console.ForegroundColor=ConsoleColor.Cyan;
          Console.WriteLine("Reading stream..");
          var line = streamReader.ReadToEnd();
          Console.WriteLine("Reading stream... Done");
          message = line ;                        
        }

        Console.ForegroundColor=ConsoleColor.Green;
        Console.WriteLine("Returning data");
        return Task.FromResult(new HelloReply
        {
          Message = "poc " + message
        });
    }

    public static async void GetFilesFromBlobStorage(IConfiguration configuration) {
      Console.ForegroundColor=ConsoleColor.Yellow;
      Console.WriteLine("Downloading from BlobStorage..");
      string connectionString = configuration["Azure:Connection"];
      string containerName =  configuration["Azure:Container"];

      var blobServiceClient = new BlobServiceClient(connectionString);
      BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

      var blobs = containerClient.GetBlobs();

      foreach (var blob in blobs) {
        BlobClient blobClient = containerClient.GetBlobClient(blob.Name);
        blobClient.DownloadTo(Path.Combine("files")+ "/"+blob.Name);
      }
      Console.WriteLine("Downloading from BlobStorage... Done");
    }
}
