using System;


namespace ArchitectureTraining
{
    // В данном примере происходит подсчет урона в три этапа: просчитывание шанса критического удара, физическое сопротивление и магическое сопротивление, чистый урон не блокируется.
    // Для магического и физического сопротивления используется идентичная формула с одним отличием - коэффициентом сопротивления. 
    // Поэтому я выделил абстрактный класс Resistance с виртуальным методом GetDamageAfterResist, который при желании можно будет переопределить.

    abstract class Resistance
    {

        private double BasicResistance { get; set; }
        public Resistance(double basicResistance)
        {
            BasicResistance = basicResistance; // Устанавливаем коэффициент сопротивления
        }
        public virtual double GetDamageAfterResist(double initialDamage)
        {
            return initialDamage - initialDamage * BasicResistance; // Простая формула сопротивления
        }
    }
    class PhysicalResistance : Resistance
    {
        public PhysicalResistance() : base(0.25) { } // Коэффициент устанавливается через base(n), формула не переопределяется

    }

    class MagicalResistance : Resistance
    {
        public MagicalResistance() : base(0.18) { }  // Коэффициент устанавливается через base(n), формула не переопределяется
    }

    class CriticalDamage
    {
        public double CountCritChance(double damage)
        {
            Random rand = new Random();
            return rand.Next(0, 10) <= 3 ? damage * 1.3 : damage; // С определенным шансом выпадет крит размером в 130%   
        }
    }

    // Изначальный урон (initialDamage) будет приходить в "запакованном" виде с тремя различными видами урона.
    class Damage
    {
        public double PhysicalDamage { get; set; }
        public double MagicalDamage { get; set; }
        public double PureDamage { get; set; }
        public Damage(double physicalDamage, double magicalDamage, double pureDamage)
        {
            this.PhysicalDamage = physicalDamage;
            this.MagicalDamage = magicalDamage;
            this.PureDamage = pureDamage;
        }
    }

    class DamageCounterFacade
    {
        //Создаем необходимые экземпляры классов для подсчета урона
        readonly PhysicalResistance pr = new PhysicalResistance(); 
        readonly MagicalResistance mr = new MagicalResistance();
        readonly CriticalDamage cd = new CriticalDamage();
        readonly Damage initDmg; // Изначальный урон
        double totalDmg = 0; // По мере подсчета будем наполнять переменную "обработанными" единицами урона
        public DamageCounterFacade(Damage dmg)
        {
            initDmg = dmg;
        }
        public void CountDamage() // Реализация "фасада"
        {
            totalDmg += pr.GetDamageAfterResist(cd.CountCritChance(initDmg.PhysicalDamage)); // Сначала считается крит, затем сопротивление 
            totalDmg += mr.GetDamageAfterResist(initDmg.MagicalDamage); // Маг. сопротивление
            totalDmg += initDmg.PureDamage; // Чистый не режется
            Console.WriteLine($"Изначально было направлено {initDmg.PureDamage} чистого, {initDmg.PhysicalDamage} физического и {initDmg.MagicalDamage} магического урона.");
            Console.WriteLine($"Получено в итоге: {totalDmg} урона.");
        }
    }
}
