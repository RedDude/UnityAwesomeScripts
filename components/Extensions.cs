using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Bounds OrthographicBounds(this Camera camera)
    {
        if (!camera.orthographic)
        {
            Debug.Log(string.Format("The camera {0} is not Orthographic!", camera.name), camera);
            return new Bounds();
        }

        var position = camera.transform.position;
        var x = position.x;
        var y = position.y;
        var size = camera.orthographicSize * 2;
        var width = size * (float)Screen.width / Screen.height;
        var height = size;

        return new Bounds(new Vector3(x, y, 0), new Vector3(width, height, 0));
    }

    public static Vector2 toVector2(this Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.y);
    }
}