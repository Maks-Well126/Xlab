#nullable disable

namespace ValheimGame
{
   

    public abstract class Monster
    {
        public string Name { get; set; }
        public string Type { get; protected set; }
        public int Health { get; protected set; }
        public bool IsFlying { get; protected set; }
        public bool IsInvisible { get; protected set; }
        public int Armor { get; protected set; }
        public bool IsAlive => Health > 0;

        protected Monster(string name, string type, int health, bool isFlying)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Безымянный" : name;
            Type = type;
            Health = health;
            IsFlying = isFlying;
        }

        public abstract string Move();
        public virtual double GetDamageMultiplier(WeaponType weapon) => 1.0;

        public void TakeDamage(int damage, WeaponType weapon)
        {
            double multiplier = GetDamageMultiplier(weapon);
            int modifiedDamage = (int)(damage * multiplier) - Armor;
            if (modifiedDamage < 0) modifiedDamage = 0;

            if (IsInvisible)
            {
                Random rnd = new Random();
                if (rnd.Next(0, 100) < 15)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{Name} ({Type}) уклонился благодаря невидимости!");
                    Console.ResetColor();
                    return;
                }
            }

            Health -= modifiedDamage;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Name} ({Type}) получил {modifiedDamage} урона! HP: {Health}");
            Console.ResetColor();

            if (Health <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{Name} ({Type}) погиб!");
                Console.ResetColor();
            }
        }

        public void Heal(int amount)
        {
            Health += amount;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} ({Type}) восстановил {amount} HP! Теперь у него {Health} HP.\n");
            Console.ResetColor();
        }

        public void UpgradeMenu()
        {
            Console.WriteLine("\nВыберите улучшение:");
            Console.WriteLine("1) +10 брони");
            Console.WriteLine("2) Включить невидимость");
            Console.WriteLine("3) +50 HP");
            Console.WriteLine("4) Всё сразу (суперапгрейд)");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": Armor += 30; break;
                case "2": IsInvisible = true; break;
                case "3": Heal(50); break;
                case "4": Armor += 30; IsInvisible = true; Heal(50); break;
                default: Console.WriteLine("Некорректный выбор!"); break;
            }
        }

        public override string ToString()
        {
            string armorText = Armor > 0 ? $"Броня: {Armor}" : "Без брони";
            string visibility = IsInvisible ? "Невидим" : "Видим";
            return $"{Type} \"{Name}\" | HP: {Health} | {armorText} | {visibility}";
        }
    }
}
