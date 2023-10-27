using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;
//need a way to convert between real coords and relative coords



[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CustomPolygon : MonoBehaviour
{
    [SerializeField] private List<double[]> trail;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private TrailRenderer myTrail;
    [SerializeField] private GameObject myLocation;
    private GameRoom myGame;
    [SerializeField] private PlayerCollider myPlayerCollider;
    private void Start()
    {
        myGame = new GameRoom(0);
        myPlayerCollider.SetGameRoom(myGame);
        Debug.Log("ID: ");
        Debug.Log(myGame.lobbyID);
        CreateTriangle();
        Vector3 positionVec = myLocation.transform.position;
        myGame.addTriangleToMe(new float[] { positionVec.x+5, positionVec.z-5}, new float[] { positionVec.x+5, positionVec.z+5}, new float[] { positionVec.x-5, positionVec.z+5});
        renderMesh(myGame.allVertices.ToArray(), myGame.allTriangles);
        myGame.addTriangleToMe(new float[] { positionVec.x+5, positionVec.z-5}, new float[] { positionVec.x-5, positionVec.z-5}, new float[] { positionVec.x-5, positionVec.z+5});
        renderMesh(myGame.allVertices.ToArray(), myGame.allTriangles);
        trail = new List<double[]>();
    }

    public void Update()
    {
        if (myTrail.enabled) {
            Vector3 position = myLocation.transform.position;
            //y is vertical
            trail.Add(new double[] { position.x, position.z });
        } else if (trail.Count > 0) {
            Debug.Log("DRAWN");
            clearTheTrail();
        }
    }
    public void clearTheTrail() {
        float aCoordsA = (float) trail[0][0];
        float aCoordsB = (float) trail[0][1];
        float bCoordsA = (float) trail[trail.Count/2][0];
        float bCoordsB = (float) trail[trail.Count/2][1];
        float cCoordsA = (float) trail[trail.Count-1][0];
        float cCoordsB = (float) trail[trail.Count-1][1];
        myGame.addTriangleToMe(new float[] { aCoordsA, aCoordsB}, new float[] { bCoordsA, bCoordsB}, new float[] { cCoordsA, cCoordsB});
        renderMesh(myGame.allVertices.ToArray(), myGame.allTriangles);

    
        trail = new List<double[]>();
    }
    public void CreateTriangle()
    {
      renderMesh(myGame.allVertices.ToArray(), myGame.allTriangles);

    }


  
    private void renderMesh(Vector3[] vertices, List<int[]> triangles) {
        List<Material> materialsUsed = myGame.materialsUsed;

        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        
        Mesh mesh = new Mesh();


        mesh.vertices = vertices;
        mesh.subMeshCount = triangles.Count;

        // Assign triangles to submeshes
        for (int i = 0; i < triangles.Count; i++) {
            mesh.SetTriangles(triangles[i], i);
        }

        // Assign mesh to MeshFilter and materials to MeshRenderer
        meshFilter.mesh = mesh;
        meshRenderer.materials = materialsUsed.ToArray();
        meshCollider.sharedMesh = mesh;
    }

   
}
