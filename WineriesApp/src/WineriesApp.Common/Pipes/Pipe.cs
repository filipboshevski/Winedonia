using WineriesApp.Common.Filters;

namespace WineriesApp.Common.Pipes
{
    public class Pipe<T>
    {
        private List<IFilter<T>> filters;

        public Pipe() {
            filters = new List<IFilter<T>>();
        }

        public void AddFilter(IFilter<T> filter)
        {
            filters.Add(filter);
        }

        public T RunFilters(T input)
        {
            foreach (var filter in filters)
            {
                input = filter.Execute(input);
            }
            return input;
        }
    }
}
