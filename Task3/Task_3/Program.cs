#nullable disable
using ValheimGame; 

namespace ConsoleValheim
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Добро пожаловать в Вальхейм");
            Console.ResetColor();

            List<Monster> monsters = new List<Monster>();
            string input;

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n===== МЕНЮ =====");
                Console.ResetColor();
                Console.WriteLine("1) Добавить Драугра");
                Console.WriteLine("2) Добавить Грейдварфа");
                Console.WriteLine("3) Добавить Смертокрыла");
                Console.WriteLine("4) Добавить Искателя");
                Console.WriteLine("5) Нанести урон монстру по ID");
                Console.WriteLine("6) Нанести урон случайному монстру");
                Console.WriteLine("7) Улучшить монстра");
                Console.WriteLine("8) Удалить монстра");
                Console.WriteLine("9) Показать всех монстров");
                Console.WriteLine("Введите 'q' для выхода");
                Console.Write("\nВыбор: ");

                input = Console.ReadLine()?.Trim().ToLower();


                switch (input)
                {
                    case "1":
                        AddMonster(monsters, "Draugr");
                        break;
                    case "2":
                        AddMonster(monsters, "Greydwarf");
                        break;
                    case "3":
                        AddMonster(monsters, "Deathsquito");
                        break;
                    case "4":
                        AddMonster(monsters, "Seeker");
                        break;    
                    case "5":
                        AttackById(monsters);
                        break;
                    case "6":
                        AttackRandom(monsters);
                        break;
                    case "7":
                        UpgradeMonster(monsters);
                        break;
                    case "8":
                        RemoveMonster(monsters);
                        break;
                    case "9":
                        ShowMonsters(monsters);
                        break;
                    case "q":
                        Console.WriteLine("\nВыход из игры...");
                        break;
                    default:
                        Console.WriteLine("Неверный ввод!");
                        break;
                }
            }
            while (input is not "q");
        }


        static WeaponType ChooseWeapon()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nВыберите оружие:");
            Console.ResetColor();
            Console.WriteLine("1) Меч");
            Console.WriteLine("2) Топор");
            Console.WriteLine("3) Лук");
            Console.WriteLine("4) Огненный шар");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();

            return choice switch
            {
                "1" => WeaponType.Sword,
                "2" => WeaponType.Axe,
                "3" => WeaponType.Bow,
                "4" => WeaponType.Fireball,
                _ => WeaponType.Sword
            };
        }

        static WeaponType GetRandomWeapon()
        {
            Random rand = new Random();
            return (WeaponType)rand.Next(0, Enum.GetValues(typeof(WeaponType)).Length);
        }

        static void AddMonster(List<Monster> monsters, string type)
        {
            Console.Write("Введите имя монстра (Enter оставить пустым): ");
            string nameInput = Console.ReadLine();
            string name = string.IsNullOrWhiteSpace(nameInput) ? "Безымянный" : nameInput;

            Monster m = type switch
            {
                "Draugr" => new Draugr(name),
                "Greydwarf" => new Greydwarf(name),
                "Deathsquito" => new Deathsquito(name),
                "Seeker" => new Seeker(name),
                _ => null
            };

            if (m != null)
            {
                monsters.Add(m);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nДобавлен новый монстр: {m.Type} \"{m.Name}\" ({m.Move()})");
                Console.ResetColor();
            }
        }

        static void AttackById(List<Monster> monsters)
        {
            if (monsters.Count == 0)
            {
                Console.WriteLine("Нет монстров для атаки!");
                return;
            }

            ShowMonsters(monsters);
            Console.Write("Введите ID монстра для атаки: ");
            if (int.TryParse(Console.ReadLine(), out int id) && id >= 0 && id < monsters.Count)
            {
                WeaponType weapon = ChooseWeapon();
                int baseDamage = Weapons.GetBaseDamage(weapon);

                var target = monsters[id];
                double multiplier = target.GetDamageMultiplier(weapon);
                int finalDamage = (int)(baseDamage * multiplier);

                Console.ForegroundColor = ConsoleColor.DarkCyan; 
                Console.WriteLine($"\nВы атакуете {target.Name} ({target.Type}) с помощью {Weapons.GetWeaponName(weapon)}!");
                Console.WriteLine($"Базовый урон: {baseDamage}, множитель против {target.Type}: x{multiplier:F1}");
                Console.WriteLine($"Фактический урон по цели: {finalDamage}\n");
                Console.ResetColor();

                target.TakeDamage(finalDamage, weapon);
                if (!target.IsAlive)
                    monsters.RemoveAt(id);
                
            }
            else
                Console.WriteLine("Некорректный ID!");
        }

        static void AttackRandom(List<Monster> monsters)
        {
            if (monsters.Count == 0)
            {
                Console.WriteLine("Нет монстров для атаки!");
                return;
            }

            Random rand = new Random();
            int index = rand.Next(monsters.Count);

            WeaponType weapon = GetRandomWeapon();
            int baseDamage = Weapons.GetBaseDamage(weapon);

            var target = monsters[index];
            double multiplier = target.GetDamageMultiplier(weapon);
            int finalDamage = (int)(baseDamage * multiplier);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\nВы случайно атакуете {target.Name} ({target.Type}) с помощью {Weapons.GetWeaponName(weapon)}!");
            Console.WriteLine($"Базовый урон: {baseDamage}, множитель против {target.Type}: x{multiplier:F1}");
            Console.WriteLine($"Фактический урон по цели: {finalDamage}\n");
            Console.ResetColor();

            target.TakeDamage(finalDamage, weapon);
            if (!target.IsAlive)
                monsters.RemoveAt(index);
        }

        static void UpgradeMonster(List<Monster> monsters)
        {
            if (monsters.Count == 0)
            {
                Console.WriteLine("Нет монстров для улучшения!");
                return;
            }

            ShowMonsters(monsters);
            Console.Write("Введите ID монстра для апгрейда: ");
            if (int.TryParse(Console.ReadLine(), out int id) && id >= 0 && id < monsters.Count)
            {
                monsters[id].UpgradeMenu();
            }
            else
                Console.WriteLine("Некорректный ID!");
        }

        static void RemoveMonster(List<Monster> monsters)
        {
            if (monsters.Count == 0)
            {
                Console.WriteLine("Нет монстров для удаления!");
                return;
            }

            ShowMonsters(monsters);
            Console.Write("Введите ID монстра для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id) && id >= 0 && id < monsters.Count)
            {
                Console.WriteLine($"Монстр {monsters[id].Name} ({monsters[id].Type}) удалён.");
                monsters.RemoveAt(id);
            }
            else
                Console.WriteLine("Некорректный ID!");
        }

        static void ShowMonsters(List<Monster> monsters)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== Текущие монстры ===");
            Console.ResetColor();

            if (monsters.Count == 0)
            {
                Console.WriteLine("Нет живых монстров!");
                return;
            }

            for (int i = 0; i < monsters.Count; i++)
                Console.WriteLine($"{i}) {monsters[i]}");
        }
    }
}
