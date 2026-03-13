namespace AcademicPortfolio.Shared.Entities;

public class AcademicWork
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DOI { get; set; } = string.Empty;
    public string JournalName { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public string PublicationType { get; set; } = "Article";

    // --- TÜBİTAK Dosyandaki Kritik Alanlar ---
    public string QCategory { get; set; } = "None";
    public int CitationCount { get; set; }
    public bool IsProjectOutput { get; set; }
    public string? ProjectCode { get; set; } // Hata buradaydı, bu satır şart.
    public string? AuthorRole { get; set; }

    public int? JournalId { get; set; }
    public Journal? Journal { get; set; }
}
