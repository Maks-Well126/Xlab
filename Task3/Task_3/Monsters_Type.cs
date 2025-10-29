namespace ValheimBattle
{
    class Draugr : Monster
    {
        public Draugr(string name) : base(name, "Драугр", 120) { }
        public override string Move() => "Драугр медленно шагает по болотам";
    }

    class Troll : Monster
    {
        public Troll(string name) : base(name, "Тролль", 250) { }
        public override string Move() => "Тролль топает по земле, сотрясая всё вокруг!";
    }

    class Deathsquito : Monster
    {
        public Deathsquito(string name) : base(name, "Смертокрыл", 80) { }
        public override string Move() => "Смертокрыл летает и готовит смертоносное жало!";
    }
}
