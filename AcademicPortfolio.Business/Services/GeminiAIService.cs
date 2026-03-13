using System.Net.Http.Json;
using System.Text.Json;
using AcademicPortfolio.Shared.Entities;

namespace AcademicPortfolio.Business.Services;

public class GeminiAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = ""; // Çalışma ortamı tarafından otomatik sağlanır

    public GeminiAIService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> AskAboutPortfolioAsync(List<AcademicWork> works, string userQuery)
    {
        try
        {
            // 1. Portföy verilerini AI için anlamlı bir metne dönüştürelim (Context)
            var context = string.Join("\n", works.Select(w =>
                $"- Başlık: {w.Title}, Yıl: {w.PublicationYear}, Dergi: {w.JournalName}, Kategori: {w.QCategory}, Proje: {w.ProjectCode ?? "Yok"}"));

            var systemPrompt = $@"Sen bir akademik portföy asistanısın. Kullanıcının akademik yayınları aşağıdadır:
            {context}
            
            Kullanıcının sorularına bu verilere dayanarak kısa, profesyonel ve yardımcı bir dilde yanıt ver. 
            Eğer kullanıcı belirli bir yayını sorarsa detaylarını açıkla. Eğer istatistik sorarsa (kaç tane Q1 var gibi) hesapla.";

            // 2. Gemini API İsteği (Gereksinimlere uygun format)
            var payload = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = userQuery } } }
                },
                systemInstruction = new { parts = new[] { new { text = systemPrompt } } }
            };

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-preview-09-2025:generateContent?key={_apiKey}";

            var response = await _httpClient.PostAsJsonAsync(url, payload);

            if (!response.IsSuccessStatusCode)
                return "AI servisine şu an ulaşılamıyor. Lütfen API ayarlarını kontrol edin.";

            var result = await response.Content.ReadFromJsonAsync<JsonElement>();
            return result.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString() ?? "AI yanıt üretemedi.";
        }
        catch (Exception ex)
        {
            return $"AI Analiz Hatası: {ex.Message}";
        }
    }
}