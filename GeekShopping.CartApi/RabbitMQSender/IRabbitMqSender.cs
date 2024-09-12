using GeekShopping.MessageBus;

namespace GeekShopping.CartApi.RabbitMQSender
{
    public interface IRabbitMqSender
    {
        void SendMessage(BaseMessage baseMessae, string queueName);
    }
}
