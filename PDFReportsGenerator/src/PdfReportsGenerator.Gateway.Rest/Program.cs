using PdfReportsGenerator.Application.Configurations;
using PdfReportsGenerator.Gateway.Rest.Configurations;
using PdfReportsGenerator.Infrastructure.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureRestGateway();
builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.ConfigureInfrastructure(builder.Configuration);

builder.Host.ConfigureLogging(builder.Configuration);

var app = builder.Build();

app.ApplyRestGatewaySettings();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.AddSignalR();
app.AddPrometheus();
app.UseExceptionHandler();
app.Run();