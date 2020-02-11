using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raytracing : MonoBehaviour
{
    public Texture2D MainTexture;
    public int height = 512;
    public int width = 512;

    float LastInput = 0.0f;
    float input = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Render();
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), MainTexture);
    }


    private void Update()
    {
        LastInput = input;
        input = Input.GetAxisRaw("Jump");

        if(LastInput != 1.0f && input == 1.0f)
        {
            Render();
        }


    }

    void Render()
    {

        Debug.Log("Begin... + " + Time.realtimeSinceStartup);
        MainTexture = new Texture2D(height, width);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Ray camRay = Camera.main.ViewportPointToRay(new Vector3((float)x / width, (float)y / height, Camera.main.nearClipPlane));

                RaycastHit hitOutput;

                bool hit = Physics.Raycast(camRay, out hitOutput);

                if (hit)
                {
                    ShaderBase shader = hitOutput.transform.GetComponent<ShaderBase>();
                    if (shader)
                        MainTexture.SetPixel(x, y, shader.CalculateColor(hitOutput));
                    else
                        MainTexture.SetPixel(x, y, Color.black);

                }
                else
                {

                    MainTexture.SetPixel(x, y, Color.black);
                }


            }
        }
        MainTexture.Apply();
        Debug.Log("End... + " + Time.realtimeSinceStartup);
    }
}
