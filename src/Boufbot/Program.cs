using Boufbot.Extensions;
using Microsoft.Extensions.Hosting;

await Host
    .CreateApplicationBuilder(args)
    .UseServicesAndConfiguration()
    .Build()
    .RunAsync();