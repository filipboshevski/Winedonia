namespace Search.Common.Interfaces;

public interface IDocument
{
    Guid Id { get; set; }
    
    string Name { get; set; }
}