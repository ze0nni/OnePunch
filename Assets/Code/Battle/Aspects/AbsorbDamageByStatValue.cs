using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Aspects
{
    public class AbsorbDamageByStatValue : BattleAspect
    {
        readonly private int statId;

        public AbsorbDamageByStatValue(int statId)
        {
            this.statId = statId;
        }

        public OnBeforeHitResult OnBeforeHit(Fighter source, Fighter consumer, OnBeforeHitResult result)
        {
            result.currentDamage = (result.currentDamage * (100 - consumer.Stat(statId))) / 100f;
            return result;
        }

        public void OnHitHappened(Fighter source, Fighter consumer, float baseDamage)
        {
            //
        }
    }
}
