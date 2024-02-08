namespace Search.ElasticMigrator.Models;

public class IndexMigration
{
    public string IndexVersion { get; set; } = string.Empty;
    
    public string IndexName { get; set; } = string.Empty;

    public string NodeName { get; set; } = string.Empty;

    public List<string> Aliases { get; set; } = new();
}