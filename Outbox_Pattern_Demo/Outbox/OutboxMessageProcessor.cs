

namespace Outbox_Pattern_Demo
{
    public class OutboxMessageProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IKafkaProducer _producer;
        public OutboxMessageProcessor(IServiceScopeFactory scopeFactory, IKafkaProducer kafkaProducer){
            _scopeFactory = scopeFactory;
            _producer = kafkaProducer;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested){
                await PublishOutboxMessageAsync(stoppingToken);
            }
        }

        private async Task PublishOutboxMessageAsync(CancellationToken cancellationToken)
        {
            try{
                using var scope = _scopeFactory.CreateScope();
                await using var _dbContext = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
                List<OutboxMessage> messages = _dbContext.OutboxMessages.Where(om=>om.IsMessageDispatched!=false).ToList();
                foreach(OutboxMessage outboxMessage in messages){
                    try{

                    }catch(Exception e){
                        Console.WriteLine(e);
                    }
                }
            }catch{
                throw;
            }
            await Task.Delay(5000, cancellationToken);
        }
    }
}