using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class BattleAreaTests
{
    [Test]
    public void Test_hit_with_damage() {
        var area = new BattleArea();

        var source = new TestFighter();
        source.onDamage = () => 10;
        source.onConsumeMeat = (_) => { };

        List<float> damages = new List<float>();
        var consumer = new TestFighter();
        consumer.onHit = (float damage, out float released) =>
        {
            damages.Add(damage);
            released = 0;
        };

        area.Attack(source, consumer);

        Assert.AreEqual(
            new float[] { 10 },
            damages
        );
    }

    [Test]
    public void Test_hill_with_released_damage() {
        var area = new BattleArea();

        List<float> meats = new List<float>();
        var source = new TestFighter();
        source.onDamage = () => 5;
        source.onConsumeMeat = (releasedDamage) =>
        {
            meats.Add(releasedDamage);
        };


        var consumer = new TestFighter();
        consumer.onHit = (float damage, out float released) =>
        {
            released = damage;
        };

        area.Attack(source, consumer);

        Assert.AreEqual(
            new float[] { 5 },
            meats
        );
    }

    [Test]
    public void Test_hill_50_percent_of_damage() {
        var area = new BattleArea();

        List<float> meats = new List<float>();
        var source = new TestFighter();
        source.onDamage = () => 5;
        source.onConsumeMeat = (releasedDamage) =>
        {
            meats.Add(releasedDamage);
        };


        var consumer = new TestFighter();
        consumer.onHit = (float damage, out float released) =>
        {
            released = damage / 2;
        };

        area.Attack(source, consumer);

        Assert.AreEqual(
            new float[] { 2.5f },
            meats
        );
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

    public delegate void OnHit(float damage, out float releasedDamage);

    public OnHit onHit;

    public void Hit(float damage, out float releasedDamage)
    {
        if (null == onHit) throw new System.NotImplementedException();
        onHit.Invoke(damage, out releasedDamage);
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