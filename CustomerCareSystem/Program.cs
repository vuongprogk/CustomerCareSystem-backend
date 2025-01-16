using System.Text;
using CustomerCareSystem.DataAccess;
using CustomerCareSystem.Interface;
using CustomerCareSystem.Repository;
using CustomerCareSystem.Util.SD;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// register cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors.CorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:5173").
        AllowAnyMethod().
        AllowAnyHeader();
    });
});

// register db context
builder.Services.AddScoped<ApplicationDbContext>();
// register repository
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICustomerFormRepository, CustomerFormRepository>();
builder.Services.AddScoped<IActionRepository, ActionRepository>();


builder.Services.AddAuthentication(option =>
{
    option.DefaultScheme =
        option.DefaultChallengeScheme =
            option.DefaultAuthenticateScheme =
                option.DefaultForbidScheme = option.DefaultSignInScheme =
                    option.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        RoleClaimType = RoleValue.RoleName
    };
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:Configuration"];
    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
    {
        AbortOnConnectFail = false,
        EndPoints = { options.Configuration }
    };
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(Cors.CorsPolicy);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();