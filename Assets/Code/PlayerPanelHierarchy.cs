using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelHierarchy : MonoBehaviour
{
    public Button attackButton;
    public Transform statsPanel;
    public Animator character;
    public GameObject statPrefab;

    private Player currentPlayer;
    private List<StatPanel> panels = new List<StatPanel>();

    internal void SetNewPlayer(Player currentPlayer, Data gameData)
    {
        this.currentPlayer = currentPlayer;
        ClearPanel();

        foreach (var stat in gameData.stats) {
            InsertStatPanel(stat.icon, () => currentPlayer.Stat(stat.id).ToString());
        }

        foreach (var buff in currentPlayer.buffs) {
            InsertStatPanel(buff.icon, () => buff.title);
        }

        UpdateStats();
    }

    public void UpdateStats() {
        character.SetInteger("Health", (int)Math.Ceiling(currentPlayer.Stat(0)));
        foreach (var s in panels) {
            s.Update();
        }
    }

    private void ClearPanel()
    {
        foreach (var p in panels) {
            p.Dispose();
        }
        panels.Clear();
    }

    class StatPanel : IDisposable
    {
        private GameObject statPanel;
        private TitleProducer title;

        public StatPanel(GameObject statPanel, string iconSrc, TitleProducer title)
        {
            this.statPanel = statPanel;
            this.title = title;

            statPanel.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(iconSrc);

            Update();
        }

        public void Update() {
            statPanel.transform.Find("Text").GetComponent<Text>().text = title.Invoke();
        }

        public void Dispose() {
            GameObject.Destroy(statPanel);
        }
    }

    private delegate String TitleProducer();
    private void InsertStatPanel(string iconSrc, TitleProducer title) {
        panels.Add(
            new StatPanel(
                Instantiate(statPrefab, statsPanel.transform),
                String.Format("Icons/{0}", iconSrc),
                title
            )
        );
    }
}
