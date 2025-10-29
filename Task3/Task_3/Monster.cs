using System;

namespace ValheimBattle
{
    abstract class Monster
    {
        public string Name { get; set; }
        public string Type { get; protected set; }
        public int Health { get; protected set; }
        public bool HasArmor { get; set; }
        public bool IsInvisible { get; set; }

        public bool IsAlive => Health > 0;

        protected Monster(string name, string type, int health)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Безымянный" : name;
            Type = type;
            Health = health;
        }

        public abstract string Move();

        public virtual void TakeDamage(int damage)
        {
            int originalDamage = damage;

            if (HasArmor)
            {
                damage = (int)(damage * 0.7);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Name} ({Type}) одет в броню! Урон снижен на 30%.");
            }

            if (IsInvisible)
            {
                Random rand = new Random();
                if (rand.Next(0, 100) < 15)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{Name} ({Type}) использует невидимость и избегает урона!");
                    Console.ResetColor();
                    return;
                }
            }

            Health -= damage;
            if (Health < 0) Health = 0;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Name} ({Type}) получил {damage} урона (изначально {originalDamage}).");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Осталось {Health} HP.\n");
            Console.ResetColor();

            if (!IsAlive)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{Name} ({Type}) погиб!\n");
                Console.ResetColor();
            }
        }

        public override string ToString()
        {
            return $"{Type} \"{Name}\" | HP: {Health} | {(HasArmor ? "Броня" : "Нет брони")} | {(IsInvisible ? "Невидимость" : "Видимый")}";
        }

        public void Heal(int amount)
        {
            Health += amount;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} ({Type}) восстановил {amount} HP! Теперь у него {Health} HP.\n");
            Console.ResetColor();
        }
    }
}
