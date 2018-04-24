using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour {

    //private List<Transform> cubes = new List<Transform>();
    private Transform[,] cubes = new Transform[20, 20];
    public Transform cube;
    private int sampleSize = 10;
    private int region = 0;
    private float distanceRatio = 0f;

	// Use this for initialization
	void Start () {
		for(int x = 0; x < 20; x++)
        {
            for(int z = 0; z < 20; z++)
            {
                cubes[x,z] = Instantiate(cube, new Vector3(((float)x + (float)x * distanceRatio), 0, (float)z + (float)z * distanceRatio), Quaternion.identity);
                cubes[x, z].gameObject.GetComponent<CubeCreator>().pos = new Vector2((float)x, (float)z);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
