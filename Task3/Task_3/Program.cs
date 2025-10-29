using System;
using System.Collections.Generic;

namespace ValheimBattle
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
                Console.WriteLine("2) Добавить Тролля");
                Console.WriteLine("3) Добавить Смертокрыла");
                Console.WriteLine("4) Нанести урон выбранному монстру");
                Console.WriteLine("5) Нанести урон случайному монстру");
                Console.WriteLine("6) Улучшить монстра");
                Console.WriteLine("7) Удалить монстра");
                Console.WriteLine("8) Показать всех монстров");
                Console.WriteLine("q) Выход");
                Console.Write("\nВыбор: ");

                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddMonster(monsters, "Draugr");
                        break;
                    case "2":
                        AddMonster(monsters, "Troll");
                        break;
                    case "3":
                        AddMonster(monsters, "Deathsquito");
                        break;
                    case "4":
                        AttackById(monsters);
                        break;
                    case "5":
                        AttackRandom(monsters);
                        break;
                    case "6":
                        UpgradeMonster(monsters);
                        break;
                    case "7":
                        RemoveMonster(monsters);
                        break;
                    case "8":
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
            while (input?.Trim().ToLower() is not "q");
        }

        static void AddMonster(List<Monster> monsters, string type)
        {
            Console.Write("Введите имя монстра (или оставьте пустым): ");
            string nameInput = Console.ReadLine();

            string name = string.IsNullOrWhiteSpace(nameInput) ? "Безымянный" : nameInput;

#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
            Monster m = type switch
            {
                "Draugr" => new Draugr(name),
                "Troll" => new Troll(name),
                "Deathsquito" => new Deathsquito(name),
                _ => null
            };
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.

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
                Console.Write("Введите урон: ");
                if (int.TryParse(Console.ReadLine(), out int damage))
                {
                    var target = monsters[id];
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nВы атакуете {target.Name} ({target.Type}) и наносите {damage} урона!");
                    Console.ResetColor();

                    target.TakeDamage(damage);
                    if (!target.IsAlive)
                        monsters.RemoveAt(id);
                }
                else
                    Console.WriteLine("Некорректный ввод урона!");
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
            int damage = rand.Next(50, 151);

            var target = monsters[index];
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nВы случайно атакуете {target.Name} ({target.Type}) и наносите {damage} урона!");
            Console.ResetColor();

            target.TakeDamage(damage);
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
                var m = monsters[id];

                Console.WriteLine("\nВыберите улучшение:");
                Console.WriteLine("1) +10 брони");
                Console.WriteLine("2) Включить невидимость");
                Console.WriteLine("3) +50 HP");
                Console.WriteLine("4) Всё сразу (суперапгрейд)");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        m.HasArmor = true;
                        Console.WriteLine($"{m.Name} ({m.Type}) теперь носит броню!");
                        break;
                    case "2":
                        m.IsInvisible = true;
                        Console.WriteLine($"{m.Name} ({m.Type}) теперь умеет становиться невидимым!");
                        break;
                    case "3":
                        m.Heal(50);
                        break;
                    case "4":
                        m.HasArmor = true;
                        m.IsInvisible = true;
                        m.Heal(50);
                        Console.WriteLine($" {m.Name} ({m.Type}) получил суперапгрейд!");
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор!");
                        break;
                }
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
            {
                Console.WriteLine($"{i}) {monsters[i]}");
            }
        }
    }
}
