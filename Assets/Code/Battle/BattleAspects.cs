using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    // В идеале все методы нужно завернуть в Task, например:
    // Task<OnBeforeHitResult> OnBeforeHit(...)
    // Task OnDamageHappened(...)
    // В таком случае можно будет, например, сделать проверку на возможность уворота или контратаки

    // По мере развития, число методов BattleAspect будет увеличиваться. И у большинства аспектов методы будут иметь
    // пустую реализацию. Я созданельно избегаю создания `AbstractBattleAspect` от которого нужно будет наследоваться и переопределить
    // нужные методы. Практика показывает что что ни к чему хорошему это не приведет.

    // Я так же осознаю что изменение сигнатуры BattleAspect может повлечь множество точечных изменений во вех его реализация
    // и что так и должно быть. Если мы меняем контракт, убедиться что этот контракт везде исполняется правильно. "Безшумные" ошибки самые опасные.

    public interface BattleAspect
    {
        OnBeforeHitResult OnBeforeHit(BattleArea area, Fighter source, Fighter consumer, OnBeforeHitResult result);

        void OnHitHappened(BattleArea area, Fighter source, Fighter consumer, float damage);
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

    public class BattleAspects : BattleAspect
    {
        readonly BattleAspect[] aspects;

        public BattleAspects(params BattleAspect[] aspects)
        {
            this.aspects = aspects;
        }

        public OnBeforeHitResult OnBeforeHit(BattleArea area, Fighter source, Fighter consumer, OnBeforeHitResult result)
        {
            var damageResult = new OnBeforeHitResult(0, false);
            foreach (var a in aspects)
            {
                damageResult = a.OnBeforeHit(area, source, consumer, damageResult);
                if (damageResult.abort)
                {
                    return damageResult;
                }
            }
            return damageResult;
        }

        public void OnHitHappened(BattleArea area, Fighter source, Fighter consumer, float damage)
        {
            foreach (var a in aspects)
            {
                a.OnHitHappened(area, source, consumer, damage);
            }
        }
    }
}
