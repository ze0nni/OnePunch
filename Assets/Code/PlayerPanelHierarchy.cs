using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelHierarchy : MonoBehaviour
{
    public Button attackButton;
    public Transform statsPanel;
    public Animator character;

    private Player currentPlayer;

    internal void SetNewPlayer(Player currentPlayer)
    {
        this.currentPlayer = currentPlayer;
    }
}
