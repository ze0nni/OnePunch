using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{   
    sealed public class CommonBattleArea
    {
        private readonly BattleAspect aspect;

        public CommonBattleArea(
            BattleAspect aspect
        )
        {
            this.aspect = aspect;
        }

        public void Attack(
            Fighter source,
            Fighter consumer
        )
        {
            var damageResult = aspect.OnBeforeHit(source, consumer, new OnBeforeHitResult(0, false));
            if (damageResult.abort) {
                return;
            }


            consumer.Hit(damageResult.currentDamage);

            aspect.OnHitHappened(source, consumer, damageResult.currentDamage);
        }
    }

}