using DAL.Utils;

var builder = WebApplication.CreateBuilder(args);

Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", "python311.dll");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterLmacoDb(builder.Configuration);
builder.Services.AddRepositories();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();