using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineriesApp.DataContext.Models;

public class Municipality
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public virtual List<Winery> Wineries { get; set; } = new();
}