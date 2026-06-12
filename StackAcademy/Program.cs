using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using StackAcademy.BdContextCursos;
using StackAcademy.Interface;
using StackAcademy.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CursosContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection")));

builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();


//Adiciona um serviço de Jwt Bearer (Forma de autenticação)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";


})

   .AddJwtBearer("JwtBearer", options =>
   {
       options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
       {
           //valida quem esta solicitando 
           ValidateIssuer = true,

           //valeida quem esta recebendo
           ValidateAudience = true,

           //define se o tempo de expiração sera valido
           ValidateLifetime = true,

           //forma de criptografia a valida a chave autenticação
           IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("cursos-chaves-autenticacao-webapi-dev")),

           //valida o tempo de expiração do token
           ClockSkew = TimeSpan.FromMinutes(5),

           //nome da issuer para onde esta vindo
           ValidIssuer = "api_filmes",

           //nome da autenticação para onde esta indo
           ValidAudience = "api_filmes"

       };
   });


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Version = "v1",
        Title = "Filmes API",
        Description = "Uma api com um catalogo de filmes",
        TermsOfService = new Uri("https://examplo.com/terms"),
        Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "matheusbeckerstanzione",
            Url = new Uri("http://github.com/matheusbeckerstanzione")
        },
        License = new Microsoft.OpenApi.OpenApiLicense
        {
            Name = "Example license",
            Url = new Uri("http://example.com/license")
        }
    });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT"
    });

    option.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] =
        Array.Empty<string>().ToList()
    });

});

builder.Services.AddCors(opions =>
{
    opions.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

//Adiciona servico de controle
builder.Services.AddControllers();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    }
    );
}

app.UseCors("CorsPolicy");
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


