using TingraService.IOC;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaApi", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var tryDbConnection = Environment.GetEnvironmentVariable("ConnectionStrings__TingraDb") is not null || true;

builder.Services.InyectarDependencias(builder.Configuration, builder.Environment, tryDbConnection);
builder.Services.AddHealthChecks();

var app = builder.Build();
app.MapHealthChecks("/health");
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PoliticaApi");

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
