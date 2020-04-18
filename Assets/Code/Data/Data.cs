using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Data
{
    public GameModel settings;
    public Stat[] stats;
    public Buff[] buffs;
}

[Serializable]
public class GameModel
{
    public int buffCountMin;
    public int buffCountMax;
    public bool allowDuplicateBuffs;

}

[Serializable]
public class Stat
{
    public int id;
    public string title;
    public string icon;
    public float value;
}

[Serializable]
public class BuffStat
{
    public float value;
    public int statId;
}

[Serializable]
public class Buff
{
    public string icon;
    public int id;
    public string title;
    public BuffStat[] stats;
}

static class BuffsExtensions {
    static public float Apply(IEnumerable<Buff> buffs, int statId, float baseValue) {
        return buffs.Aggregate(baseValue, (acc, b) => BuffsExtensions.Apply(b.stats, statId, acc));
    }

    static public float Apply(IEnumerable<BuffStat> stats, int statId, float baseValue) {
        return stats.Aggregate(baseValue, (acc, s) => BuffsExtensions.Apply(s, statId, acc));
    }

    static public float Apply(BuffStat stat, int statId, float baseValue)
    {
        return statId == stat.statId
            ? baseValue + stat.value
            : baseValue
            ;
    }
}