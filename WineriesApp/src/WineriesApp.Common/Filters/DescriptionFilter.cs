namespace WineriesApp.Common.Filters
{
    public class DescriptionFilter : IFilter<string>
    {
        private readonly string separator;
        private readonly int index;

        public DescriptionFilter(string separator, int index)
        {
            this.separator = separator;
            this.index = index;
        }
    
        public string Execute(string input)
        {
            var fields = input.Split(separator);

            fields[index] = fields[index].Replace("\"", "");
        
            return string.Join(separator, fields);
        }
    }
}