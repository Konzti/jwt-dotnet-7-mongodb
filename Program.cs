using JwtDotNet7.Settings;
using JwtDotNet7.DI;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureApplicationSettings();

builder.Services.AddConfig(builder.Configuration);
builder.Services.AddAppDependencies();
builder.Services.AddControllers();
builder.Services.AddJwt(builder.Configuration);


var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();