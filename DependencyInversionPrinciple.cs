using System;

namespace ArchitectureTraining
{
    public interface IDeliveryMethod
    {
        void Deliver(string text, decimal price); // Доставка 
    }

    class Delivery
    {
        public string Text { get; set; }
        public decimal Price { get; set; }
        public IDeliveryMethod Deliverer { get; set; } // Способ, с помощью которого будет осуществляться доставка товара. | Delivery method
        public Delivery(IDeliveryMethod deliveryMethod)
        {
            this.Deliverer = deliveryMethod;
        }

        public void DeliverGood()
        {
            Deliverer.Deliver(Text, Price); // Вызывается метод доставки у способа, заданного в конструкторе | Calling an base Deliver() implementation
        }
    }

    class CourierDelivery : IDeliveryMethod
    {
        public void Deliver(string text, decimal price) // Реализация курьерской доставки | Courier delivery
        {
            Console.WriteLine("Товар будет доставлен курьером.");
            Console.WriteLine($"Доставка будет стоить 300 руб.");
        }
    }

    class PostDelivery : IDeliveryMethod
    {
        public void Deliver(string text, decimal price) // Реализация почтовой доставки | Post Delivery
        {
            Console.WriteLine("Товар будет отправлен почтой.");
            Console.WriteLine($"Доставка будет стоить 150 руб.");
        }
    }
}
