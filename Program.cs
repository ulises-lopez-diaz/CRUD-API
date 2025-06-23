using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Pipeline configuration
app.UseGlobalErrorHandler();           // 1. Manejo de errores
app.UseTokenAuthentication();         // 2. Autenticación por token
app.UseRequestResponseLogging();      // 3. Logging de peticiones/respuestas

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
