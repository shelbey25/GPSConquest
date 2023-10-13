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
    [SerializeField] private TrailRenderer myTrail;
    [SerializeField] private GameObject myLocation;
    private GameRoom myGame;
    private void Start()
    {
        myGame = new GameRoom(0);
        CreateTriangle();
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
        Debug.Log(aCoordsA);
        Debug.Log(aCoordsB);
        Debug.Log(bCoordsA);
        Debug.Log(bCoordsB);
        Debug.Log(cCoordsA);
        Debug.Log(cCoordsB);
 

        Material redMaterial = new Material(Shader.Find("Standard"));
redMaterial.color = Color.red;

Material blueMaterial = new Material(Shader.Find("Standard"));
blueMaterial.color = Color.blue;

        // Define the mesh filter and renderer
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        
        // Create a new mesh
        Mesh mesh = new Mesh();
        
        // Define vertices, triangles, and normals for the triangle
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(50, 1, -50), // Bottom-right
    new Vector3(-50, 1, -50), // Bottom-left
    new Vector3(-50, 1, 50),  // Top-left
    new Vector3(50, 1, 50),    // Top-right
    new Vector3(20, 1, 20), 
    new Vector3(aCoordsA, 1, aCoordsB),  // Top-left
    new Vector3(bCoordsA, 1, bCoordsB),    // Top-right
    new Vector3(cCoordsA, 1, cCoordsB)
        };

//to set the zones
mesh.subMeshCount = 2;

// First triangle (red)
    int[] trianglesRed = new int[] {  };
        int[] trianglesBlue = new int[] {  5, 6, 7 };

        // Set vertices and submesh count
        mesh.vertices = vertices;
        mesh.subMeshCount = 2;

        // Assign triangles to submeshes
        mesh.SetTriangles(trianglesRed, 0);
        mesh.SetTriangles(trianglesBlue, 1);

        // Assign mesh to MeshFilter and materials to MeshRenderer
        meshFilter.mesh = mesh;
        meshRenderer.materials = new Material[] { redMaterial, blueMaterial };

    
        trail = new List<double[]>();
    }
    public void CreateTriangle()
    {
       
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(50, 1, -50), // Bottom-right
    new Vector3(-50, 1, -50), // Bottom-left
    new Vector3(-50, 1, 50),  // Top-left
    new Vector3(50, 1, 50),    // Top-right
    new Vector3(20, 1, 20) 
        };


// First triangle (red)
    int[] trianglesRed = new int[] { 0, 1, 2, 2, 4, 0 };
        int[] trianglesBlue = new int[] { 2, 3, 4, 4, 3, 0 };
    List<int[]> triangles = new List<int[]> {trianglesRed, trianglesBlue};
    renderMesh(vertices, triangles);

    }


    public static Color RandomColor()
{
    System.Random randNumGen = new System.Random();
    int r = randNumGen.Next(0, 256);
    int g = randNumGen.Next(0, 256);
    int b = randNumGen.Next(0, 256);
    return new Color32((byte)r, (byte)g, (byte)b, 255); 
}

    private void renderMesh(Vector3[] vertices, List<int[]> triangles) {
        List<Material> materialsUsed = new List<Material>();
        for (int i = 0; i < triangles.Count; i++) {
            Material coloredMaterial = new Material(Shader.Find("Standard"));
            coloredMaterial.color = RandomColor();
            materialsUsed.Add(coloredMaterial);
        }
        

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collided with: {collision.gameObject.name}");
    }
}
