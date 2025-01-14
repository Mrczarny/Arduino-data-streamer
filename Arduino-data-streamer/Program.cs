using System.IO.Ports;

using System;
using Arduino_data_streamer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://0.0.0.0:7207/", "http://0.0.0.0:5299/");


// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("localhost", policy =>
    {
        policy.WithOrigins("http://localhost",
            "http://localhost:4200");
        policy.AllowCredentials();
        policy.AllowAnyHeader();
    });
});
//builder.Services.AddHostedService<SerialSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("localhost");
app.UseDefaultFiles();
app.UseStaticFiles();



app.MapGet("/hello", () =>
{
    return "Hello, worrld!";
});

app.MapPost("/data", async (object data,IHubContext<DataHub> _hubContext)  =>
{
    var dataModel = new DataModel();
    if (data == null) return Results.BadRequest();
    try
    {
        dataModel = JsonSerializer.Deserialize<DataModel>(data.ToString());
        if (dataModel == null) return Results.BadRequest();
    }
    catch (System.Exception)
    {
        return Results.BadRequest();
    }
    await _hubContext.Clients.All.SendAsync("newData", data);
    return Results.Ok();
}); 


app.MapHub<DataHub>("/signalr");

app.Run();

// internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
