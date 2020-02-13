using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeShaderScript : MonoBehaviour
{

  public ComputeShader Shader;
  public string kernelName = "CSMain";
  public RenderTexture RenderTex;

  // Use this for initialization

  private int _kernel;
  private RenderTexture _renderTex;
    void Awake()
    {
            _kernel = Shader.FindKernel(kernelName);
        
            //_renderTex = new RenderTexture (512, 512, 24);
            _renderTex = RenderTex;
            _renderTex.enableRandomWrite = true;
            _renderTex.Create ();
        
            ExecComputeShader();

        RunShader();
    }


    public void ExecComputeShader()
    {

        Shader.SetTexture(_kernel, "Result", _renderTex);
        Shader.SetInt("TimeValue", (int)Time.timeSinceLevelLoad * 100);
        Shader.Dispatch(_kernel, 512 / 8, 512 / 8, 1);
    }

    struct VecMatPair
    {
        public Vector3 point;
        public Matrix4x4 matrix;
    }
    public ComputeShader shader;

    void RunShader()
    {
        VecMatPair[] data = new VecMatPair[5];
        VecMatPair[] output = new VecMatPair[5];

        //INITIALIZE DATA HERE
        data[0].point = new Vector3(1, 1, 1);
        data[0].matrix = new Matrix4x4(Vector4.one, Vector4.one, Vector4.one, Vector4.one);


        ComputeBuffer buffer = new ComputeBuffer(data.Length, 76);
        buffer.SetData(data);
        int kernel = shader.FindKernel("Multiply");
        shader.SetBuffer(kernel, "dataBuffer", buffer);
        shader.Dispatch(kernel, data.Length, 1, 1);
        buffer.GetData(output);
        Debug.Log(output[0].point);
    }

}
