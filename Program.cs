using System.Text;
using BlogAspNet;
using BlogAspNet.Data;
using BlogAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigureAuthentication(builder);
ConfigureMvc(builder);
ConfigureServices(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
LoadConfiguration(app);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    Console.WriteLine("Estou no ambiente de produção!!");
}

app.Run();

void LoadConfiguration(WebApplication app)
{
    Configuration.JwtKey = app.Configuration.GetValue<string>("JwtKey");
    Configuration.ApiKeyname = app.Configuration.GetValue<string>("ApiKeyname");
    Configuration.ApiKey = app.Configuration.GetValue<string>("ApiKey");

    var smtp = new Configuration.SmtpConfiguration(); // crio uma instancia da classe SmtpConfiguration que esta dentro da classe Configuration
    app.Configuration.GetSection("Smtp").Bind(smtp); // popula a var smtp com a section de Smtp do appsettings.json
    Configuration.Smtp = smtp; // passo o valor da minha smpt para propriedade Smtp localizada na classe Configuration
}

void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddMemoryCache();
    builder.Services
        .AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter =
                true; // * DESABILITA O RESULTADO DA VALIDACAO PADRAO DO MODELSTATE *
        })
        .AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
}

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<BlogDataContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });
    builder.Services.AddTransient<TokenService>(); //* Sempre criar um novo tokenService
    builder.Services.AddTransient<EmailService>();
}