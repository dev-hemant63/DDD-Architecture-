using Infrastructure;
using Microsoft.OpenApi.Models;
using WEBAPI.Extension;
using WEBAPI.Model;

var builder = WebApplication.CreateBuilder(args);

ConnectionHelper ch = new ConnectionProvidor { connectionString = builder.Configuration.GetConnectionString("SqlConnection") };
builder.Services.AddSingleton<ConnectionHelper>(ch);
ServiceCollectionExtension.RegisterService(builder.Services, builder.Configuration);
AppSettings appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);
builder.Services.AddControllers();
builder.Services.AddSingleton<AppSettings>(appSettings);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce API", Version = "v1" });
    var xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "ApiDoc.xml");
    c.IncludeXmlComments(xmlPath);
    c.UseAllOfToExtendReferenceSchemas();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standard authorization header using the bearer scheme(\"Bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
       {
           new OpenApiSecurityScheme
           {
              Reference = new OpenApiReference
              {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
              }
           },
           new string[] { }
       }
     });
});

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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
