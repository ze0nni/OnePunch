using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{

    public class Player : Fighter
    {
        readonly Stats stats;

        public Player(
            int healthStatId,
            Stats stats
        )
        {
            this.stats = stats;
            this.health = stats.Get(healthStatId);
        }

        private float health;
        public float Health { get => health; }

        public float Stat(int statId)
        {
            return this.stats.Get(statId);
        }

        public Stats Stats { get => this.stats; }

        public void Hit(float damage)
        {
            if (damage < 0) {
                return;
            }
            this.health = Math.Max(0, health - damage);
        }

        public void Hill(float value)
        {
            if (value < 0) {
                return;
            }
            this.health += value;
        }
    }

    public interface PlayerFactory {
        Player Produce(bool withBuffs);
    }

    public sealed class CommonPlayerFactory : PlayerFactory
    {
        readonly int healthStatId;
        readonly StatsFactory statsFactory;

        public CommonPlayerFactory(int healthStatId, StatsFactory factory)
        {
            this.healthStatId = healthStatId;
            this.statsFactory = factory;
        }

        public Player Produce(bool withBuffs)
        {
            return new Player(
                healthStatId,
                statsFactory.Produce(withBuffs)
            );
        }
    }
}