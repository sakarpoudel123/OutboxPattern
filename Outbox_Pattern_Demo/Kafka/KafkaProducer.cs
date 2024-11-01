using System.Net;
using Confluent.Kafka;

public class KafkaProducer: IKafkaProducer{

    private readonly IOutboxMessageRepository _outboxRespository;
    private readonly ProducerConfig _producerConfig;
    private readonly string topic = "test";
    public KafkaProducer(IOutboxMessageRepository outboxRepository){
        _outboxRespository = outboxRepository;
        _producerConfig = new ProducerConfig{
                                                BootstrapServers = "localhost:9092",
                                                ClientId = Dns.GetHostName()
                                            };

    }

    public async Task SendMessageToKafkaAsync(OutboxMessage message)
    {
        if(message == null){
            throw new ArgumentNullException(nameof(message));
        }
        using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();
        try{
            var result = await producer.ProduceAsync(topic, new Message<Null, string> {Value = message.Event_Payload});
            if(result.Status ==PersistenceStatus.Persisted){
                await _outboxRespository.UpdateAsync(message, OutboxMessageStatus.Sent);
            }
        }catch(Exception){
            await _outboxRespository.UpdateAsync(message, OutboxMessageStatus.New);
        }
    }
} 