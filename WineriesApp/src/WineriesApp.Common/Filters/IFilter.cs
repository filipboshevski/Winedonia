namespace WineriesApp.Common.Filters
{
    public interface IFilter<T>
    {
        T Execute(T input);
    }
}

/*
 
Filters ToDo:
- Website Filter --- DONE ---
- Coordinates Filter  --- DONE ---
- PhoneFormat Filter --- DONE ---

 */