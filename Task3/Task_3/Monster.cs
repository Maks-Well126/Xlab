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
            int modifiedDamage = damage;

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

            int damageToArmor = 0;
            int damageToHealth = 0;

            if (Armor > 0)
            {
                if (modifiedDamage <= Armor)
                {
                    damageToArmor = modifiedDamage;
                    Armor -= modifiedDamage;
                    modifiedDamage = 0;
                }
                else
                {
                    damageToArmor = Armor; 
                    damageToHealth = modifiedDamage - Armor;
                    Armor = 0;
                    Health -= damageToHealth;
                }
            }
            else
            {
                damageToHealth = modifiedDamage; 
                Health -= damageToHealth;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{Type} \"{Name}\" получил урон!");
            if (damageToArmor > 0)
                Console.WriteLine($"Броня поглотила {damageToArmor} урона. Осталось брони: {Armor}");
            if (damageToHealth > 0)
                Console.WriteLine($"По здоровью нанесено {damageToHealth} урона. Осталось HP: {Health}");
            if (Armor == 0 && damageToArmor > 0)
                Console.WriteLine("Броня разрушена!");
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
            Console.WriteLine("1) +30 брони");
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
