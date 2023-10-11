using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;

public class Rank
{
    public string name;
    public double percent;

    public Rank(string n, double r) {
        name = n;
        percent = r;
    }
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<Rank> rankings = new List<Rank>();
    [SerializeField] private GameObject menu;

    [SerializeField] public TMP_Text rankOneName;
    [SerializeField] public TMP_Text rankTwoName;
    [SerializeField] public TMP_Text rankThreeName;
    [SerializeField] public TMP_Text rankFourName;
    [SerializeField] public TMP_Text rankFiveName;
    [SerializeField] public TMP_Text rankSixName;
    [SerializeField] public TMP_Text localRank;
    public Rank me;
    private void Awake() {
        Assert.IsNotNull(rankings);
    }
    public void Start() {
        me = new Rank("Shelbe", 0.41);
        Rank user1 = new Rank("User1", 1);
        Rank user2 = new Rank("User2", 0.5);
        Rank user3 = new Rank("User3", 0.4);
        Rank user4 = new Rank("User4", 0.3);
        Rank user6 = new Rank("User6", 0.1);
        rankings.Add(me);
        rankings.Add(user1);
        rankings.Add(user2);
        rankings.Add(user3);
        rankings.Add(user4);
        rankings.Add(user6);
        Debug.Log(rankings);
        Debug.Log(rankings[0].name);
        rankings.Sort((x, y) => {
            if (y.percent > x.percent) return 1;
            if (y.percent < x.percent) return -1;
            return 0;
        });
        Debug.Log(rankings);
        Debug.Log(rankings[0].name);
    }

    public void Update() {
        rankOneName.text = rankings[0].name;
        rankTwoName.text = rankings[1].name;
        rankThreeName.text = rankings[2].name;
        int myRank = 0;
        for (int i = 0; i < rankings.Count; i++) {
            if (rankings[i].name == (me.name)) {
                myRank = i;
            }
        }
        try {
        rankFourName.text = rankings[myRank-1].name;
        } catch (ArgumentOutOfRangeException e) {
            
        }
        rankFiveName.text = me.name;
        try {
        rankSixName.text = rankings[myRank+1].name;
        } catch (ArgumentOutOfRangeException e) {
            
        }
        localRank.text = "  " + ((myRank - 1) > 0 ? (myRank) + ". \n" : "\n") + "  " + (myRank + 1) + ". \n" + "  " + (myRank + 2) + ". \n";
    }
    public void updatePercent(string whom, int newPercent) {
        for (int i = 0; i < rankings.Count; i++) {
            if (rankings[i].name == (whom)) {
                rankings[i].percent = newPercent;
            }
        }
        rankings.Sort((x, y) => {
            if (y.percent > x.percent) return 1;
            if (y.percent < x.percent) return -1;
            return 0;
        });
    }
    public void toggleMenu() {
        menu.SetActive(!menu.activeSelf);
    }
    public string getFirstPlace() {
        return rankings[0].name;
    }

}
