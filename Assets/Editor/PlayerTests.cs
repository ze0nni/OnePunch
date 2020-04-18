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
}