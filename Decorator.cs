using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectureTraining
{

    // Реализация паттерна Decorator 
    // Decorator implementation
    abstract class DeliveryII
    {
        public string Name { get; protected set; } // Название способа доставки | Title of the delivery method
        public DeliveryII(string name)
        {
            this.Name = name;
        }
        public virtual void Deliver()
        {
            Console.WriteLine($"Выбрана {this.Name}. Стоимость доставки: {this.GetCost()}");
        }
        public abstract int GetCost(); // Подсчет стоимости | Getting delivery cost
    }

    class CourDelivery : DeliveryII
    {
        public CourDelivery() : base("Курьерская доставка") { } // Задается название способа доставки | Setting up title of the delivery method  
        public override int GetCost()
        {
            return 300;
        }
    }

    // Декоратор | Decorator
    abstract class DeliveryDecorator : DeliveryII
    {
        protected DeliveryII delivery;
        public DeliveryDecorator(string name, DeliveryII delivery) : base(name)
        {
            this.delivery = delivery;
        }
    }

    // Доставка за МКАД | Courier delivery over MKAD
    class CourDeliveryOverMKAD : DeliveryDecorator
    {
        public CourDeliveryOverMKAD(DeliveryII delivery) : base(delivery.Name + " за МКАД", delivery) { }
        public override int GetCost()
        {
            return delivery.GetCost() + 100; // Стоимость доставки за МКАД выше, поэтому возвращается иная сумма | Because of the distance, the return cost is higher
        }

    }
}
