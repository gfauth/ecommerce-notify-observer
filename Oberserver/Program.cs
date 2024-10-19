using Microsoft.OpenApi.Models;
using Oberserver.Data;
using Oberserver.Data.Context;
using Oberserver.Data.Interfaces;
using Oberserver.Domain.Interfaces;
using Oberserver.Domain.Services;
using Oberserver.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification API", Version = "v1" });
    c.IncludeXmlComments(string.Format(@"{0}\Oberserver.xml", System.AppContext.BaseDirectory));
    c.SchemaFilter<IgnoreNullValuesSchemaFilter>();
});

//Dependence injection configuration

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISqlServerContext, SqlServerContext>();

//Configure database connection

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();