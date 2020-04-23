using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    // В идеале все методы нужно завернуть в Task, например:
    // Task<OnBeforeHitResult> OnBeforeHit(...)
    // Task OnDamageHappened(...)

    public interface BattleAspect
    {
        OnBeforeHitResult OnBeforeHit(Fighter source, Fighter consumer, OnBeforeHitResult result);

        void OnHitHappened(Fighter source, Fighter consumer, float damage);
    }

    public struct OnBeforeHitResult
    {
        public float currentDamage;
        public bool abort;

        public OnBeforeHitResult(float currentDamage, bool abort)
        {
            this.currentDamage = currentDamage;
            this.abort = abort;
        }
    }

    public class BattleAspects: BattleAspect
    {
        readonly BattleAspect[] aspects;

        public BattleAspects(params BattleAspect[] aspects)
        {
            this.aspects = aspects;
        }

        public OnBeforeHitResult OnBeforeHit(Fighter source, Fighter consumer, OnBeforeHitResult result)
        {
            var damageResult = new OnBeforeHitResult(0, false);
            foreach (var a in aspects)
            {
                damageResult = a.OnBeforeHit(source, consumer, damageResult);
                if (damageResult.abort)
                {
                    return damageResult;
                }
            }
            return damageResult;
        }

        public void OnHitHappened(Fighter source, Fighter consumer, float damage)
        {
            foreach (var a in aspects)
            {
                a.OnHitHappened(source, consumer, damage);
            }
        }
    }

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