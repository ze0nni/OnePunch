using Assets.Code.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{

    public interface BuffsFactory
    {
        Buff[] Produce(int amount);
    }

    // Не стал делать тесты для Random но технически можно и это проверить

    public sealed class NoDuplcatesBuffsFactory : BuffsFactory
    {
        readonly Buff[] buffs;

        public NoDuplcatesBuffsFactory(Buff[] buffs)
        {
            this.buffs = buffs;
        }

        public Buff[] Produce(int amount)
        {
            return new Shuffled<Buff>(buffs).Take(amount).ToArray();
        }
    }

    public sealed class DuplcatesBuffsFactory: BuffsFactory {
        readonly Buff[] buffs;

        public DuplcatesBuffsFactory(Buff[] buffs)
        {
            this.buffs = buffs;
        }

        public Buff[] Produce(int amount)
        {
            var output = new Buff[amount];
            for (var i = 0; i < amount; i++) {
                output[i] = buffs[UnityEngine.Random.Range(0, buffs.Length)];
            }
            return output;
        }
    }

}
