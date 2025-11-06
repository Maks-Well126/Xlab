public class Program
{
    public static void Main()
    {
        IMonsterHandler<Monster> handler = new MonsterHandler();

        // Контравариантность позволяет это
        IMonsterHandler<Zombie> zombieHandler = handler;
        IMonsterHandler<Skillet> skilletHandler = handler;

        zombieHandler.Handle(new Zombie());
        skilletHandler.Handle(new Skillet());
    }
}

public interface IMonsterHandler<in T>
{
    void Handle(T monster);
}

public class MonsterHandler : IMonsterHandler<Monster>
{
    public void Handle(Monster monster)
    {
        Console.WriteLine($"{monster.GetType().Name} получил урон!");
    }
}

public class Monster { }

public class Zombie : Monster { }

public class Skillet : Monster { }

