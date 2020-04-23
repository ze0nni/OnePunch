using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OnBeforeHitResult {
    public float currentDamage;
    public bool abort;

    public OnBeforeHitResult(float currentDamage, bool abort)
    {
        this.currentDamage = currentDamage;
        this.abort = abort;
    }   
}

// В идеале все методы нужно завернуть в Task, например:
// Task<OnBeforeHitResult> OnBeforeHit(...)
// Task OnDamageHappened(...)
public interface BattleAspect {
    OnBeforeHitResult OnBeforeHit(Fighter source, Fighter consumer, OnBeforeHitResult result);
    void OnHitHappened(Fighter source, Fighter consumer, float damage);
}

sealed public class BattleArea
{
    private readonly BattleAspect[] aspects;

    public BattleArea(
        params BattleAspect[] aspects
    ) {
        this.aspects = aspects;
    }

    public void Attack(
        Fighter source,
        Fighter consumer
    ) {
        var damageResult = new OnBeforeHitResult(0, false);
        foreach (var a in aspects) {
            damageResult = a.OnBeforeHit(source, consumer, damageResult);
            if (damageResult.abort) {
                return;
            }
        }
        

        consumer.Hit(damageResult.currentDamage);

        foreach (var a in aspects) {
            a.OnHitHappened(source, consumer, damageResult.currentDamage);
        }
    }
}
