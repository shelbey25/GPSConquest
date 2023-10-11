using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;



[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CustomPolygon : MonoBehaviour
{
    [SerializeField] private List<double[]> trail;
    private void Start()
    {
        CreateTriangle();
        trail = new List<double[]>();
    }

    public void Update()
    {

    }
    public void clearTheTrail() {
        trail = new List<double[]>();
    }
    void CreateTriangle()
    {
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
    new Vector3(20, 1, 20) 
        };

//to set the zones
mesh.subMeshCount = 2;

// First triangle (red)
    int[] trianglesRed = new int[] { 0, 1, 2, 2, 4, 0 };
        int[] trianglesBlue = new int[] { 2, 3, 4, 4, 3, 0 };

        // Set vertices and submesh count
        mesh.vertices = vertices;
        mesh.subMeshCount = 2;

        // Assign triangles to submeshes
        mesh.SetTriangles(trianglesRed, 0);
        mesh.SetTriangles(trianglesBlue, 1);

        // Assign mesh to MeshFilter and materials to MeshRenderer
        meshFilter.mesh = mesh;
        meshRenderer.materials = new Material[] { redMaterial, blueMaterial };

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collided with: {collision.gameObject.name}");
    }
}
