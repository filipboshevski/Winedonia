using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineriesApp.DataContext.Models
{
    public class Winery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public double Rating { get; set; }

        public string Website { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public virtual List<Wine> Wines { get; set; } = new ();

        public virtual Municipality? Municipality { get; set; }
    }
}
