using System.Text.Json;

public class OrderService:IOrderService{
    private readonly CustomerDbContext customerDbContext;

    public OrderService(CustomerDbContext customerDbContext){
        this.customerDbContext = customerDbContext;
    }
    public async Task<List<Order>> GetAllOrdersAsync(){
        return await Task.FromResult(customerDbContext.Orders.ToList<Order>());
    }
    public async Task<Order> GetOrderAsync(int id){
        return await Task.FromResult(customerDbContext.Orders.FirstOrDefault(x=> x.Order_Id == id));    
    }
    public async Task CreateAllOrderAsync(Order order){
        using var transaction = customerDbContext.Database.BeginTransaction();
        try{
            customerDbContext.AddOrder(order);
            await customerDbContext.SaveChangesAsync();
            var OutboxMessage = new OutboxMessage{Event_Payload = JsonSerializer.Serialize(order),
                                                 Event_Time = DateTime.Now,
                                                 IsMessageDispatched = false  
                                                    }
        }
    }
}