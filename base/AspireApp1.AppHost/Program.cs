var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebApplication1>("web")
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Forecast API (HTTP)";
        url.Url += "/weatherforecast";
    })
    .WithUrlForEndpoint("https", url =>
    {
        url.DisplayText = "Forecast API (HTTPS)";
        url.Url += "/weatherforecast";
    })
;

builder.Build().Run();
