using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderBase : MonoBehaviour
{
    virtual public Color CalculateColor(RaycastHit hitInfo) { return Color.black; }
}
