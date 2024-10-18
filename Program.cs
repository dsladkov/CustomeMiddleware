using CustomMiddleware;
using CustomMiddleware.Middleware;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting web application");
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSerilog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMessageWriter, LogMessageWriter>();

var app = builder.Build();
app.UseExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    //Do work that can write to Response.
    await next.Invoke(context);
    //Do loggin or other work that doesn't write to the Response.
});

app.Use(async (context, next) => 
{
    await next.Invoke();
});

app.Run();
