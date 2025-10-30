namespace ValheimGame
{
    public class Draugr : Monster
    {
        public Draugr(string name) : base(name, "Драугр", 120, false) { }

        public override string Move() => "Драугр медленно шагает по темному лесу";

        public override double GetDamageMultiplier(WeaponType weapon)
        {
            return weapon switch
            {
                WeaponType.Axe => 1.2,
                WeaponType.Bow => 0.8,
                WeaponType.Fireball => 1.0,
                _ => 1.0
            };
        }
    }

    public class Greydwarf : Monster
    {
        public Greydwarf(string name) : base(name, "Грейдварф", 100, false) { }

        public override string Move() => "Грейдварф бежит, размахивая дубиной!";

        public override double GetDamageMultiplier(WeaponType weapon)
        {
            return weapon switch
            {
                WeaponType.Axe => 1.3,
                WeaponType.Fireball => 0.7,
                _ => 1.0
            };
        }
    }

    public class Deathsquito : Monster
    {
        public Deathsquito(string name) : base(name, "Смертокрыл", 60, true) { }

        public override string Move() => "Смертокрыл летает и готовит смертоносное жало!";

        public override double GetDamageMultiplier(WeaponType weapon)
        {
            return weapon switch
            {
                WeaponType.Bow => 1.5,
                WeaponType.Sword => 0.5,
                WeaponType.Axe => 0.8,
                _ => 1.0
            };
        }
    }

    public class Seeker : Monster
    {
        public Seeker(string name) : base(name, "Искатель", 150, true)
        {
            Armor = 5;
        }

        public override string Move() => "Искатель пролетает над полем битвы, издавая пронзительное жужжание!";

        public override double GetDamageMultiplier(WeaponType weapon)
        {
            return weapon switch
            {
                WeaponType.Fireball => 2.0,
                WeaponType.Sword => 0.7,
                WeaponType.Bow => 1.0,
                WeaponType.Axe => 0.8,
                _ => 1.0
            };
        }
    }
}
