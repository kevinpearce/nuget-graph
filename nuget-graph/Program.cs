using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using nuget_graph;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddTransient<IEntrypoint, Entrypoint>();

var host = builder.Build();

var entrypoint = host.Services.GetRequiredService<IEntrypoint>();
await entrypoint.Run(args);