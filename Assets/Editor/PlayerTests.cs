using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{

    class PlayerTests
    {
        [Test]
        public void InitPlayerWithStats()
        {
            var player = new Player(
                0,
                new TestStats().Set(0, 10).Set(1, 55).Set(2, 1)
            );

            Assert.AreEqual(10, player.Stat(0));
            Assert.AreEqual(55, player.Stat(1));
            Assert.AreEqual(1, player.Stat(2));
        }

        [Test]
        public void InitHealthFromStat()
        {
            var player = new Player(
                3,
                new TestStats().Set(3, 12)
            );

            Assert.AreEqual(12, player.Health);
        }

        [Test]
        public void UpdateHealthOnHit() {
            var player = new Player(
                3,
                new TestStats().Set(3, 12)
            );

            player.Hit(4);

            Assert.AreEqual(8, player.Health);
        }

        [Test]
        public void DoNotAcceptHealthLessThanZero()
        {
            var player = new Player(
                3,
                new TestStats().Set(3, 12)
            );

            player.Hit(30);

            Assert.AreEqual(0, player.Health);
        }

        [Test]
        public void NoAcceptNegativeDamage() {
            var player = new Player(
                3,
                new TestStats().Set(3, 12)
            );

            player.Hit(-30);

            Assert.AreEqual(12, player.Health);
        }

        [Test]
        public void UpdateHealthWhenHill() {
            var player = new Player(
                3,
                new TestStats().Set(3, 12)
            );

            player.Hill(2);

            Assert.AreEqual(14, player.Health);
        }

        [Test]
        public void NoAcceptNegativeHilling()
        {
            var player = new Player(
                3,
                new TestStats().Set(3, 12)
            );

            player.Hill(-2);

            Assert.AreEqual(12, player.Health);
        }

        private class TestStats : Stats
        {
            readonly Dictionary<int, float> stats = new Dictionary<int, float>();

            public float Get(int statId)
            {
                return this.stats[statId];
            }

            public TestStats Set(int statId, float value)  {
                this.stats[statId] = value;

                return this;
            }
        }
    }
}