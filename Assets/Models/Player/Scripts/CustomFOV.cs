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
        if (defaultFOV < 120) {
        defaultFOV = defaultFOV + 5;
        }
        _camera.fieldOfView = defaultFOV;  
    }
    public void ZoomIn()
    {
        if (defaultFOV > 20) {
        defaultFOV = defaultFOV - 5;
        }
        _camera.fieldOfView = defaultFOV; 
    }
}