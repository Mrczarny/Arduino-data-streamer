using System.IO.Ports;

using System;
using System.Reflection;
using Arduino_data_streamer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
 builder.WebHost.UseUrls("https://localhost:8081/", "http://localhost:8080/");



// Add services to the container.
builder.Services.AddSignalR().AddJsonProtocol();
builder.Services.AddDbContext<DataContext>(opt =>
{
    if (!String.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("mssql")))
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("mssql"));
    }
    else
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        opt.UseSqlite($"Data Source={System.IO.Path.Join(path, "data.db")}");
    }
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins(
                "http://localhost")
            .AllowCredentials()
            .AllowAnyHeader()
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod();
    });
});
//builder.Services.AddHostedService<SerialSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();
app.UseCors("localhost");
app.UseDefaultFiles();
app.UseStaticFiles();

var API = "ppp";

app.MapGet("/hello", () =>
{
    return "Hello, world!";
});

app.MapGet("/latest/{id}", (string id, DataContext _dataContext) =>
{
    try
    {
         var lastData = _dataContext.DataModels
            .Where(x => x.BotId == id)
            .OrderBy(x => x.Timestamp)
            .Last();
         lastData.Id = "";
        return Results.Ok(lastData);
    }
    catch (Exception e)
    {
        return Results.BadRequest();
    }
});

app.MapPost("/data", async (JsonElement? data,IHubContext<DataHub> _hubContext, DataContext _dataContext)  =>
{
    var dataModel = new DataModel();
    if (data == null) return Results.BadRequest();
    try
    {
        if (data.Value.GetProperty("apiKey").GetString() != API) return Results.BadRequest();
        dataModel = JsonSerializer.Deserialize<DataModel>(data.ToString());
        dataModel.Timestamp = DateTime.Now;
        if (string.IsNullOrEmpty(dataModel?.BotId)) return Results.BadRequest();
    }
    catch (System.Exception)
    {
        return Results.BadRequest();
    }

    var lastDataStamp = _dataContext.DataModels
        .Where(x => x.BotId == dataModel.BotId)
        .OrderBy(x => x.Timestamp)
        .LastOrDefault(x => x.BotId == dataModel.BotId);
    await _hubContext.Clients.Group(dataModel.BotId).SendAsync("newData", dataModel);
    if (lastDataStamp is null ||  DateTime.Now - lastDataStamp?.Timestamp  > TimeSpan.FromMinutes(5))
    {
        _dataContext.Add(dataModel);
    }

    _dataContext.SaveChanges();
    return Results.Ok();
}); 



app.MapHub<DataHub>("/signalr");

//var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
//using (var scope = scopeFactory.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<Hub>();
//    var d = roleManager.Context.Items;
//}

app.Run();

// internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
