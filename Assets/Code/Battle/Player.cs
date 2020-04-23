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

}