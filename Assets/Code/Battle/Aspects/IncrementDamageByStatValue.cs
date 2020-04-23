using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Aspects
{
    public class IncrementDamageByStatValue : BattleAspect
    {
        readonly int statId;

        public IncrementDamageByStatValue(int statId)
        {
            this.statId = statId;
        }

        public OnBeforeHitResult OnBeforeHit(BattleArea area, Fighter source, Fighter consumer, OnBeforeHitResult result)
        {
            result.currentDamage += source.Stat(statId);

            return result;
        }

        public void OnHitHappened(BattleArea area, Fighter source, Fighter consumer, float baseDamage)
        {
            //
        }
    }
}
