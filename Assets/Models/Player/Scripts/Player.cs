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
    public List<Vector3> allVertices;
    public List<int[]> allTriangles;
    public List<Player> players;
    public List<Material> materialsUsed;

    public GameRoom(int localLobbyParam) {
        localLobby = localLobbyParam;
        lobbyID = globalLobbyCount;
        globalLobbyCount = globalLobbyCount + 1;


        

        allVertices = new List<Vector3>
        {
            new Vector3(50, 1, -50), // Bottom-right
            new Vector3(-50, 1, -50), // Bottom-left
            new Vector3(-50, 1, 50),  // Top-left
            new Vector3(50, 1, 50),    
            new Vector3(20, 1, 20), // Bottom-left
            new Vector3(-50, 1, -50),  // Top-left
            new Vector3(-50, 1, -20),
            new Vector3(-40, 1, -20)
        };
         allTriangles = new List<int[]> {new int[] { 5, 6, 7 }, new int[] { 2, 3, 4, 4, 3, 0 }};
            
        materialsUsed = new List<Material>();
        for (int i = 0; i < allTriangles.Count; i++) {
            Material coloredMaterial = new Material(Shader.Find("Standard"));
            coloredMaterial.color = RandomColor();
            materialsUsed.Add(coloredMaterial);
        }
    }

      public static Color RandomColor()
{
    System.Random randNumGen = new System.Random();
    int r = randNumGen.Next(0, 256);
    int g = randNumGen.Next(0, 256);
    int b = randNumGen.Next(0, 256);
    return new Color32((byte)r, (byte)g, (byte)b, 255); 
}


    private float[] calcCentroid(List<float[]> vertices) {
        float centerCoordX = 0.0f;
        float centerCoordY = 0.0f;
        for (int i = 0; i < vertices.Count; i++) {
            centerCoordX = centerCoordX + vertices[i][0];
            centerCoordY = centerCoordY + vertices[i][1];
        }
        return new float[] {centerCoordX/vertices.Count, centerCoordY/vertices.Count};
    }

    private List<float[]> orderClockwise(List<float[]> vertices) {
        float[] center = calcCentroid(vertices);
        vertices.Sort((v2, v1) =>
        {
            float angle1 = Mathf.Atan2(v1[1] - center[1], v1[0] - center[0]); //GPT
            float angle2 = Mathf.Atan2(v2[1] - center[1], v2[0] - center[0]); //GPT
            return angle1.CompareTo(angle2); //GPT
        });
        return vertices;
    }

    public void addTriangleToMe(float[] a, float[] b, float[] c) {
        int lengthVert = allVertices.Count;
        //Only ceratin movement patterns work, why?

        List<float[]> myVertices = orderClockwise(new List<float[]> {a, b, c});

        allVertices.Add(new Vector3(myVertices[0][0], 1, myVertices[0][1]));
        allVertices.Add(new Vector3(myVertices[1][0], 1, myVertices[1][1]));
        allVertices.Add(new Vector3(myVertices[2][0], 1, myVertices[2][1]));


        int[] tempArr = new int[allTriangles[0].Length+3];
        for (int i = 0; i < tempArr.Length; i++) {
            if (i < allTriangles[0].Length) {
                tempArr[i] = allTriangles[0][i];
            } else {
                if (i == allTriangles[0].Length) {
                    tempArr[i] = lengthVert;
                }
                if (i == allTriangles[0].Length+1) {
                    tempArr[i] = lengthVert+1;
                }
                if (i == allTriangles[0].Length+2) {
                    tempArr[i] = lengthVert+2;
                }
            }
        }
        allTriangles[0] = tempArr;
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
