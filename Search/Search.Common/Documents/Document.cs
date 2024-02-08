using Search.Common.Interfaces;

namespace Search.Common.Documents;

public class Document
    : IDocument
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public double Rating { get; set; }
    
    public string? Location { get; set; }
}