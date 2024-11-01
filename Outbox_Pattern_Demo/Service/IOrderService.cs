public interface IOrderService{
    public Task<List<Order>> GetAllOrdersAsync();
    public Task<Order> GetOrderAsync(int id);
    public Task CreateAllOrderAsync(Order order);

}