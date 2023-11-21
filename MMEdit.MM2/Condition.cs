namespace MMEdit.MM2
{
    using System;

    [Flags]
    public enum Condition
    {
        Good        = 0,
        Cursed      = 1,
        Silenced    = 1 << 1,
        Diseased    = 1 << 2,
        Poisoned    = 1 << 3,
        Asleep      = 1 << 4,
        Paralyzed   = 1 << 5,
        Unconscious = 1 << 6,
        Unalive     = 1 << 7,
        Dead        = Unalive | Cursed,
        Stone       = Unalive | Silenced,
        Eradicated  = Unalive | Poisoned,
    }
}
