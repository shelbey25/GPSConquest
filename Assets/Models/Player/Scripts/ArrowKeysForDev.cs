//Whole File written by GPT as just DEV Tools



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class ArrowKeysForDev : MonoBehaviour
{
    public AbstractMap map;
    public float speed = 0.00001f;

    void Update()
    {
        Vector2d move = Vector2d.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            move.y += speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move.y -= speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x -= speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move.x += speed;
        }

        Vector2d newLatLong = map.CenterLatitudeLongitude + move;
        map.UpdateMap(newLatLong, map.Zoom);
    }
}