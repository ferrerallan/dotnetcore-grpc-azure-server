
# .NET Core gRPC Azure Server Example

## Description

This repository provides an example of a gRPC server application built with .NET Core and hosted on Azure. It demonstrates how to set up and use .NET Core to create a gRPC server for communication with gRPC clients, which is useful for developers looking to implement efficient, high-performance inter-service communication in the cloud.

## Requirements

- .NET Core SDK
- Visual Studio or any preferred IDE
- Azure account for deploying the server

## Mode of Use

1. Clone the repository:
   ```bash
   git clone https://github.com/ferrerallan/dotnetcore-grpc-azure-server.git
   ```
2. Navigate to the project directory:
   ```bash
   cd dotnetcore-grpc-azure-server
   ```
3. Build the project:
   ```bash
   dotnet build
   ```
4. Run the application locally:
   ```bash
   dotnet run
   ```
5. Deploy the application to Azure:
   ```bash
   az webapp up --name <your-app-name> --resource-group <your-resource-group>
   ```

## Implementation Details

- **Protos/**: Contains the .proto files defining the gRPC services and messages.
- **Services/**: Contains the service implementations for the gRPC server.
- **Program.cs**: The main entry point of the gRPC server application.
- **Startup.cs**: Configuration for the gRPC services and middleware.
- **appsettings.json**: Configuration file for application settings.

### Example of Use

Here is an example of how to create a gRPC server in .NET Core:

```csharp
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcAzureServerExample
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
```

This code defines a gRPC service that implements the `SayHello` method, which returns a greeting message.

## License

This project is licensed under the MIT License.

You can access the repository [here](https://github.com/ferrerallan/dotnetcore-grpc-azure-server).
