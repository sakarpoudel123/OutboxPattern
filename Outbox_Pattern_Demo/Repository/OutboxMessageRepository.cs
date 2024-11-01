
using System.Collections.ObjectModel;

namespace Outbox_Pattern_Demo
{
    public class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly CustomerDbContext _context;
        private readonly IReadOnlyCollection<OutboxMessage> _outboxMessages;
        public OutboxMessageRepository(CustomerDbContext context){
            _context = context;
        }
        public async Task<IReadOnlyCollection<OutboxMessage>> GetMessagesByIdsAsync(IEnumerable<int> ids)
        {
            List<OutboxMessage>? orders = _context.OutboxMessages.ToList();
            var readOnlyOrders = new ReadOnlyCollection <OutboxMessage>(orders);
            return readOnlyOrders;
        }

        public async Task<IReadOnlyCollection<OutboxMessage>> GetUnsentMessagesAsync()
        {
            List<OutboxMessage>? unsentMessage = _context.OutboxMessages.Where(e=> e.IsMessageDispatched!=true).ToList();
            ReadOnlyCollection<OutboxMessage>? result = new ReadOnlyCollection<OutboxMessage>(unsentMessage);
            return result;
        }

        public async Task UpdateAsync(OutboxMessage message, bool status)
        {
            var entity = _context.OutboxMessages.FirstOrDefault(o=>o.Event_Id==message.Event_Id);
            if(entity!=null){
                entity.Event_Id = message.Event_Id;
                entity.Event_Time = message.Event_Time;
                entity.Event_Payload = message.Event_Payload;
                entity.IsMessageDispatched = message.IsMessageDispatched;
                await _context.SaveChangesAsync();
            }   
        }
    }
}