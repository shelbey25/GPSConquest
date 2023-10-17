using UnityEngine;


public class CustomFOV : MonoBehaviour {
    [SerializeField] private Camera _camera;
    private float defaultFOV;
private void Start()
    {
        defaultFOV = 60.0f;
        _camera.fieldOfView = defaultFOV;
    }
    public void ZoomOut()
    {
        _camera.fieldOfView = defaultFOV+5.0f;  
    }
    public void ZoomIn()
    {
        _camera.fieldOfView = defaultFOV-5.0f; 
    }
}