using WaveFunction.ARPG.Chars;

namespace WaveFunction.ARPG.Battle
{
    public class Attack
    {
        public Attack(AttackBuilder builder)
        {
            _stats = builder.AtkStats;
        }

        public static AttackBuilder Make => new AttackBuilder();

        public double SumDamage => Enum.GetValues<StatUtil.AtkStats>().Sum(stat => _stats[stat]());


        public double this[StatUtil.AtkStats stat] => _stats[stat]();

        private readonly Dictionary<StatUtil.AtkStats, Func<double>> _stats;
    }

    public class AttackBuilder
    {
        public AttackBuilder()
        {
            AtkStats = new Dictionary<StatUtil.AtkStats, Func<double>>();
            foreach (var stat in Enum.GetValues<StatUtil.AtkStats>())
            {
                AtkStats.Add(stat, static () => 0);
            }
        }

        public Attack Result => new Attack(this);

        public AttackBuilder WithStat(StatUtil.AtkStats s, double i)
        {
            AtkStats[s] = () => i;
            return this;
        }

        public Dictionary<StatUtil.AtkStats, Func<double>> AtkStats { get; }
    }
}
