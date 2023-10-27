using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;
public class PlayerCollider : MonoBehaviour
{

    [SerializeField] private TrailRenderer myTrail;
    private GameRoom myGame;
    [SerializeField] private GameObject myPin;
    private Vector3 savedCoords;
    
    public void SetGameRoom(GameRoom room)
    {
        myGame = room;
    }

    void Start() {
        Debug.Log("HI");
    }

    void Update() {
        savedCoords = myPin.transform.position;
    }

    //CHATGPT
    bool IsPointInTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
{
    // Compute vectors
    Vector3 v0 = c - a;
    Vector3 v1 = b - a;
    Vector3 v2 = p - a;

    // Compute dot products
    float dot00 = Vector3.Dot(v0, v0);
    float dot01 = Vector3.Dot(v0, v1);
    float dot02 = Vector3.Dot(v0, v2);
    float dot11 = Vector3.Dot(v1, v1);
    float dot12 = Vector3.Dot(v1, v2);

    // Compute barycentric coordinates
    float invDenom = 1.0f / (dot00 * dot11 - dot01 * dot01);
    float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
    float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

    // Check if point is in triangle
    return (u >= 0) && (v >= 0) && (u + v <= 1);
}

private int checker(Collider other, bool typeOfOpp) {
    int checker = -1;

    if ((other.GetType() == typeof(MeshCollider)) && (myGame != null)) {
        MeshCollider altCol = (MeshCollider)other;
        MeshRenderer meshRenderer = GetComponent<Collider>().gameObject.GetComponent<MeshRenderer>();
        List<int[]> tris = myGame.allTriangles;
        List<Vector3> verts = myGame.allVertices;
        Vector3 positionVec = myPin.transform.position;
        if (typeOfOpp) {
            positionVec = myPin.transform.position;
        } else {
            positionVec = savedCoords;
        }
        for (int i = 0; i < tris.Count; i++) {
            for (int z = 0; z < tris[i].Length; z = z + 3) {
            if (IsPointInTriangle(new Vector3(positionVec.x, 0, positionVec.z), new Vector3(verts[tris[i][z]].x, 0, verts[tris[i][z]].z), new Vector3(verts[tris[i][z+1]].x, 0, verts[tris[i][z+1]].z), new Vector3(verts[tris[i][z+2]].x, 0, verts[tris[i][z+2]].z))) {
                Debug.Log("!!!!!!!!!!!!!!!!");
                Debug.Log(checker);
                Debug.Log(i);
                checker = i;
                Debug.Log(checker);
            }
            }
        }
        //Debug.Log(meshRenderer.materials[0].color);
    }
    Debug.Log("Return");
    Debug.Log(checker);
    return checker;
}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.material.color);
        

        if (checker(other, true) == 0) {
        myTrail.Clear();
        myTrail.enabled = false;
        }
 }


    void OnTriggerExit(Collider other)
    {
        //NEED TO STORE PREVIOUS POSITION AS WORKS AFTER OUT
        Debug.Log(checker(other, false));
        if (checker(other, false) == 0) {
        myTrail.Clear();
        myTrail.enabled = true;
        }
    }
}