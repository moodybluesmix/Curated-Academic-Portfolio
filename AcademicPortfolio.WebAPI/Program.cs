using AcademicPortfolio.Data.Context;
using AcademicPortfolio.Business.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabaný Yapýlandýrmasý (PostgreSQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Servis Kayýtlarý
builder.Services.AddHttpClient<WorkService>();
builder.Services.AddControllers();

// 3. CORS ÝZNÝ: Dashboard'un (7177) API (7231) ile konuþmasýný saðlar.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.AllowAnyOrigin()   // Geliþtirme aþamasýnda tüm portlara izin ver
              .AllowAnyMethod()   // GET, POST vb. tüm metodlara izin ver
              .AllowAnyHeader();  // Tüm baþlýklara izin ver
    });
});

// 4. Swagger Oluþturucu Kaydý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<GeminiAIService>();
var app = builder.Build();

// 5. Geliþtirme Ortamý ve Swagger Arayüzü Ayarlarý
if (app.Environment.IsDevelopment())
{
    // Swagger JSON dosyasýný üretir
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        // GÖRSELDEKÝ 404 HATASINI DÜZELTEN KRÝTÝK SATIR:
        // Swagger UI'a, "v1" isimli JSON dosyasýný nerede bulacaðýný tam olarak söylüyoruz.
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Academic Portfolio API v1");

        // Swagger'ýn tarayýcýda /swagger adresinde açýlmasýný saðlar
        options.RoutePrefix = "swagger";
    });
}

// 6. MIDDLEWARE SIRALAMASI (Önemli: Cors her zaman Redirect'ten önce gelir)
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();