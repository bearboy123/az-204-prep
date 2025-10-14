
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Configuration;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();
