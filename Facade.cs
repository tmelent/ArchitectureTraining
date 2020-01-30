using System;


namespace ArchitectureTraining
{
    // В данном примере происходит подсчет урона, как в некоторых играх, в три этапа: 
    // просчитывание шанса критического удара, физическое сопротивление и магическое сопротивление, чистый урон не блокируется.
    // Для магического и физического сопротивления используется идентичная формула с одним отличием - коэффициентом сопротивления. 
    // Поэтому я выделил абстрактный класс Resistance с виртуальным методом GetDamageAfterResist, который при желании можно будет переопределить.

    // In this example, damage is calculated in three stages, as in some games: 
    // calculating critical strike chance, physical resistance and magic resistance, pure damage is not blocked.
    // For magic and physical resistance, an identical formula is used with one difference - the resistance coefficient. 
    // So I have allocated an abstract Resistance class with a virtual GetDamageAfterResist method that can be overrided if desired.

    abstract class Resistance
    {
        private double BasicResistance { get; set; }
        public Resistance(double basicResistance)
        {
            BasicResistance = basicResistance; // Устанавливаем коэффициент сопротивления | Setting up an basicResistance multiplier.
        }
        public virtual double GetDamageAfterResist(double initialDamage)
        {
            return initialDamage - initialDamage * BasicResistance; // Простая формула сопротивления | A simple resistance formula.
        }
    }
    class PhysicalResistance : Resistance
    {
        public PhysicalResistance() : base(0.25) { } // Коэффициент устанавливается через base(n) | A multiplier is setting up via base(n).
    }

    class MagicalResistance : Resistance
    {
        public MagicalResistance() : base(0.18) { }  // Коэффициент устанавливается через base(n) | A multiplier is setting up via base(n).
    }

    class CriticalDamage
    {
        public double CountCritChance(double damage)
        {
            Random rand = new Random();
            return rand.Next(0, 10) <= 3 ? damage * 1.3 : damage; // С определенным шансом выпадет крит размером в 130% | With a certain chance it will return 130% damage
        }
    }

    // Изначальный урон (initialDamage) будет приходить в "запакованном" виде с тремя различными видами урона.
    // Initial Damage will come in "packed" form with three different types of damage in it.
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
        PhysicalResistance pr; 
        MagicalResistance mr;
        CriticalDamage cd;
        readonly Damage initDmg; // Изначальный урон | Initial Damage
        double totalDmg = 0; // Все считаем в эту переменную | This variable will contain everything we count 
        public DamageCounterFacade(Damage dmg, PhysicalResistance _pr, MagicalResistance _mr, CriticalDamage _cd)
        {
            pr = _pr;
            mr = _mr;
            cd = _cd;
            initDmg = dmg;
        }
        public void CountDamage() // Реализация "фасада" | "Facade" implementation
        {
            totalDmg += pr.GetDamageAfterResist(cd.CountCritChance(initDmg.PhysicalDamage)); // Сначала считается крит, затем сопротивление | We count crit first, then ph. resistance
            totalDmg += mr.GetDamageAfterResist(initDmg.MagicalDamage); // Маг. сопротивление | Magical resistance
            totalDmg += initDmg.PureDamage; // Чистый не режется | Pure damage goes through resistance
            Console.WriteLine($"Изначально было направлено {initDmg.PureDamage} чистого, {initDmg.PhysicalDamage} физического и {initDmg.MagicalDamage} магического урона.");
            Console.WriteLine($"Получено в итоге: {totalDmg} урона.");
        }
    }
}
