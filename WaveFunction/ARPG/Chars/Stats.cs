using System.Collections.Immutable;
using WaveFunction.ARPG.Characters.Items;

namespace WaveFunction.ARPG.Characters
{
    public static class StatUtil
    {
        public enum CharacterStats
        {
            HealthMax = Stat.HealthMax,
            HealthCurrent = Stat.HealthCurrent,

            MagicMax = Stat.MagicMax,
            MagicCurrent = Stat.MagicCurrent,

            StaminaMax = Stat.StaminaMax,
            StaminaCurrent = Stat.StaminaCurrent,
            
            
        };

        public enum DamageStats
        {
            SlashDamage = Stat.SlashDamage, // Solidum
            PierceDamage = Stat.PierceDamage, // Spatium
            StrikeDamage = Stat.StrikeDamage, // Inertiae
        
            WindDamage = Stat.WindDamage, // Solidum, Inertiae
            FireDamage = Stat.FireDamage, // Febris
            LightningDamage = Stat.LightningDamage, // Ordinem
            IceDamage = Stat.IceDamage, // Ordinem, Febris
        
            PsychicDamage = Stat.PsychicDamage, // Varias
            PoisonDamage = Stat.PoisonDamage, // Subsidium
            BleedDamage = Stat.BleedDamage, // Ordinem, Subsidium
        
            RadiantDamage = Stat.RadiantDamage, // Lumines
            CrushDamage = Stat.CrushDamage, // Varias
            NecroticDamage = Stat.NecroticDamage, // Subsidium, Lumines
        };

        public enum AtkStats
        {
            SlashDamage = Stat.SlashDamage, // Solidum
            PierceDamage = Stat.PierceDamage, // Spatium
            StrikeDamage = Stat.StrikeDamage, // Inertiae
        
            WindDamage = Stat.WindDamage, // Solidum, Inertiae
            FireDamage = Stat.FireDamage, // Febris
            LightningDamage = Stat.LightningDamage, // Ordinem
            IceDamage = Stat.IceDamage, // Ordinem, Febris
        
            PsychicDamage = Stat.PsychicDamage, // Varias
            PoisonDamage = Stat.PoisonDamage, // Subsidium
            BleedDamage = Stat.BleedDamage, // Ordinem, Subsidium
        
            RadiantDamage = Stat.RadiantDamage, // Lumines
            CrushDamage = Stat.CrushDamage, // Varias
            NecroticDamage = Stat.NecroticDamage, // Subsidium, Lumines

            Stun = Stat.StunChance,
            BlockPen = Stat.BlockPenetration,
            Acc = Stat.Accuracy,
            Repeat = Stat.Repeat,
            Chain = Stat.Chain,
            Splash = Stat.Splash
        };

        public static AtkStats ToAtk(this DamageStats s) => (AtkStats)s;
        public static Stat ToStat(this AtkStats s) => (Stat)s;
        public static Stat ToStat(this DamageStats s) => (Stat)s;
        public static Stat ToStat(this CharacterStats s) => (Stat)s;
        public static bool IsAtkStat(this Stat s) => Enum.GetValues<AtkStats>().Any(stats => s == (Stat)stats);
        public static bool IsDmgStat(this Stat s) => Enum.GetValues<DamageStats>().Any(stats => s == (Stat)stats);
        public static bool IsCharStat(this Stat s) => Enum.GetValues<CharacterStats>().Any(stats => s == (Stat)stats);

        public static Dictionary<EquipSlots, IEquipment> Defaults => new Dictionary<EquipSlots, IEquipment>()
        {
            { EquipSlots.Head, ItemStore.Helm.Done },
            { EquipSlots.Chest, ItemStore.Chest.Done },
            { EquipSlots.Pants, ItemStore.Pants.Done },
            { EquipSlots.Boots, ItemStore.Boots.Done },
            { EquipSlots.Gloves, ItemStore.Gloves.Done },
            { EquipSlots.Ring1, ItemStore.Ring.Done },
            { EquipSlots.Ring2, ItemStore.Ring.Done },
            { EquipSlots.Amulet, ItemStore.Amulet.Done },
            { EquipSlots.LeftHand, ItemStore.Weapon.Done },
            { EquipSlots.RightHand, ItemStore.Weapon.Done },
            { EquipSlots.Belt, ItemStore.Belt.Done },
            { EquipSlots.Jewel, ItemStore.Jewel.Done },
        };
    }

    public enum Stat
    {
        HealthMax,
        StaminaMax,
        MagicMax,
        HealthCurrent,
        StaminaCurrent,
        MagicCurrent,

        SlashDamage, // Solidum
        PierceDamage, // Spatium
        StrikeDamage, // Inertiae
        
        WindDamage, // Solidum, Inertiae
        FireDamage, // Febris
        LightningDamage, // Ordinem
        IceDamage, // Ordinem, Febris
        
        PsychicDamage, // Varias
        PoisonDamage, // Subsidium
        BleedDamage, // Ordinem, Subsidium
        
        RadiantDamage, // Lumines
        CrushDamage, // Varias
        NecroticDamage, // Subsidium, Lumines
        
        StunChance,
        BlockPenetration,
        Accuracy,
        Repeat,
        Chain,
        Splash
    }

    public enum EquipSlots
    {
        Head,
        Chest,
        Pants,
        Boots,
        Gloves,
        Ring1,
        Ring2,
        Amulet,
        LeftHand,
        RightHand,
        Belt,
        Jewel
    }
}
