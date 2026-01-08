using Backend.Models;

namespace Backend.DTOs.Importer;

public class FKDataDTOs
{
    public required IEnumerable<YearPeriods> YearPeriodFK { get; set; }
    public required IEnumerable<Paragraphs> ParagraphFK { get; set; }
    public required IEnumerable<SubCategories> subCategoriesFK { get; set; }
    public required IEnumerable<Questions> questionsCache { get; set; }
}