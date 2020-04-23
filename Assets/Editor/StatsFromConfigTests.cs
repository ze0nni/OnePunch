using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    class StatsFromConfigTests
    {
        [Test]
        public void Test_values_froms_stats()
        {
            var stats = new StatsFromConfig(
                new Stat[] {
                        new Stat() { id = 0, value = 10 },
                        new Stat() { id = 1, value = 20 },
                        new Stat() { id = 2, value = 30 },
                        new Stat() { id = 3, value = 40 },
                },
                new Buff[] { }
            );

            Assert.AreEqual(10, stats.Get(0));
            Assert.AreEqual(20, stats.Get(1));
            Assert.AreEqual(30, stats.Get(2));
            Assert.AreEqual(40, stats.Get(3));
        }

        [Test]
        public void Test_values_froms_stats_and_bufss()
        {
            var stats = new StatsFromConfig(
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

            Assert.AreEqual(10, stats.Get(0));
            Assert.AreEqual(20, stats.Get(1));
            Assert.AreEqual(30, stats.Get(2));
            Assert.AreEqual(40, stats.Get(3));
        }
    }
}
