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
    }

    public sealed class StatsFromConfig : Stats
    {
        readonly Dictionary<int, Stat> stats;
        readonly Buff[] buffs;

        public StatsFromConfig(Stat[] stats, Buff[] buffs)
        {
            this.stats = stats.ToDictionary(s => s.id, s => s);
            this.buffs = buffs;
        }

        public float Get(int statId)
        {
            return BuffsExtensions.Apply(
                buffs,
                statId,
                this.stats[statId].value
            );
        }
    }
}
