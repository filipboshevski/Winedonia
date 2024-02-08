using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WineriesApp.DataContext.Enums;

namespace WineriesApp.DataContext.Models
{
    public class Wine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
    
        public WineType Type { get; set; }
    
        public double Rating { get; set; }

        public string PreviewImageUrl { get; set; } = string.Empty;

        public string MainImageUrl { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public virtual List<Winery> Wineries { get; set; } = new List<Winery>();
    }
}