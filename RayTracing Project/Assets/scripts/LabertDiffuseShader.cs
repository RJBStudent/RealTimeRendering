using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabertDiffuseShader : ShaderBase
{
    [SerializeField] Light light;

    public override Color CalculateColor(RaycastHit hitInfo)
    {

        //--------
        Mesh mesh = (hitInfo.collider as MeshCollider).sharedMesh;
        Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;
        Vector3 n0 = normals[triangles[hitInfo.triangleIndex * 3 + 0]];
        Vector3 n1 = normals[triangles[hitInfo.triangleIndex * 3 + 1]];
        Vector3 n2 = normals[triangles[hitInfo.triangleIndex * 3 + 2]];
        Vector3 baryCenter = hitInfo.barycentricCoordinate;
        Vector3 interpolatedNormal = n0 * baryCenter.x + n1 * baryCenter.y + n2 * baryCenter.z;
        interpolatedNormal = interpolatedNormal.normalized;
        //--------

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


        Vector3 distToLight = Vector3.zero;
        float dot = 0.0f;
        float insideRange = 0.0f;
        switch (light.type)
        {
            case LightType.Directional:
                {
                    distToLight = -light.transform.forward;
                    dot = Vector3.Dot(distToLight, interpolatedNormal);
                }
                break;
            case LightType.Point:
                {
                    Vector3 dist = light.transform.position - hitInfo.point;
                    dot = Vector3.Dot(dist.normalized, interpolatedNormal);
                    if(dist.magnitude <= light.range)
                    {
                        insideRange = (dist.magnitude * dist.magnitude) / (light.range * light.range);
                        insideRange = 1.0f - insideRange;
                  }
                    distToLight = dist.normalized;
                }
                break;
        }

        RaycastHit shadowHitOut;

        float inShadow = 0.0f;
        bool hit = Physics.Raycast(hitInfo.point, (distToLight).normalized);
        if (hit)
        {
            inShadow = 1.0f;
        }
        
        
        //float dot = Vector3.Dot(hitInfo.normal, (light.transform.position - hitInfo.point).normalized);
        //dot = Mathf.Abs(dot);
        fragColor.r = fragColor.r * light.intensity*light.color.r*  insideRange *(1.0f - inShadow) *Mathf.Max(dot, 0.0f);
        fragColor.g = fragColor.g * light.intensity* light.color.g *insideRange *(1.0f - inShadow) * Mathf.Max(dot, 0.0f);
        fragColor.b = fragColor.b * light.intensity* light.color.b *insideRange *(1.0f - inShadow) * Mathf.Max(dot, 0.0f);
        fragColor.a = 1.0f;
        

       
        

        return fragColor;
    }

}
