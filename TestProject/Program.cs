using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<INewsFeedService, NewsFeedService>();

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Content-Security-Policy",
        "default-src 'self'; " +
        "font-src 'self' https: data:; " +    
        "img-src 'self' https: data: blob:; " +
        "script-src 'self' https: 'unsafe-inline'; " +
        "style-src 'self' 'unsafe-inline' https:; " +  
        "connect-src 'self' https: ws: wss: http: localhost;");  
    await next();
});

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();