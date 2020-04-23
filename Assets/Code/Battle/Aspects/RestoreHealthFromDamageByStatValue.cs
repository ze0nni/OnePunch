using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Aspects
{
    public class RestoreHealthFromDamageByStatValue : BattleAspect
    {
        readonly int statId;

        public RestoreHealthFromDamageByStatValue(int statId)
        {
            this.statId = statId;
        }

        public OnBeforeHitResult OnBeforeHit(BattleArea area, Fighter source, Fighter consumer, OnBeforeHitResult result)
        {
            
            return result;
        }

        public void OnHitHappened(BattleArea area, Fighter source, Fighter consumer, float baseDamage)
        {
            var statValue = source.Stat(statId);
            if (statValue > 0)
            {
                var hillValue = (baseDamage * statValue) / 100f;
                source.Hill(hillValue);
            }
        }
    }
}
