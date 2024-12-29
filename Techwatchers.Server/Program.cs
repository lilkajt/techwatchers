using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TechwatchersContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), 
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevOrigin", policy =>
    {
        policy.WithOrigins("https://127.0.0.1:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularDevOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGroup("/api").MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
