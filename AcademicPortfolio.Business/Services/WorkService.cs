using AcademicPortfolio.Data.Context;
using AcademicPortfolio.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace AcademicPortfolio.Business.Services;

public class WorkService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;

    public WorkService(HttpClient httpClient, AppDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "AcademicPortfolioProject/1.0");
    }

    // DOI Sorgulama Metodu (Öncekiyle aynı kalabilir)
    public async Task<AcademicWork?> FetchMetadataFromDoiAsync(string doi)
    {
        try
        {
            var url = $"https://api.crossref.org/works/{doi}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadFromJsonAsync<System.Text.Json.JsonElement>();
            var message = content.GetProperty("message");

            return new AcademicWork
            {
                DOI = doi,
                Title = message.GetProperty("title")[0].GetString() ?? "Başlık Yok",
                JournalName = message.TryGetProperty("container-title", out var ct) && ct.GetArrayLength() > 0 ? ct[0].GetString() ?? "" : "Dergi Yok",
                PublicationYear = message.TryGetProperty("published-print", out var pp) ? pp.GetProperty("date-parts")[0][0].GetInt32() : DateTime.Now.Year,
                QCategory = "None"
            };
        }
        catch { return null; }
    }

    // KRİTİK DÜZELTME: Geri dönüş tipi Task<string> yapıldı
    public async Task<string> SaveWorkAsync(AcademicWork work)
    {
        try
        {
            var exists = await _context.AcademicWorks.AnyAsync(w => w.DOI == work.DOI);
            if (exists) return "Bu yayın zaten kayıtlı.";

            work.Id = 0; // Yeni kayıt
            work.JournalId = null;
            work.Journal = null;

            _context.AcademicWorks.Add(work);
            await _context.SaveChangesAsync();

            return "OK"; // Controller'daki if(success == "OK") kontrolü için
        }
        catch (Exception ex)
        {
            return $"Hata: {ex.Message}";
        }
    }
}