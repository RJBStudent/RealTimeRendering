using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialShader : ShaderBase
{
    public override Color CalculateColor(RaycastHit hitInfo)
    {
        Color fragColor;
        Material mat;
        mat = hitInfo.transform.GetComponent<Renderer>().sharedMaterial;
        if (mat.mainTexture == true)
        {
            fragColor = (mat.mainTexture as Texture2D).GetPixelBilinear(hitInfo.textureCoord.x, hitInfo.textureCoord.y);
        }
        else
        {
            fragColor = hitInfo.transform.GetComponent<Renderer>().sharedMaterial.color;
        }

        return fragColor;
    }

}
