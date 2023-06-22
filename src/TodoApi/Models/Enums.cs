namespace Todo.Api.Models;

public enum RecurrenceRangeType
{
    EndDate,
    NoEnd,
    Numbered,
}

public enum WeekIndex
{
    First,
    Second,
    Third,
    Fourth,
    Last,
}

public enum RecurrencePatternType
{
    Daily,
    Weekly,
    AbsoluteMonthly,
    RelativeMonthly,
    AbsoluteYearly,
    RelativeYearly
}

public enum WellknownListName
{
    None,
    DefaultList,
    FlaggedEmails,
    UnknownFutureValue,
}