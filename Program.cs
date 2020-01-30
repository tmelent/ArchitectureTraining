﻿using System;

namespace ArchitectureTraining
{
    class Program
    {
        static void Main()
        {
            // Инверсия зависимостей
            Delivery courDelivery = new Delivery(new CourierDelivery());
            courDelivery.DeliverGood();
            Delivery postDelivery = new Delivery(new PostDelivery());
            postDelivery.DeliverGood();

            // Паттерн "Декоратор"
            DeliveryII deliveryAct = new CourDelivery();
            deliveryAct.Deliver();
            deliveryAct = new CourDeliveryOverMKAD(deliveryAct);
            deliveryAct.Deliver();
            Console.Read();
        }
    }
}