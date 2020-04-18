using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Player
{
    readonly Dictionary<int, float> stats = new Dictionary<int, float>();
    readonly Buff[] buffs;

    public Player(
        IEnumerable<Stat> stats,
        IEnumerable<Buff> buffs
    ) {
        this.buffs = buffs.ToArray();

        foreach (var s in stats) {
            this.stats[s.id] = BuffsExtensions.Apply(buffs, s.id, s.value);
        }
    }

    public float Stat(int statId) {
        if (stats.TryGetValue(statId, out var value)) {
            return value;
        } else {
            return 0;
        }
    }
}
