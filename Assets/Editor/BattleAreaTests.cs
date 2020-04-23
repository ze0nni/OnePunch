using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class BattleAreaTests
{
    [Test]
    public void Test_hit_with_zero_damage() {
        var area = new BattleArea();

        var source = new TestFighter();
        var consumer = new TestFighter();

        var damages = new List<float>();
        consumer.onHit = (damage) => { damages.Add(damage); };

        area.Attack(source, consumer);

        Assert.AreEqual(
            new float[] { 0 },
            damages
        );
    }

    [Test]
    public void Test_hit_with_1_damage()
    {
        var area = new BattleArea(
            new TestAspect() {
                onBeforeHitFunc = (s, c, result) => {
                    result.currentDamage += 1;
                    return result;
                }
            }
        );

        var source = new TestFighter();
        var consumer = new TestFighter();

        var damages = new List<float>();
        consumer.onHit = (damage) => { damages.Add(damage); };

        area.Attack(source, consumer);

        Assert.AreEqual(
            new float[] { 1 },
            damages
        );
    }

    [Test]
    public void Test_no_hit_if_aborted()
    {
        var area = new BattleArea(
            new TestAspect()
            {
                onBeforeHitFunc = (s, c, result) => {
                    result.abort = true;
                    return result;
                }
            }
        );

        var consumer = new TestFighter();

        var damages = new List<float>();
        consumer.onHit = (damage) => { Assert.Fail("Must never calls"); };

        area.Attack(null, consumer);
    }

    [Test]
    public void Test_hit_with_3_damage()
    {
        var area = new BattleArea(
            new TestAspect()
            {
                onBeforeHitFunc = (s, c, result) => {
                    result.currentDamage += 1;
                    return result;
                }
            },
            new TestAspect()
            {
                onBeforeHitFunc = (s, c, result) => {
                    result.currentDamage += 2;
                    return result;
                }
            }
        );

        var source = new TestFighter();
        var consumer = new TestFighter();

        var damages = new List<float>();
        consumer.onHit = (damage) => { damages.Add(damage); };

        area.Attack(source, consumer);

        Assert.AreEqual(
            new float[] { 3 },
            damages
        );
    }

    [Test]
    public void Test_OnHitHappened_calls() {
        var calls = new List<float>();

        var area = new BattleArea(
            new TestAspect() {
                onBeforeHitFunc = (s, c, result) => {
                    return result;
                },
                onHitHappenedFunc = (s,c,damage) => {
                    calls.Add(damage);
                }
            }
        );

        var consumer = new TestFighter();
        consumer.onHit = (damage) => { };

        area.Attack(null, consumer);
        area.Attack(null, consumer);

        Assert.AreEqual(
            new float[] { 0, 0 },
            calls
        );
    }
}

internal class TestAspect : BattleAspect
{
    public delegate OnBeforeHitResult OnBeforeHitFunc(Fighter source, Fighter consumer, OnBeforeHitResult result);
    public OnBeforeHitFunc onBeforeHitFunc;
    public OnBeforeHitResult OnBeforeHit(Fighter source, Fighter consumer, OnBeforeHitResult result)
    {
        return onBeforeHitFunc.Invoke(source, consumer, result);
    }

    public delegate void OnHitHappenedFunc(Fighter source, Fighter consumer, float baseDamage);
    public OnHitHappenedFunc onHitHappenedFunc;
    public void OnHitHappened(Fighter source, Fighter consumer, float damage)
    {
        onHitHappenedFunc?.Invoke(source, consumer, damage);
    }
}

internal class TestFighter : Fighter
{
    public delegate float GetStat(int statId);

    public GetStat getStat = null;

    public float Stat(int statId)
    {
        if (null == getStat) throw new System.NotImplementedException();
        return getStat.Invoke(statId);
    }

    public delegate float OnDamage();

    public OnDamage onDamage;

    public float Damage()
    {
        if (null == onDamage) throw new System.NotImplementedException();
        return onDamage.Invoke();
    }

    public delegate void OnHit(float damage);

    public OnHit onHit;

    public void Hit(float damage)
    {
        if (null == onHit) throw new System.NotImplementedException();
        onHit.Invoke(damage);
    }

    public delegate void OnHill(float value);

    public OnHill onHill;

    public void Hill(float value)
    {
        if (null == onHill) throw new System.NotImplementedException();
        onHill.Invoke(value);
    }

    public delegate void OnConsumeMeat(float releasedDamage);

    public OnConsumeMeat onConsumeMeat;

    public void ConsumeMeat(float releasedDamage)
    {
        if (null == onConsumeMeat) throw new System.NotImplementedException();
        onConsumeMeat.Invoke(releasedDamage);
    }
}