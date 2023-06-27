namespace TodoApi.Models;
using Models;

public static class Merging
{
    public static void ApplyTo(this TodoTask delta, TodoTask original)
    {
        if (delta.Title != null)
        {
            original.Title = delta.Title;
        }
    }
}

