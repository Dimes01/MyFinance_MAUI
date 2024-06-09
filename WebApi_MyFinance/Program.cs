using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPI_MyFinance;
using WebAPI_MyFinance.Models;

#region builder

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = AuthOptions.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
        };
    });
builder.Services.AddAuthorization();

#pragma warning disable CS8600
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
#pragma warning restore CS8600

builder.Services.AddDbContext<MyFinanceContext>(options => options.UseNpgsql(connection));

#endregion

#region app

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var optionsBuilder = new DbContextOptionsBuilder<MyFinanceContext>();
var options = optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).Options;


app.MapPost("/login", async (User user, MyFinanceContext context) =>
{
    var PasswordHash = Encoding.UTF8.GetString(AuthOptions.GetHashSHA256(user.PasswordHash));
    var person = await context.Users.FirstOrDefaultAsync(p => p.Email == user.Email && p.PasswordHash == PasswordHash);
    if (person is null) return Results.Unauthorized();

    var claims = new List<Claim> { new(ClaimTypes.Name, user.Nickname) };
    var jwt = new JwtSecurityToken(
        issuer: AuthOptions.Issuer,
        audience: AuthOptions.Audience,
        claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

    var response = new
    {
        access_token = encodedJwt,
        nickname = user.Nickname
    };

    return Results.Json(response);
});

// Debug
app.MapGet("/hash/id/{str}", (string str) => AuthOptions.GetHashSHA256(str));

app.MapPost("/register", async (User user, MyFinanceContext context) =>
{
    if (await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id) is not null)
        return Results.Conflict("Данный пользователь уже существует...");
    if (await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email) is not null)
        return Results.Conflict("Данный Email уже зарегистрирован...");
    if (await context.Users.FirstOrDefaultAsync(u => u.Nickname == user.Nickname) is not null)
        return Results.Conflict("Данный никнейм уже занят...");
    var person = new User
    {
        Id = user.Id,
        Email = user.Email,
        PasswordHash = Encoding.UTF8.GetString(AuthOptions.GetHashSHA256(user.PasswordHash)),
        Nickname = user.Nickname
    };
    context.Users.Add(person);
    await context.SaveChangesAsync();
    return Results.Json(person);
});

app.Run();

#endregion
