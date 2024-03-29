using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddCubeManagerServices();
builder.Services.AddCubeManagerCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseRouting();


app.UseCubeManagerEndpoints();
app.UseCubeManagerWebSockets();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
