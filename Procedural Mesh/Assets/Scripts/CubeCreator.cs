using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class CubeCreator : MonoBehaviour
{
    public Vector3 size = Vector3.one;
    public Vector2 pos = new Vector2(0,0);
    private float timer = 0;
    private float timeMod = .5f;
    private float modifier = 1f;
    private float posMod = 10f;



    private void FixedUpdate()
    {
        timer += Time.deltaTime * timeMod;

        if(timer > 255)
        {
            timer = 0;
        }
        

        MeshFilter meshFilter = this.GetComponent<MeshFilter>();    // one submesh for each face 
        MeshCreator mc = new MeshCreator();

        Vector3 cubeSize = size * 0.5f;

        float noiseValueT0 = Perlin.Noise((pos.x + cubeSize.x) / posMod, (pos.y - cubeSize.z) / posMod, timer);
        float noiseValueT1 = Perlin.Noise((pos.x - cubeSize.x) / posMod, (pos.y - cubeSize.z) / posMod, timer);
        float noiseValueT2 = Perlin.Noise((pos.x - cubeSize.x) / posMod, (pos.y + cubeSize.z) / posMod, timer);
        float noiseValueT3 = Perlin.Noise((pos.x + cubeSize.x) / posMod, (pos.y + cubeSize.z) / posMod, timer);
        //float noiseValueT0 = Perlin.Noise((pos.x + cubeSize.x) * posMod, (pos.y - cubeSize.z) * posMod, timer);
        //float noiseValueT1 = Perlin.Noise((pos.x - cubeSize.x) * posMod, (pos.y - cubeSize.z) * posMod, timer);
        //float noiseValueT2 = Perlin.Noise((pos.x - cubeSize.x) * posMod, (pos.y + cubeSize.z) * posMod, timer);
        //float noiseValueT3 = Perlin.Noise((pos.x + cubeSize.x) * posMod, (pos.y + cubeSize.z) * posMod, timer);

        // top of the cube        
        // t0 is top left point        
        Vector3 t0 = new Vector3(cubeSize.x, cubeSize.y + modifier * noiseValueT0, -cubeSize.z);
        Vector3 t1 = new Vector3(-cubeSize.x, cubeSize.y + modifier * noiseValueT1, -cubeSize.z);
        Vector3 t2 = new Vector3(-cubeSize.x, cubeSize.y + modifier * noiseValueT2, cubeSize.z);
        Vector3 t3 = new Vector3(cubeSize.x, cubeSize.y + modifier * noiseValueT3, cubeSize.z);

        // bottom of the cube       
        Vector3 b0 = new Vector3(cubeSize.x, -cubeSize.y, -cubeSize.z);
        Vector3 b1 = new Vector3(-cubeSize.x, -cubeSize.y, -cubeSize.z);
        Vector3 b2 = new Vector3(-cubeSize.x, -cubeSize.y, cubeSize.z);
        Vector3 b3 = new Vector3(cubeSize.x, -cubeSize.y, cubeSize.z);

        // Top square        
        mc.BuildTriangle(t0, t1, t2);
        mc.BuildTriangle(t0, t2, t3);

        // Bottom square        
        mc.BuildTriangle(b2, b1, b0);
        mc.BuildTriangle(b3, b2, b0);

        // Back square        
        mc.BuildTriangle(b0, t1, t0);
        mc.BuildTriangle(b0, b1, t1);

        mc.BuildTriangle(b1, t2, t1);
        mc.BuildTriangle(b1, b2, t2);

        mc.BuildTriangle(b2, t3, t2);
        mc.BuildTriangle(b2, b3, t3);

        mc.BuildTriangle(b3, t0, t3);
        mc.BuildTriangle(b3, b0, t0);

        meshFilter.mesh = mc.CreateMesh();
    }
}
