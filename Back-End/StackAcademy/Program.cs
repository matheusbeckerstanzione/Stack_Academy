using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using StackAcademy.BdContextCursos;
using StackAcademy.Interface;
using StackAcademy.Repositories;

var builder = WebApplication.CreateBuilder(args);

// adiciona o contexto ao banco de dados
builder.Services.AddDbContext<CursosContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaltConnection")));

// adiciona o repositorio
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();

//Adicionar servicos de jwt Bearrer(forma de autenticação)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearrer";
    options.DefaultChallengeScheme = "JwtBearrer";

})
    .AddJwtBearer("JwtBearrer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            //valida quem esta solicitando o token
            ValidateIssuer = true,
            //valida quem esta recebendo o token
            ValidateAudience = true,
            //valida o tempo de expiração do token
            ValidateLifetime = true,
            //Forma de Criptografia e valida  a chave de autentificacao
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("StackAcademy-chave-autenticacao-webapi-dev")),
            //Valida o tempo de expiracao do tonken
            ClockSkew = TimeSpan.FromMinutes(5),
            //nome do issuer (de onde esta vindo)
            ValidIssuer = "api_StackAcademy",
            //nome do audience(para onde esta indo)
            ValidAudience = "api_StackAcademy"
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Version = "v1",
        Title = "StackAcademy API",
        Description = "API para gerenciamento de cursos",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "Joao-Victor",
            Url = new Uri("https://example.com/contato")
        },
        License = new Microsoft.OpenApi.OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/licenca")
        }

    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT:"

    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] =
        Array.Empty<String>().ToList()
    });

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});



//Adiciona serviços de controler 
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

// Adiciona o mapeamentos de Controllers

app.MapControllers();

app.Run();