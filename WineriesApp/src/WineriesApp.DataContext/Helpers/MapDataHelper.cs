using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineriesApp.Common.Filters;
using WineriesApp.Common.Pipes;
using WineriesApp.DataContext.Enums;
using WineriesApp.DataContext.Models;

namespace WineriesApp.DataContext.Helpers
{
    public static class MapDataHelper
    {
        public static IEnumerable<Winery> GetWineries(WineriesDbContext context)
        {
            var wineries = new List<Winery>();
            var pipe = new Pipe<string>();
            var municipalities = context.Municipalities.ToList();

            pipe.AddFilter(new WebsiteFilter());
            pipe.AddFilter(new CoordinatesFilter());
            pipe.AddFilter(new PhoneFormatFilter());
            pipe.AddFilter(new DescriptionFilter(",", 9));

            using (var reader = new StreamReader("Resources/data_wineries.csv"))
            {
                reader.ReadLine(); // Skip Headers
                
                var line = reader.ReadLine();

                while (line != null)
                {
                    line = pipe.RunFilters(line ?? string.Empty);

                    var fields = line.Split(',');
                    fields = fields.Select(f => f.Replace("$", ",")).ToArray();

                    var winery = new Winery
                    {
                        Name = fields[0]
                    };

                    if (float.TryParse(fields[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var latitude))
                    {
                        winery.Latitude = latitude;
                    }

                    if (float.TryParse(fields[2], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var longitude))
                    {
                        winery.Longitude = longitude;
                    }

                    winery.Address = fields[3];
                    winery.PhoneNumber = fields[4];
                    
                    if (double.TryParse(fields[5], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var rating))
                    {
                        winery.Rating = rating;
                    }

                    winery.Website = fields[6];
                    winery.ImageUrl = fields[8];
                    winery.Municipality = municipalities.FirstOrDefault(m => m.Name == fields[7]);
                    winery.Description = fields[9];

                    wineries.Add(winery);

                    line = reader.ReadLine();
                }
            }

            return wineries;
        }

        public static IEnumerable<Wine> GetWines()
        {
            var wines = new List<Wine>();
            var pipe = new Pipe<string>();

            pipe.AddFilter(new DescriptionFilter(",", 5));

            using (var reader = new StreamReader("Resources/data_wines.csv"))
            {
                reader.ReadLine(); // Skip Headers

                var line = reader.ReadLine();

                while (line != null)
                {
                    line = pipe.RunFilters(line ?? string.Empty);

                    var fields = line.Split(',');
                    fields = fields.Select(f => f.Replace("$", ",")).ToArray();
                    
                    var wine = new Wine
                    {
                        Name = fields[0],
                        Type = (WineType)Convert.ToInt32(fields[1])
                    };
                    
                    if (double.TryParse(fields[2], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var rating))
                    {
                        wine.Rating = rating;
                    }

                    wine.PreviewImageUrl = fields[3];
                    wine.MainImageUrl = fields[4];
                    wine.Description = fields[5];
                    
                    wines.Add(wine);

                    line = reader.ReadLine();
                }
            }

            return wines;
        }

        public static Dictionary<string, List<string>> GetWinesWineries()
        {
            var result = new Dictionary<string, List<string>>();

            using (var reader = new StreamReader("Resources/data_wines_wineries.csv"))
            {
                reader.ReadLine(); // Skip Headers

                var line = reader.ReadLine();

                while (line != null)
                {
                    var fields = line.Split(",");

                    if (!result.ContainsKey(fields[0]))
                    {
                        result[fields[0]] = new List<string> { fields[1] };
                        continue;
                    }

                    if (!result[fields[0]].Any(x => x == fields[1]))
                    {
                        result[fields[0]].Add(fields[1]);
                    }

                    line = reader.ReadLine();
                }
            }

            return result;
        }

        public static List<Municipality> GetMunicipalities()
        {
            var result = new List<Municipality>();
            
            using (var reader = new StreamReader("Resources/data_municipalities.csv"))
            {
                reader.ReadLine(); // Skip Headers

                var line = reader.ReadLine();

                while (line != null)
                {
                    result.Add(new Municipality() { Name = line });
                    line = reader.ReadLine();
                }
            }

            return result;
        }
    }
}
