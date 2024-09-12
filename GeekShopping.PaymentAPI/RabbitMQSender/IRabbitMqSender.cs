using GeekShopping.MessageBus;

namespace GeekShopping.PaymentAPI.RabbitMQSender
{
    public interface IRabbitMqSender
    {
        void SendMessage(BaseMessage baseMessae);
    }
}
