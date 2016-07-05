using UnityEngine;
using System.Collections;

public class GL : MonoBehaviour {

    //最重要データ
    public static int WorldLimit = 128;
    public static byte[,,] CubeWorld = new byte[WorldLimit*16, 64, WorldLimit*16];
    public static GameObject[,] BoxWorld = new GameObject[WorldLimit, WorldLimit];

    public static GameObject[] CubeSet = new GameObject[256];

    public static int Config_FovLimit = 16;
    public static int Config_Callback = 8;

    //public GameObject Cube0;
    //public GameObject Cube1;
    //public GameObject Cube2;
    //public GameObject Cube3;
    //public GameObject Cube4;
    //public GameObject Cube5;
    //public GameObject Cube6;
    //public GameObject Cube7;
    //public GameObject Cube8;
    //public GameObject Cube9;
    //public GameObject Cube10;
    //public GameObject Cube11;
    //public GameObject Cube12;
    //public GameObject Cube13;
    //public GameObject Cube14;

    // Use this for initialization
    void Start () {

        //GL.CubeSet[0] = Cube0;
        //GL.CubeSet[1] = Cube1;
        //GL.CubeSet[2] = Cube2;
        //GL.CubeSet[3] = Cube3;
        //GL.CubeSet[4] = Cube4;
        //GL.CubeSet[5] = Cube5;
        //GL.CubeSet[6] = Cube6;
        //GL.CubeSet[7] = Cube7;
        //GL.CubeSet[8] = Cube8;
        //GL.CubeSet[9] = Cube9;
        //GL.CubeSet[10] = Cube10;
        //GL.CubeSet[11] = Cube11;
        //GL.CubeSet[12] = Cube12;
        //GL.CubeSet[13] = Cube13;
        //GL.CubeSet[14] = Cube14;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
