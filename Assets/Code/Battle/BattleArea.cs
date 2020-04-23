using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    // BattleArea вместе с BattleAspect позволяют что-то похожее на интерпритатор 

    public interface BattleArea {
        BattleAspect Aspect { get; }

        void Attack(Fighter source, Fighter consumer);
    }

    sealed public class CommonBattleArea: BattleArea
    {
        private readonly BattleAspect aspect;
        public BattleAspect Aspect { get => this.aspect; }

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
            var damageResult = aspect.OnBeforeHit(this, source, consumer, new OnBeforeHitResult(0, false));
            if (damageResult.abort) {
                return;
            }

            consumer.Hit(damageResult.currentDamage);

            aspect.OnHitHappened(this, source, consumer, damageResult.currentDamage);
        }
    }

}