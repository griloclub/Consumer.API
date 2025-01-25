namespace Consumer.Domain.Enums;

public enum TableStatus
{
    Closed = 0,
    Open = 1,
    Reserved = 2,
    InService = 3,
    AwaitingPayment = 4,
    Cleaning = 5
}