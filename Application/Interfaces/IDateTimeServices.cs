namespace Application.Interfaces
{
    public interface IDateTimeServices
    {
        DateTime NowUTC { get; }
        DateTime NowPeru { get; }
    }
}
