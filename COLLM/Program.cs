using DAL.Utils;
using COLLM.Extensions;

var builder = WebApplication.CreateBuilder(args);

Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", "python311.dll");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterLmacoDb(builder.Configuration);
builder.Services.AddRepositories();

builder.Services.RegisterServices();

//temp
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader())
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();