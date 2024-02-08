namespace WineriesApp.Services.Models;

public class Document
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public double Rating { get; set; }
    
    public string? Location { get; set; }
}