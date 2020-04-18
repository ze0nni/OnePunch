using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PlayerPanelHierarchy leftPanel;
    public PlayerPanelHierarchy rightPanel;

    private Data gameData;

    readonly private BattleArea battleArea = new BattleArea();
    private Player leftPlayer;
    private Player rightPlayer;

    void Start()
    {
        this.gameData = JsonUtility.FromJson<Data>(Resources.Load<TextAsset>("data").text);

        StartWithBuffs();
    }

    private Player NewPlayer(bool withBuff) {
        return new Player(
            gameData.stats,
            gameData.buffs.Where(_ => withBuff && Random.value > 0.5f).ToArray()
        );
    }

    private void StartBattle(
        Player leftPlayer,
        Player rightPlayer
    ) {
        this.leftPlayer = leftPlayer;
        this.rightPlayer = rightPlayer;

        this.leftPanel.SetNewPlayer(leftPlayer, gameData);
        this.rightPanel.SetNewPlayer(rightPlayer, gameData);
    }

    public void StartWithBuffs() {
        StartBattle(
            NewPlayer(true),
            NewPlayer(true)
        );
    }

    public void StartWithoutBuffs()
    {
        StartBattle(
            NewPlayer(false),
            NewPlayer(false)
        );
    }

    private void PerformAttack(Player source, Player consumer) {
        if (!source.IsAlive() || !consumer.IsAlive()) {
            return;
        }

        battleArea.Attack(source, consumer);

        this.leftPanel.UpdateStats();
        this.rightPanel.UpdateStats();
    }

    public void PerformLeftPlayerAttack() {
        PerformAttack(this.leftPlayer, this.rightPlayer);
    }

    public void PerformRightPlayerAttack()
    {
        PerformAttack(this.rightPlayer, this.leftPlayer);
    }
}
