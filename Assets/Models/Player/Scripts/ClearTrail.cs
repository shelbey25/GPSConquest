using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEditor;

public class ClearTrail : MonoBehaviour
{
    public void Awake() {

    }
    public void Start() {

    }
    public void clearTheTrail() {
        GetComponent<TrailRenderer>().Clear();
        GetComponent<TrailRenderer>().enabled = !GetComponent<TrailRenderer>().enabled;
        Debug.Log("Called Clear Trail");
    }
}
