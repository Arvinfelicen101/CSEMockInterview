using Backend.Models;

namespace Backend.DTOs.Importer;

public record MappedData(
    List<Category> Categories,
    List<Paragraphs> Paragraphs,
    List<ItemChoices> Choices
);