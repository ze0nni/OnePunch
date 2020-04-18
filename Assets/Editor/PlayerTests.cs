using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PlayerTests
{
    [Test]
    public void Init_player_with_stats() {
        var player = new Player(
            new Stat[] {
                new Stat() { id = 0, value = 10 },
                new Stat() { id = 1, value = 20 },
                new Stat() { id = 2, value = 30 },
                new Stat() { id = 3, value = 40 },
            },
            new Buff[] { }
        );

        Assert.AreEqual(10, player.Stat(0));
        Assert.AreEqual(20, player.Stat(1));
        Assert.AreEqual(30, player.Stat(2));
        Assert.AreEqual(40, player.Stat(3));
    }

    [Test]
    public void Init_player_with_buffs() {
        var player = new Player(
            new Stat[] {
                new Stat() { id = 0, value = 20 },
                new Stat() { id = 1, value = 10 },
                new Stat() { id = 2, value = 10 },
                new Stat() { id = 3, value = 10 },
            },
            new Buff[] {
                new Buff() {
                    stats = new BuffStat[] {
                        new BuffStat() { statId = 0, value = -10 }
                    }
                },
                new Buff() {
                    stats = new BuffStat[] {
                        new BuffStat() { statId = 1, value = 10 },
                        new BuffStat() { statId = 2, value = 20 }
                    }
                },
                new Buff() {
                    stats = new BuffStat[] {
                        new BuffStat() { statId = 3, value = 30 }
                    }
                }
            }
        );

        Assert.AreEqual(10, player.Stat(0));
        Assert.AreEqual(20, player.Stat(1));
        Assert.AreEqual(30, player.Stat(2));
        Assert.AreEqual(40, player.Stat(3));
    }

    [Test]
    public void decrement_stat_0_on_hit() {
        var player = new Player(
            new Stat[] {
                    new Stat() { id = 0, value = 10 },
            },
            new Buff[] { }
        );

        player.Hit(5, out var _);

        Assert.AreEqual(5, player.Stat(0));
    }

    [Test]
    public void player_damage_from_stat_2() {
        var player = new Player(
            new Stat[] {
                    new Stat() { id = 2, value = 10 },
            },
            new Buff[] {
                new Buff() {
                    stats = new BuffStat[] {
                        new BuffStat() { statId = 2, value = 5}
                    }
                }
            }
        );

        Assert.AreEqual(15, player.Damage());
    }

    [Test]
    public void absorb_damage_with_stat_1()
    {
        var player = new Player(
            new Stat[] {
                    new Stat() { id = 0, value = 10 },
                    new Stat() { id = 1, value = 20 },
            },
            new Buff[] { }
        );

        player.Hit(5, out var releasedDamage);

        Assert.AreEqual(4, releasedDamage);
        Assert.AreEqual(6, player.Stat(0));
    }

    [Test]
    public void hill_using_stat_3() {
        var player = new Player(
            new Stat[] {
                    new Stat() { id = 0, value = 10 },
                    new Stat() { id = 3, value = 80 },
            },
            new Buff[] { }
        );

        player.ConsumeMeat(10);

        Assert.AreEqual(18, player.Stat(0));
    }

    [Test]
    public void isAlive_changed_on_hit() {
        var player = new Player(
            new Stat[] {
                    new Stat() { id = 0, value = 10 },
            },
            new Buff[] { }
        );

        Assert.IsTrue(player.IsAlive());

        player.Hit(5, out var _);
        Assert.IsTrue(player.IsAlive());

        player.Hit(5, out var _);
        Assert.IsFalse(player.IsAlive());
    }
}