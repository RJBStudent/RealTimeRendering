using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShader : ShaderBase
{
    // Start is called before the first frame update
    public override Color CalculateColor(RaycastHit hitInfo)
    {
        Color fragColor;
        Vector2 circle = new Vector2(0.5f, 0.5f);
        float radius = 0.25f;

        if((hitInfo.textureCoord - circle).magnitude < radius)
        {
            fragColor = Color.red;
        }
        else
        {
            fragColor = hitInfo.transform.GetComponent<Renderer>().sharedMaterial.color;
        }

        return fragColor;
    }
}
