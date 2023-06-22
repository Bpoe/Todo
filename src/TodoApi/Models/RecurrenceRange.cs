namespace Todo.Api.Models;

public class RecurrenceRange
{
    public DateTime EndDate { get; set; }

    public int NumberOfOccurrences { get; set; }

    public string RecurrenceTimeZone { get; set; }

    public DateTime StartDate { get; set; }

    public RecurrenceRangeType Type { get; set; }
}
