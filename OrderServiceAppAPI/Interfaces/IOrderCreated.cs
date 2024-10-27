namespace OrderServiceAppAPI.Interfaces
{
    public interface IOrderCreated
    {
        int Id { get; }
        DateTime OrderDate { get; }
    }
}