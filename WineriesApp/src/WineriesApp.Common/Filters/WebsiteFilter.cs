namespace WineriesApp.Common.Filters
{
    public class WebsiteFilter : IFilter<string>
    {
        public string Execute(string input)
        {
            var fields = input.Split(',');
            var coordinates = $"{fields[1]}%2C{fields[2]}";

            var website = fields[6] != "null" ? fields[6] : $"https://www.google.com/maps/place/{coordinates}";
            fields[6] = website;

            return string.Join(",", fields);
        }
    }
}
