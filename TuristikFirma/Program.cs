using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TuristikFirma;
using TuristikFirma.Abstractions;
using TuristikFirma.DbContext.Repositories;
using TuristikFirma.Models;
using TuristikFirma.Services;
using TuristikFirma.TuristikFirma.DataAccess;
using TuristikFirma.TuristikFirma.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));

var secretKey = builder.Configuration.GetSection("JWTSettings:SecretKey").Value;
var issuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value;
var audience = builder.Configuration.GetSection("JWTSettings:Audience").Value;
var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        ValidateLifetime = true,
        IssuerSigningKey = signInKey,
        ValidateIssuerSigningKey = true
    };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TuristikFirmaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(TuristikFirmaDbContext))));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<TuristikFirmaDbContext>();

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("OnlyAdmin", policyBuilder => policyBuilder.RequireClaim("Role", "Admin"));
});

builder.Services.AddScoped<IHelperService, HelperService>();
builder.Services.AddScoped<IToursService, ToursService>();
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
builder.Services.AddScoped<IToursRepository, ToursRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.UseCors(x =>
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins().AllowAnyOrigin();
    x.WithMethods().AllowAnyMethod();
});

app.Run();

