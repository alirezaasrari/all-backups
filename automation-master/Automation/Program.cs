using Automation.Controllers.Interface;
using Automation.Controllers.Service;
using Automation.Model.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var myAllowSpecificOrigins = "_mySpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//builder.Services.AddTransient<IFormService, FormService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(
    option =>
    {
        option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        option.OperationFilter<SecurityRequirementsOperationFilter>();
    });


builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options => {
        Options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Hello Talaye Company")),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(myAllowSpecificOrigins);

app.UseDefaultFiles();

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
