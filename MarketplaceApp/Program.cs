using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MarketplaceApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using MarketplaceApp.Models;
using Microsoft.OpenApi.Writers;
using Microsoft.AspNetCore.Identity;
using MarketplaceApp.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
});
//builder.Services.AddControllers();


//Add authentication services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

//!!!
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

//!!!
app.MapGet("/accessdenied", async (HttpContext context) =>
{
    context.Response.StatusCode = 403;
    await context.Response.WriteAsync("Access denied");
});

app.MapPost("/login", (User loginData) =>
{
    User? _user;
    using (var scope = app.Services.CreateScope())
    {
        var service = scope.ServiceProvider;
        var _context = service.GetService<ApplicationDbContext>();
        _user = _context.user.FirstOrDefault(u => u.email == loginData.email && u.password == loginData.password);
    }
    if (_user is null) return Results.Unauthorized();

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Role, _user.category),
        new Claim(ClaimTypes.Name, _user.email)
    };

    ///!!!
    

var jwt = new JwtSecurityToken(
    issuer: AuthOptions.ISSUER,
    audience: AuthOptions.AUDIENCE,
    claims: claims,
    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(15)),
    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

    var response = new
    {
        access_token = encodedJwt,
        user = _user,
};
return Results.Json(response);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //

}

app.UseHttpsRedirection();

//Add Authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "Marketplace123_app_321_secretKey";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
