using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectureTraining
{

    // Реализация паттерна Decorator
    abstract class DeliveryII
    {
        public string Name { get; protected set; } // Название способа доставки
        public DeliveryII(string name)
        {
            this.Name = name;
        }
        public virtual void Deliver()
        {
            Console.WriteLine($"Выбрана {this.Name}. Стоимость доставки: {this.GetCost()}");
        }
        public abstract int GetCost(); // Подсчет стоимости
    }

    class CourDelivery : DeliveryII
    {
        public CourDelivery() : base("Курьерская доставка") { } // Задается название способа доставки        
        public override int GetCost()
        {
            return 300;
        }
    }

    // Декоратор
    abstract class DeliveryDecorator : DeliveryII
    {
        protected DeliveryII delivery;
        public DeliveryDecorator(string name, DeliveryII delivery) : base(name)
        {
            this.delivery = delivery;
        }
    }

    // Доставка за МКАД
    class CourDeliveryOverMKAD : DeliveryDecorator
    {
        public CourDeliveryOverMKAD(DeliveryII delivery) : base(delivery.Name + " за МКАД", delivery) { }
        public override int GetCost()
        {
            return delivery.GetCost() + 100; // Стоиомсть доставки за МКАД выше, поэтому возвращается иная сумма
        }

    }
}
