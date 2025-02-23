var builder = WebApplication.CreateBuilder(args);

// Hinzufügen der Services zum Container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Hinzufügen der CORS-Richtlinie
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Konfiguration des Connection-Strings (definiert in appsettings.json unter "ConnectionStrings:DefaultConnection")
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
DevExpress.Xpo.XpoDefault.DataLayer = DevExpress.Xpo.XpoDefault.GetDataLayer(connectionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);

var app = builder.Build();

// Konfiguration der HTTP-Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
