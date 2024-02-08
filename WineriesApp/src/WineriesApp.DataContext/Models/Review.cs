using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineriesApp.DataContext.Enums;

namespace WineriesApp.DataContext.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public ReviewEntityType Type { get; set; }

        public double Rating { get; set; }

        public string? Comment { get; set; }
        
        public DateTime Date { get; set; }

        public virtual Wine? Wine { get; set; }

        public virtual Winery? Winery { get; set; }
    }
}
