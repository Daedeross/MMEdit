namespace MMEdit.MM2
{
    using System;

    [Flags]
    public enum MM2Class
    {
        Knight  = 1,
        Paladin = 1 << 1,
        Archer = 1 << 2,
        Cleric = 1 << 3,
        Sorcerer = 1 << 4,
        Robber = 1 << 5,
        Ninja = 1 << 6,
        Barbarian = 1 << 7
    }
}
