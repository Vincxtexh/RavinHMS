using HMS.BL;
using HMS.BL.Interfaces.Masters;
using HMS.BL.Repository;
using HMS.BL.Repository.Masters;
using HMS.Dal;
using HMS.Dal.Interfaces;
using HMS.Dal.Interfaces.Masters;
using HMS.Dal.Repository;
using HMS.Dal.Repository.Masters;

var builder = WebApplication.CreateBuilder(args);

var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder => {
        //builder.WithOrigins("http://localhost:800").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
        //builder.SetIsOriginAllowed(origin => true);
    });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserDetails, UserDetailsBL>();
builder.Services.AddTransient<IUserDetailsDL, UserDetailsDL>();

builder.Services.AddScoped<ITitleBL, TitleBL>();
builder.Services.AddTransient<ITitleDL, TitleDL>();

builder.Services.AddTransient<IDatabase, Database>();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(devCorsPolicy);
    }

 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
