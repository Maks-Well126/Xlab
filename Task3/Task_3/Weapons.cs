namespace ValheimGame
{
    public enum WeaponType
    {
        Sword,
        Bow,
        Axe,
        Fireball
    }

    public static class Weapons
    {
        public static int GetBaseDamage(WeaponType weapon)
        {
            return weapon switch
            {
                WeaponType.Sword => 50,
                WeaponType.Axe => 60,
                WeaponType.Bow => 40,
                WeaponType.Fireball => 70,
                _ => 50
            };
        }

        public static string GetWeaponName(WeaponType weapon)
    {
        return weapon switch
        {
            WeaponType.Sword => "Меч",
            WeaponType.Axe => "Топор",
            WeaponType.Bow => "Лук",
            WeaponType.Fireball => "Огненный шар",
            _ => "default"
        };
    }
    }

    
}
