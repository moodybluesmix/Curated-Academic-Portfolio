using Microsoft.AspNetCore.Mvc;
using AcademicPortfolio.Business.Services;
using AcademicPortfolio.Data.Context;
using Microsoft.EntityFrameworkCore;
using AcademicPortfolio.Shared.Entities;

namespace AcademicPortfolio.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorksController : ControllerBase
{
    private readonly WorkService _workService;
    private readonly GeminiAIService _aiService;
    private readonly AppDbContext _context;

    // Servisleri Constructor üzerinden içeri alıyoruz (Dependency Injection)
    public WorksController(WorkService workService, GeminiAIService aiService, AppDbContext context)
    {
        _workService = workService;
        _aiService = aiService;
        _context = context;
    }

    // 1. Tüm Yayınları Getir
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var works = await _context.AcademicWorks.ToListAsync();
        return Ok(works);
    }

    // 2. DOI ile İnternetten (Crossref) Veri Çek
    [HttpGet("fetch/{*doi}")]
    public async Task<IActionResult> GetMetadata(string doi)
    {
        if (string.IsNullOrEmpty(doi)) return BadRequest("DOI boş olamaz.");

        doi = System.Net.WebUtility.UrlDecode(doi);
        var result = await _workService.FetchMetadataFromDoiAsync(doi);

        if (result == null)
            return NotFound($"DOI verisi çekilemedi. Girilen DOI: {doi}");

        return Ok(result);
    }

    // 3. Yayını Veritabanına Kaydet
    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] AcademicWork work)
    {
        if (work == null) return BadRequest("Veri boş olamaz.");

        var success = await _workService.SaveWorkAsync(work);

        if (success == "OK") return Ok(new { message = "Yayın başarıyla kaydedildi." });
        return BadRequest(new { message = success });
    }

    // 4. Yayını Veritabanından Sil
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var work = await _context.AcademicWorks.FindAsync(id);
        if (work == null) return NotFound("Yayın bulunamadı.");

        _context.AcademicWorks.Remove(work);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Yayın başarıyla silindi." });
    }

    // 5. KRİTİK: Yapay Zeka Sorgu Endpoint'i (MCP Protokolü Vizyonu)
    // Dashboard'daki AI asistanından gelen soruları burası cevaplar
    [HttpPost("ai-query")]
    public async Task<IActionResult> AskAI([FromBody] AIRequest request)
    {
        if (string.IsNullOrEmpty(request.Prompt)) return BadRequest("Soru boş olamaz.");

        // Veritabanındaki tüm yayınları AI'a context (bağlam) olarak gönderiyoruz
        var allWorks = await _context.AcademicWorks.ToListAsync();

        // Gemini servisini çağırıp yanıtı alıyoruz
        var aiResponse = await _aiService.AskAboutPortfolioAsync(allWorks, request.Prompt);

        return Ok(new { response = aiResponse });
    }
}

// AI İsteği için yardımcı sınıf
public class AIRequest
{
    public string Prompt { get; set; } = string.Empty;
}