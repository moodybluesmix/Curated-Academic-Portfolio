using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicPortfolio.Shared.Entities
{
    public class Journal
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ISSN { get; set; }
        public string? Publisher { get; set; }
        // Bir derginin birden fazla yayını olabilir
        public ICollection<AcademicWork> Works { get; set; } = new List<AcademicWork>();
    }
}
