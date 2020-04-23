using Battle;
using Battle.Aspects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PlayerPanelHierarchy leftPanel;
    public PlayerPanelHierarchy rightPanel;

    private PlayerFactory playerFactory;
    private BattleArea battleArea;

    void Start()
    {
        #region Всю эту инициализацию можно в будущем заменить на DI-контейнер

        var gameData = JsonUtility.FromJson<Data>(Resources.Load<TextAsset>("data").text);

        // Здоровье персонажа берется из стата #0
        this.playerFactory = new CommonPlayerFactory(0, StatsFactoryFromConfig.Of(gameData));
        this.battleArea = new CommonBattleArea(new BattleAspects(
            // Аспект считает нанесенный урон из стата #2
            new IncrementDamageByStatValue(2),
            // Аспект считает "защиту" исходя из стата #1
            new AbsorbDamageByStatValue(1),
            // Аспект для вампиризма, исходя из стата 3
            new RestoreHealthFromDamageByStatValue(3)
        ));
        #endregion

        StartWithoutBuffs();
    }

    private void StartBattle(
        Player leftPlayer,
        Player rightPlayer
    )
    {
        this.leftPanel.SetNewPlayer(leftPlayer);
        this.rightPanel.SetNewPlayer(rightPlayer);
    }

    public void StartWithBuffs() {
        StartBattle(
            playerFactory.Produce(true),
            playerFactory.Produce(true)
        );
    }

    public void StartWithoutBuffs()
    {
        StartBattle(
            playerFactory.Produce(false),
            playerFactory.Produce(false)
        );
    }

    private void PerformAttack(PlayerPanelHierarchy sourcePanel, PlayerPanelHierarchy consumerPanel) {
        var source = sourcePanel.currentPlayer;
        var consumer = consumerPanel.currentPlayer;

        if (source.Health <= 0  || consumer.Health <= 0) {
            return;
        }
    
        sourcePanel.character.SetTrigger("Attack");
        battleArea.Attack(source, consumer);

        // Вызов анимации и UpdateStats в идеале нужно сделать реактивными биндингами
        // Что бы статы рассылали уведомелния при изменении
        // хотя у обоих подходов есть плюсы и минусы
        sourcePanel.UpdateStats();
        consumerPanel.UpdateStats();
    }

    public void PerformLeftPlayerAttack() {
        PerformAttack(leftPanel, rightPanel);
    }

    public void PerformRightPlayerAttack()
    {
        PerformAttack(rightPanel, leftPanel);
    }
}
