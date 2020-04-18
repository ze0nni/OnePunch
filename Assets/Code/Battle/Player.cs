using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Player : Fighter
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

    private float Update(int statId, Func<float, float> consumer) {
        var newValue = consumer.Invoke(Stat(statId));
        this.stats[statId] = newValue;
        return newValue;
    }

    public float Damage()
    {
        return Stat(2);
    }

    public void Hit(float originDamage, out float releasedDamage)
    {
        var armor = Stat(1);
        var damage = originDamage * ((100 - armor) / 100);

        Update(0, current => current - damage);

        releasedDamage = damage;
    }

    public void ConsumeMeat(float releasedDamage)
    {
        var vampire = Stat(3);
        var hillValue = releasedDamage * ((vampire) / 100);

        Update(0, current => current + hillValue);
    }
}
