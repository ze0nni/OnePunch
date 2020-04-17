using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PlayerPanelHierarchy left;
    public PlayerPanelHierarchy right;

    private Data gameData;

    void Start()
    {
        this.gameData = JsonUtility.FromJson<Data>(Resources.Load<TextAsset>("data").text);
    }
}
