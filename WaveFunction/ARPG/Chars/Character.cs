using WaveFunction.ARPG.Battle;
using WaveFunction.ARPG.Items;

namespace WaveFunction.ARPG.Chars
{
    public enum TurnPhase
    {
        Action,
        BonusAction,
        Movement,
        Reaction,
        OnDeath,
        OnHit,
        OnCrit,
        OnReceiveCrit
    }

    public class ActionReport
    {
    }

    public class Pawn
    {
        public Pawn(Character c)
        {
            CharBase = c;
        }

        public Character CharBase { get; }

        public ActionReport PerformTurnPhase(TurnPhase currentPhase) => CharBase.PerformTurnPhase(currentPhase);
    }

    public class Character
    {
        public Character(CharacterBuilder builder)
        {
            _baseStats = builder.Stats;
            _equipment = builder.Equipment;
            _ctrl = builder.Ctrl;
        }

        public ActionReport PerformTurnPhase(TurnPhase phase) => _ctrl.PerformTurnPhase(phase);

        private readonly IController _ctrl;
        public static CharacterBuilder Make => new CharacterBuilder();

        public Character WithController(IController ctrl) =>
            new CharacterBuilder(_baseStats, _equipment).WithController(ctrl).Result;

        public void HitWith(Attack attack)
        {
            if (attack == null) throw new ArgumentNullException(nameof(attack));

            var newValue = _baseStats[StatUtil.CharacterStats.HealthCurrent]() - attack.SumDamage;
            _baseStats[StatUtil.CharacterStats.HealthCurrent] = () => newValue;
        }

        public Character WithEquipment(IEquipment item)
        {
            var builder = new CharacterBuilder(_baseStats, _equipment);
            foreach (var value in Enum.GetValues<EquipSlots>())
            {
                builder.WearingItem(value, _equipment[value]);
            }

            builder.WearingItem(item.TargetSlot(), item);
            return builder.Result;
        }

        public double this[StatUtil.CharacterStats name] => _baseStats[name]();
        private readonly Dictionary<StatUtil.CharacterStats, Func<double>> _baseStats;

        private readonly Dictionary<EquipSlots, IItemBase> _equipment;
    }


    public class CharacterBuilder
    {
        public CharacterBuilder()
        {
            Stats = new Dictionary<StatUtil.CharacterStats, Func<double>>();
            foreach (var stat in Enum.GetValues<StatUtil.CharacterStats>())
            {
                Stats.Add(stat, static () => 100);
            }

            Equipment = new Dictionary<EquipSlots, IItemBase>();
            Ctrl = new PRomControl(static _ => new ActionReport());
            foreach (var value in Enum.GetValues<EquipSlots>())
            {
                Equipment.Add(value, StatUtil.Defaults[value]);
            }
        }

        public CharacterBuilder(Dictionary<StatUtil.CharacterStats, Func<double>> stats,
            Dictionary<EquipSlots, IItemBase> equip)
        {
            Stats = stats;
            Equipment = equip;
            Ctrl = new PRomControl(static _ => new ActionReport());
        }

        public CharacterBuilder WithStat(StatUtil.CharacterStats statName, double statVal)
        {
            Stats[statName] = () => statVal;
            return this;
        }

        public CharacterBuilder WithStat(StatUtil.CharacterStats statName, Func<Double> statVal)
        {
            Stats[statName] = statVal;
            return this;
        }

        public CharacterBuilder WearingItem(EquipSlots slot, IItemBase item)
        {
            Equipment[slot] = item;
            return this;
        }

        public Character Result => new Character(this);

        public Dictionary<StatUtil.CharacterStats, Func<double>> Stats { get; }
        public Dictionary<EquipSlots, IItemBase> Equipment { get; }
        public IController Ctrl { get; private set; }
        public Character Player => CharDefs.Player;

        public CharacterBuilder WithController<T>()
        {
            var temp = Activator.CreateInstance<T>();
            if (temp is not IController controller)
                throw new InvalidDataException($"{typeof(T)} is not a controller");

            WithController(controller);
            return this;
        }

        public CharacterBuilder WithController(IController controller)
        {
            Ctrl = controller;
            return this;
        }
    }
}
