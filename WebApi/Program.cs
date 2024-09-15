using Serilog;
using SharedKernal;
using SharedKernal.Correlation;
using SharedKernal.Migrations;
using SharedKernal.ModuleInstaller;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, config) =>
{
    config.ReadFrom.Configuration(ctx.Configuration);
});

builder.Services.AddKernalServices(builder.Configuration);


var mvcBuilder = builder.Services.AddAppControllers();

var installRes = builder.Services.InstallModules(mvcBuilder, builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(options => { });

app.CheckDatabaseMigration(installRes.RegisteredDatabases.ToArray());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(SharedConstants.CrossCutting.CROSPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<AppCorrelationIdMiddleware>();

app.MapControllers();

app.Run();
