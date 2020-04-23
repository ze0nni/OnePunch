using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    public interface Stats
    {
        float Get(int statId);
        List<Stat> Stats { get; }
        List<Buff> Buffs { get; }
    }

    public interface StatsFactory {
        Stats Produce(bool withBuffs);
    }

    public sealed class StatsFromConfig : Stats
    {
        readonly Dictionary<int, Stat> stats;
        readonly Stat[] statsList;
        readonly Buff[] buffs;

        public StatsFromConfig(Stat[] stats, Buff[] buffs)
        {
            this.stats = stats.ToDictionary(s => s.id, s => s);
            this.statsList = stats;
            this.buffs = buffs;
        }

        public List<Stat> Stats => new List<Stat>(statsList);

        public List<Buff> Buffs => new List<Buff>(buffs);

        public float Get(int statId)
        {
            return BuffsExtensions.Apply(
                buffs,
                statId,
                this.stats[statId].value
            );
        }


    }

    public sealed class StatsFactoryFromConfig : StatsFactory
    {
        public static StatsFactory Of(Data data)
        {
            return new StatsFactoryFromConfig(
                data.stats,
                data.settings.buffCountMin,
                data.settings.buffCountMax,
                data.settings.allowDuplicateBuffs
                    ? (BuffsFactory) new DuplcatesBuffsFactory(data.buffs)
                    : (BuffsFactory) new NoDuplcatesBuffsFactory(data.buffs)
            );
        }

        readonly Stat[] stats;
        readonly int minBuffs;
        readonly int maxBuffs;
        readonly BuffsFactory buffsFactory;

        public StatsFactoryFromConfig(Stat[] stats, int minBuffs, int maxBuffs, BuffsFactory buffsFactory)
        {
            this.stats = stats;
            this.minBuffs = minBuffs;
            this.maxBuffs = maxBuffs;
            this.buffsFactory = buffsFactory;
        }

        public Stats Produce(bool withBuffs)
        {
            return new StatsFromConfig(
                this.stats,
                withBuffs
                    ? buffsFactory.Produce(UnityEngine.Random.Range(minBuffs, maxBuffs))
                    : new Buff[] { }
            );
        }
    }
}
