public static class Program
{
    private static void Main(string[] args)
    {
        var monsters = new List<Monster>();
        
        Console.WriteLine("Welcome to XLab Diablo\n");
        
        while (true)
        {
            Console.WriteLine("Choose option:");
            Console.WriteLine("1. Add Skillet");
            Console.WriteLine("2. Take Damage to the first monster");
            Console.WriteLine("3. Upgrade the first monster");
            Console.WriteLine("4. Exit");
            
            Console.Write("Enter your choice: ");
            var input = ReadInput();
            
            Console.Clear();
            
            if (input is "1") AddSkillet(monsters);
            else if (input is "2" && monsters.Count > 0) TakeDamageToMonster(monsters[0]);
            else if (input is "3" && monsters.Count > 0) UpgradeMonster(monsters[0]);
            else if (input is "4") break;
            else Console.WriteLine("Invalid choice or no monsters available.\n");
        }
    }
    
    private static void AddSkillet(List<Monster> monsters)
    {
        monsters.Add(new Skillet(100, $"Monster {monsters.Count + 1}"));
        Console.WriteLine($"Monster {monsters.Count} created!\n");
    }

    private static void TakeDamageToMonster(Monster monster)
    {
        Console.Write("Enter damage: ");
        var input = ReadInput();
        
        if (int.TryParse(input, out var damage))
        {
            var oldHp = monster.Hp;
            monster.TakeDamage(damage);
            var newHp = monster.Hp;

            Console.WriteLine($"{monster.Name} took {damage}. Hp: {oldHp} -> {newHp}");

            Console.WriteLine("\n--- Current Effects ---");
            monster.ShowEffects();
            Console.WriteLine("-----------------------\n");
        }
        else
        {
            Console.WriteLine($"Invalid damage {input}");
        }
    }

    private static void UpgradeMonster(Monster monster)
    {
        Console.WriteLine("\nChoose upgrade:");
        Console.WriteLine("1. Leather Armor (+25)");
        Console.WriteLine("2. Iron Armor (+60)");
        Console.WriteLine("3. God Armor (+200)");
        Console.WriteLine("4. Miss Effect (25%)");
        Console.WriteLine("5. Back to main menu");
        Console.Write("Your choice: ");
        
        var choice = ReadInput();
        var health = monster.HealthComponent;

        // Проверяем наличие брони
        bool hasArmor = health is LeatherArmorHealth or IronArmorHealth or GodArmorHealth 
                        || (health is HealthDecorator hd && ContainsArmor(hd));

        switch (choice)
        {
            case "1":
            case "2":
            case "3":
                if (hasArmor)
                {
                    Console.WriteLine("The monster already has armor! You can only add MissEffect.\n");
                    AskAddMissEffect(monster);
                    return;
                }
                if (choice == "1") health = new LeatherArmorHealth(health);
                else if (choice == "2") health = new IronArmorHealth(health);
                else health = new GodArmorHealth(health);
                Console.WriteLine("Armor added!");
                break;

            case "4":
                health = new MissEffectHealth(25, health);
                Console.WriteLine("MissEffect added!");
                break;

            default:
                Console.WriteLine("Returning to main menu...\n");
                return;
        }

        monster.HealthComponent = health;
    }

    private static void AskAddMissEffect(Monster monster)
    {
        Console.WriteLine("1. Add MissEffect (25%)");
        Console.WriteLine("2. Return to main menu");
        Console.Write("Enter choice: ");
        var choice = ReadInput();

        if (choice == "1")
        {
            monster.HealthComponent = new MissEffectHealth(25, monster.HealthComponent);
            Console.WriteLine("MissEffect added!\n");
        }
        else
        {
            Console.WriteLine("Returning to main menu...\n");
        }
    }

    private static bool ContainsArmor(HealthDecorator decorator)
    {
        while (decorator != null)
        {
            if (decorator is LeatherArmorHealth or IronArmorHealth or GodArmorHealth)
                return true;
            if (decorator.Decorable is HealthDecorator inner)
                decorator = inner;
            else break;
        }
        return false;
    }

    private static string ReadInput() =>
        Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
}

public class Skillet : Monster
{
    public Skillet(int hp, string name = "Noname")
        : base(hp, name) { }
}

public abstract class Monster : IDamageable
{
    private Health _health;
    public int Hp => _health.Value;
    public Health HealthComponent
    {
        get => _health;
        set => _health = value ?? throw new ArgumentNullException(nameof(value));
    }
    public string Name { get; set; }

    protected Monster(int hp, string name = "Noname")
        : this(new Health(hp), name) { }

    protected Monster(Health health, string name = "Noname")
    {
        Name = name;
        HealthComponent = health;
    }

    public void TakeDamage(int damage) => _health.TakeDamage(damage);

    public void ShowEffects()
    {
        var effects = new List<string>();
        var current = _health;
        int totalMissEffect = 0;

        while (current is HealthDecorator decorator)
        {
            switch (decorator)
            {
                case LeatherArmorHealth:
                    effects.Add("Leather Armor (+25)");
                    break;
                case IronArmorHealth:
                    effects.Add("Iron Armor (+60)");
                    break;
                case GodArmorHealth:
                    effects.Add("God Armor (+200)");
                    break;
                case MissEffectHealth miss:
                    totalMissEffect += miss.EffectValue;
                    break;
            }
            current = decorator.Decorable;
        }

        if (totalMissEffect > 0)
            effects.Add($"MissEffect ({totalMissEffect}%)");

        if (effects.Count == 0)
            Console.WriteLine("No effects applied.");
        else
            Console.WriteLine(string.Join(", ", effects));
    }
}

public class Health : IDamageable
{
    private int _value;
    public int Value
    {
        get => _value;
        protected set => _value = value > 0 ? value : 0;
    }

    public Health(int hp)
    {
        _value = hp >= 0 ? hp : throw new ArgumentException($"Hp can't be negative {hp}", nameof(hp));
    }

    public virtual void TakeDamage(int damage) => Value -= Math.Max(0, damage);
}

public interface IDamageable
{
    void TakeDamage(int damage);
}

public abstract class HealthDecorator : Health
{
    public readonly Health Decorable;

    protected HealthDecorator(Health decorable)
        : base(decorable.Value)
    {
        Decorable = decorable ?? throw new ArgumentNullException(nameof(decorable));
    }

    public sealed override void TakeDamage(int damage)
    {
        Decorable.TakeDamage(AffectDamage(damage));
        Value = Decorable.Value;
    }

    protected abstract int AffectDamage(int damage);
}

public sealed class LeatherArmorHealth : HealthDecorator
{
    private readonly int _armor = 25;
    public LeatherArmorHealth(Health decorable) : base(decorable) { }
    protected override int AffectDamage(int damage) => damage - _armor;
}

public sealed class IronArmorHealth : HealthDecorator
{
    private readonly int _armor = 60;
    public IronArmorHealth(Health decorable) : base(decorable) { }
    protected override int AffectDamage(int damage) => damage - _armor;
}

public sealed class GodArmorHealth : HealthDecorator
{
    private readonly int _armor = 200;
    public GodArmorHealth(Health decorable) : base(decorable) { }
    protected override int AffectDamage(int damage) => damage - _armor;
}

public sealed class MissEffectHealth : HealthDecorator
{
    public int EffectValue => _effect;
    private readonly int _effect;
    public MissEffectHealth(int effect, Health decorable)
        : base(decorable) => _effect = Math.Clamp(effect, 0, 100);

    protected override int AffectDamage(int damage) => (int)(damage * ((100 - _effect) / 100f));
}
