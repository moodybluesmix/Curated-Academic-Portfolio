using Microsoft.AspNetCore.Mvc;
using AcademicPortfolio.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AcademicPortfolio.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReportController(AppDbContext context)
    {
        _context = context;
    }

    // Bu metod veritabanındaki tüm yayınları çeker ve Excel'in doğrudan açabileceği CSV formatına dönüştürür.
    // Erişim Adresi: https://localhost:7231/api/Report/export-excel
    [HttpGet("export-excel")]
    public async Task<IActionResult> ExportToExcel()
    {
        try
        {
            // 1. Veritabanından tüm yayınları tarihe göre sıralı çekiyoruz
            var works = await _context.AcademicWorks
                .OrderByDescending(w => w.PublicationYear)
                .ToListAsync();

            if (works == null || works.Count == 0)
            {
                return BadRequest("Raporlanacak yayın bulunamadı. Lütfen önce yayın ekleyin.");
            }

            // 2. CSV (Excel Uyumlu) içeriğini oluşturuyoruz
            var builder = new StringBuilder();

            // Başlık satırı (TÜBİTAK ve Akademik Teşvik formatına uygun sütunlar)
            // Sütunları ayırmak için ';' (noktalı virgül) kullanıyoruz, Excel bunu otomatik algılar.
            builder.AppendLine("ID;Yayin Basligi;DOI;Dergi/Konferans;Yil;Tur;Q Kategorisi;Atif Sayisi");

            foreach (var work in works)
            {
                // Verilerin içindeki noktaları ve virgülleri bozmamak için başlıkları temizleyerek ekliyoruz
                string safeTitle = work.Title.Replace(";", ",");
                string safeJournal = work.JournalName.Replace(";", ",");

                builder.AppendLine($"{work.Id};{safeTitle};{work.DOI};{safeJournal};{work.PublicationYear};{work.PublicationType};{work.QCategory};{work.CitationCount}");
            }

            // 3. Türkçe karakter desteği için UTF-8 BOM (Byte Order Mark) ekliyoruz
            var csvData = Encoding.UTF8.GetBytes(builder.ToString());
            var result = Encoding.UTF8.GetPreamble().Concat(csvData).ToArray();

            // 4. Dosyayı kullanıcıya "Akademik_Rapor.csv" adıyla gönderiyoruz
            string fileName = $"Akademik_Portfoy_Rapor_{DateTime.Now:yyyyMMdd_HHmm}.csv";
            return File(result, "text/csv", fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Rapor oluşturulurken bir hata oluştu: {ex.Message}");
        }
    }
}