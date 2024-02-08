namespace WineriesApp.Common.Filters
{
    public class PhoneFormatFilter : IFilter<string>
    {
        public string Execute(string input)
        {
            var fields = input.Split(',');
            var phone = fields[4].Substring(1, fields[4].Length-1);

            phone = $"+389 {phone}";
            fields[4] = phone;

            return string.Join(",", fields);
        }
    }
}
