using System;

namespace ArchitectureTraining
{
    class Program
    {
        static void Main()
        {
            // Принцип инверсии зависимостей | "Dependency Inversion Principle"
            Delivery courDelivery = new Delivery(new CourierDelivery());
            courDelivery.DeliverGood();
            Delivery postDelivery = new Delivery(new PostDelivery());
            postDelivery.DeliverGood();

            // Паттерн "Декоратор" | "Decorator"
            DeliveryII deliveryAct = new CourDelivery();
            deliveryAct.Deliver();
            deliveryAct = new CourDeliveryOverMKAD(deliveryAct);
            deliveryAct.Deliver();

            // Паттерн "Фасад" | "Facade"
            Damage initialDamage = new Damage(100, 100, 0);
            DamageCounterFacade facade = new DamageCounterFacade(initialDamage, new PhysicalResistance(), new MagicalResistance(), new CriticalDamage());
            facade.CountDamage();           

            Console.Read();
        }
    }
}
