public interface IKafkaProducer{
    Task SendMessageToKafkaAsync(OutboxMessage message);
}