namespace WineriesApp.Common.Filters
{
    public class CoordinatesFilter : IFilter<string>
    {
        public string Execute(string input)
        {
            var fields = input.Split(',');

            if (!float.TryParse(fields[1], out _) || !float.TryParse(fields[2], out _))
            {
                throw new Exception("Not valid format");
            }

            fields[1] = $"{fields[1].Split(".")[0]}.{fields[1].Split(".")[1].Substring(0,5)}";
            fields[2] = $"{fields[2].Split(".")[0]}.{fields[2].Split(".")[1].Substring(0, 5)}";

            return string.Join(",", fields);

        }
    }
}
