using Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConnectionHelper ch = new ConnectionProvidor { connectionString = builder.Configuration.GetConnectionString("SqlConnection") };
builder.Services.AddSingleton<ConnectionHelper>(ch);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Set the path of the XML documentation file
    var xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "ApiDoc.xml");
    c.IncludeXmlComments(xmlPath);
}); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseReDoc(c =>
{
    c.SpecUrl = "/swagger/v1/swagger.json";
    c.DocumentTitle = "Ecommerce API Documentation";
   
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
