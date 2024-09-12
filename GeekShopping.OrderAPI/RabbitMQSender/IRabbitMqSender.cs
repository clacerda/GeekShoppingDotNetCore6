using GeekShopping.MessageBus;

namespace GeekShopping.OrderApi.RabbitMQSender
{
    public interface IRabbitMqSender
    {
        void SendMessage(BaseMessage baseMessae, string queueName);
    }
}
