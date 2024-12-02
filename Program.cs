using BlogAspNet.Data;
using BlogAspNet.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true; // * DESABILITA O RESULTADO DA VALIDACAO PADRAO DO MODELSTATE *
    });

builder.Services.AddDbContext<BlogDataContext>();
builder.Services.AddTransient<TokenService>(); //* Sempre criar um novo tokenService

var app = builder.Build();
app.MapControllers();

app.Run();