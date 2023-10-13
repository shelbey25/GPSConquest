using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;

public class GameRoom
{
    public int localLobby;
    public int lobbyID;
    public static int globalLobbyCount = 0;
    public Vector3[] allVertices;
    public List<int[]> allTriangles;

    public GameRoom(int localLobbyParam) {
        localLobby = localLobbyParam;
        lobbyID = globalLobbyCount;
        globalLobbyCount = globalLobbyCount + 1;

    }
}


public class Player : MonoBehaviour
{
    [SerializeField] private int rank = 1;
    [SerializeField] private double percent = 0.021;
    [SerializeField] private int walkTotal = 54;
[SerializeField] private int level = 1;
[SerializeField] private GameObject achievement1;
[SerializeField] private GameObject achievement2;
[SerializeField] private GameObject achievement3;
[SerializeField] private GameObject achievement4;
[SerializeField] private GameObject achievement5;
[SerializeField] private GameObject gameRoom1;
[SerializeField] private GameObject gameRoom2;
[SerializeField] private GameObject gameRoom3;
[SerializeField] private GameObject menu;
private int gameRoomCount = 1;
private GameRoom activeGame;
[SerializeField] public TMP_InputField inputField;

private List<GameRoom> myGames = new List<GameRoom>();
    // Start is called before the first frame update
    void Start()
    {
        InitData();
        achievement1.SetActive((walkTotal >= 1));
        achievement2.SetActive((walkTotal >= 10));
        achievement3.SetActive((walkTotal >= 100));
        achievement4.SetActive((percent >= 0.01));
        achievement5.SetActive((percent >= 0.1));
        activeGame = null;
        gameRoom1.SetActive((myGames.Count >= 1));
        gameRoom2.SetActive((myGames.Count >= 2));
        gameRoom3.SetActive((myGames.Count >= 3));
    }

    public void Update() {
    inputField.text = inputField.text.ToUpper();

    }

    private void InitData() {
        level = walkTotal/10;
        GameRoom gameRoomA = new GameRoom(1);
        GameRoom gameRoomB = new GameRoom(2);
        myGames.Add(gameRoomA);
        myGames.Add(gameRoomB);
    }

    public void setActiveGame1() {
        activeGame = myGames[0];
        menu.SetActive(!menu.activeSelf);
    }
    public void setActiveGame2() {
        activeGame = myGames[1];
        menu.SetActive(!menu.activeSelf);
    }
    public void setActiveGame3() {
        activeGame = myGames[2];
        menu.SetActive(!menu.activeSelf);
    }

    public void newGame() {
        if (myGames.Count < 3) {
            GameRoom gameRoomFinal = new GameRoom(myGames.Count+1);
            myGames.Add(gameRoomFinal);
            gameRoom1.SetActive((myGames.Count >= 1));
        gameRoom2.SetActive((myGames.Count >= 2));
        gameRoom3.SetActive((myGames.Count >= 3));
            menu.SetActive(!menu.activeSelf);
            
        }
    }
    
}
