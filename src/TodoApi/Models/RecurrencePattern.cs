namespace Todo.Api.Models;

public class RecurrencePattern
{
    public int DayOfMonth { get; set; }

    public IList<DayOfWeek> DaysOfWeek { get; set; } = new List<DayOfWeek>();

    public DayOfWeek FirstDayOfWeek { get; set; }

    public WeekIndex? Index { get; set; }

    public int Interval { get; set; }

    public int Month { get; set; }

    public RecurrencePatternType Type { get; set; }
}
