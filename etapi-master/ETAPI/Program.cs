using ETAPI.Context;
using ETAPI.Interfaces;
using ETAPI.Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;

//var myAllowSpecificOrigins = "_mySpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers();
//builder.Services.AddControllers().AddNewtonsoftJson(options =>
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);

// Here we add services to the project
builder.Services.AddTransient<IFormService,FormService>();
builder.Services.AddTransient<IFormChartService,FormChartService>();
builder.Services.AddTransient<ICodeFormXService,CodeFormXService>();
builder.Services.AddTransient<IFormUnitService,FormUnitService>();
builder.Services.AddTransient<ITaeedService,TaeedService>();
builder.Services.AddTransient<IVariableService,VariableService>();
builder.Services.AddTransient<IUnitService,UnitService>();
builder.Services.AddTransient<IIfService,IfService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Here we add services and interfaces to the whole project
//builder.Services.AddScoped<IService, Service>();
builder.Services.AddSwaggerGen();

// Add datacontext to the whole project
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Add cores will be modified here
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: myAllowSpecificOrigins,
//        builder =>
//        {
//            builder.WithOrigins("http://localhost:4200")
//            .AllowAnyMethod()
//            .AllowAnyHeader();
//        });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors(myAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
