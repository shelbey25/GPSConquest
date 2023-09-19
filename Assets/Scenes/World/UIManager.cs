using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class Rank
{
    public string name;
    public int percent;

    public Rank(string n, int r) {
        name = n;
        percent = r;
    }
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<Rank> rankings = new List<Rank>();
    [SerializeField] private GameObject menu;
    private void Awake() {
        Assert.IsNotNull(rankings);
    }
    public void Start() {
        Rank me = new Rank("Shelbe", 5);
        Rank user1 = new Rank("User1", 1);
        Rank user2 = new Rank("User2", 2);
        Rank user3 = new Rank("User3", 3);
        Rank user4 = new Rank("User4", 4);
        Rank user6 = new Rank("User6", 6);
        rankings.Add(me);
        rankings.Add(user1);
        rankings.Add(user2);
        rankings.Add(user3);
        rankings.Add(user4);
        rankings.Add(user6);
        rankings.Sort((x, y) => x.percent - y.percent);
    }
    public void updatePercent(string whom, int newPercent) {
        for (int i = 0; i < rankings.Count; i++) {
            if (rankings[i].name == (whom)) {
                rankings[i].percent = newPercent;
            }
        }
        rankings.Sort((x, y) => x.percent - y.percent);
    }
    public void toggleMenu() {
        menu.SetActive(!menu.activeSelf);
    }
}
