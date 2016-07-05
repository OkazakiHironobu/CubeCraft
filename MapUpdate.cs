using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MapUpdate : MonoBehaviour {

    GameObject camera;

    public GameObject CubeBox;

	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Main Camera");
	}

    float dt = 0.0f;
    float timer = 0.0f;

    #region //FPS測定用
    private float oldTime;
    private int frame = 0;
    private float frameRate = 0f;
    private const float INTERVAL = 0.5f; // この時間おきにFPSを計算して表示させる    
    #endregion

    void Update () {
        dt = Time.deltaTime * 1.0f;
        timer += dt;

        #region //fps計算
        frame++;
        float time = Time.realtimeSinceStartup - oldTime;
        if (time >= INTERVAL)
        {
            frameRate = frame / time;
            oldTime = Time.realtimeSinceStartup;
            frame = 0;
        }
        GameObject.Find("Canvas/TextFPS").GetComponent<Text>().text = "FPS " + frameRate;
        #endregion


        Vector3 pos = camera.transform.position;

        int posx = (int)(pos.x / 16);
        //int posy = (int)(pos.y / 16);
        int posz = (int)(pos.z / 16);

        if (true)
        {

            int cnt = 0;
            for (int k = 0; k < 9; k++)
            {
                int x = posx;
                //int y = posy;
                int z = posz;

                //3x3
                x += -1 + (k % 3);
                //y += -1 + (k / 3);
                z += -1 + (k / 3);

                #region //BoxWorldの限界
                if (x <= 0) continue;
                if (x >= GL.WorldLimit) continue;
                if (z <= 0) continue;
                if (z >= GL.WorldLimit) continue;
                #endregion

                if (GL.BoxWorld[x, z] == null)
                {
                    //SetCubeInBox(x, z);
                    StartCoroutine(CreateVisualMesh(x, z));
                    break;
                }
            }


        }


        //CubeBoxを渦巻き状に追加していく？  TODO最適化
        for (int k = 0; k < GL.Config_Callback; k++)
        {
            int max = GL.Config_FovLimit;   //遠景限界

            //Vector3 looking = new Vector3(posx, posy, posz);
            Vector3 vec = camera.transform.eulerAngles.normalized;
            vec = camera.transform.forward.normalized;

            float radius = 0.4f;
            float kaku = 0.0f;  //ラジアン角
            Vector2 before = new Vector2(posx, posz);
            while (radius < max)
            {
                for (int i = 0; i < 17; i++)
                {
                    //kaku = ((camera.transform.eulerAngles.y - 90) * Mathf.PI) / 180.0f;
                    kaku = (90 - camera.transform.eulerAngles.y) * (Mathf.PI / 180.0f);
                    kaku += (-0.40f + (0.05f * i)) * Mathf.PI;
                    int x = posx + (int)(radius * Mathf.Cos(kaku));
                    int z = posz + (int)(radius * Mathf.Sin(kaku));

                    #region //BoxWorldの限界
                    if (x <= 0) continue;
                    if (x >= GL.WorldLimit) continue;
                    if (z <= 0) continue;
                    if (z >= GL.WorldLimit) continue;
                    #endregion

                    if (before == new Vector2(x, z)) continue;

                    before = new Vector2(x, z);

                    if (GL.BoxWorld[x, z] == null)
                    {
                        //SetCubeInBox(x, z);
                        StartCoroutine(CreateVisualMesh(x, z));

                        radius += 16.0f;
                        break;
                    }
                }

                radius += 0.40f;
            }

        }


        #region  //旧式  視界に入ってると思われるCubeBoxを追加していく
        for (int k = 0; k < GL.Config_Callback; k++)
        {
            int max = GL.Config_FovLimit;   //遠景限界
            
            //Vector3 looking = new Vector3(posx, posy, posz);
            Vector3 vec = camera.transform.eulerAngles.normalized;
            vec = camera.transform.forward.normalized;
            
            float radius = 0.4f;
            float kaku = 0.0f;  //ラジアン角
            Vector2 before = new Vector2(posx,posz);
            while(radius < max)
            {
                for (int i = 0; i < 17; i++)
                {
                    //kaku = ((camera.transform.eulerAngles.y - 90) * Mathf.PI) / 180.0f;
                    kaku = (90 - camera.transform.eulerAngles.y) * (Mathf.PI / 180.0f);                    
                    kaku += (-0.40f + (0.05f*i)) * Mathf.PI;
                    int x = posx + (int)(radius * Mathf.Cos(kaku));
                    int z = posz + (int)(radius * Mathf.Sin(kaku));

                    #region //BoxWorldの限界
                    if (x <= 0) continue;
                    if (x >= GL.WorldLimit) continue;
                    if (z <= 0) continue;
                    if (z >= GL.WorldLimit) continue;
                    #endregion

                    if (before == new Vector2(x, z)) continue;

                    before = new Vector2(x, z);

                    if (GL.BoxWorld[x, z] == null)
                    {
                        //SetCubeInBox(x, z);
                        StartCoroutine(CreateVisualMesh(x,z));

                        radius += 16.0f;
                        break;
                    }
                }

                radius += 0.40f;
            }
            
        }
        #endregion

    }



    int width = 16;
    int height = 64;
    float brickHeight = 1.0f;
    public Mesh visualMesh;

    //チャンク単位の情報から新しいメッシュを作成
    public virtual IEnumerator CreateVisualMesh(int wx , int wz)
    {
        GameObject obj = (GameObject)Instantiate(CubeBox);
        obj.name = "CubeBox(" + wx + "," + wz + ")";
        obj.transform.position = new Vector3(16 * wx, 0, 16 * wz);
        GL.BoxWorld[wx, wz] = obj;

        //GameObject child = obj.transform.FindChild("SightCube").gameObject;
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
        MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();

        visualMesh = new Mesh();

        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> tris = new List<int>();

        int cx = 0;
        int cy = 0;
        int cz = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < width; z++)
                {

                    //if (map[x, y, z] == 0) continue;
                    cx = (width * wx) + x;
                    cy = y;
                    cz = (width * wz) + z;                   
                    if (GL.CubeWorld[cx, cy, cz] == 0) continue;

                    //byte brick = map[x, y, z];
                    byte brick = GL.CubeWorld[cx, cy, cz];

                    // Left wall
                    if (IsTransparent(cx - 1, cy, cz))
                        BuildFace(brick, new Vector3(x, y, z), Vector3.up, Vector3.forward, false, verts, uvs, tris);
                    // Right wall
                    if (IsTransparent(cx + 1, cy, cz))
                        BuildFace(brick, new Vector3(x + 1, y, z), Vector3.up, Vector3.forward, true, verts, uvs, tris);

                    // Bottom wall
                    if (IsTransparent(cx, cy - 1, cz))
                        BuildFace(brick, new Vector3(x, y, z), Vector3.forward, Vector3.right, false, verts, uvs, tris);
                    // Top wall
                    if (IsTransparent(cx, cy + 1, cz))
                        BuildFace(brick, new Vector3(x, y + 1, z), Vector3.forward, Vector3.right, true, verts, uvs, tris);

                    // Back
                    if (IsTransparent(cx, cy, cz - 1))
                        BuildFace(brick, new Vector3(x, y, z), Vector3.up, Vector3.right, true, verts, uvs, tris);
                    // Front
                    if (IsTransparent(cx, cy, cz + 1))
                        BuildFace(brick, new Vector3(x, y, z + 1), Vector3.up, Vector3.right, false, verts, uvs, tris);


                }
            }
        }

        visualMesh.vertices = verts.ToArray();
        visualMesh.uv = uvs.ToArray();
        visualMesh.triangles = tris.ToArray();
        visualMesh.RecalculateBounds();
        visualMesh.RecalculateNormals();

        meshFilter.mesh = visualMesh;


        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = visualMesh;

        yield return 0;

    }    
    public virtual void BuildFace(byte brick, Vector3 corner, Vector3 up, Vector3 right, bool reversed, List<Vector3> verts, List<Vector2> uvs, List<int> tris)
    {
        int index = verts.Count;


        //float uvRow = ((corner.y + up.y) % 15);
        //if (uvRow >= 8) uvRow = 15 - uvRow;
        //uvRow /= 8f;
        //Vector2 uvCorner = new Vector2(0.00f, uvRow);
        //if (brick < 16) uvCorner.y += 0.125f;

        corner.y *= brickHeight;
        up.y *= brickHeight;
        right.y *= brickHeight;

        verts.Add(corner);
        verts.Add(corner + up);
        verts.Add(corner + up + right);
        verts.Add(corner + right);

        //Vector2 uvWidth = new Vector2(0.125f, 0.125f);
        Vector2 uvWidth = new Vector2(0.062500f, -0.062500f) + new Vector2(-0.010f, +0.010f);

        //uvCorner.x += (float)((brick) % 16 - 1) / 16.0f;
        Vector2 uvCorner = new Vector2(0.062500f * (brick%16), -0.062500f * (brick/16)) + new Vector2(0.005f, -0.005f);

        uvs.Add(uvCorner);
        uvs.Add(new Vector2(uvCorner.x, uvCorner.y + uvWidth.y));
        uvs.Add(new Vector2(uvCorner.x + uvWidth.x, uvCorner.y + uvWidth.y));
        uvs.Add(new Vector2(uvCorner.x + uvWidth.x, uvCorner.y));

        if (reversed)
        {
            tris.Add(index + 0);
            tris.Add(index + 1);
            tris.Add(index + 2);
            tris.Add(index + 2);
            tris.Add(index + 3);
            tris.Add(index + 0);
        }
        else
        {
            tris.Add(index + 1);
            tris.Add(index + 0);
            tris.Add(index + 2);
            tris.Add(index + 3);
            tris.Add(index + 2);
            tris.Add(index + 0);
        }

    }
    public virtual bool IsTransparent(int x, int y, int z)
    {
        if (y < 0) return false;
        //byte brick = GetByte(x, y, z);
        byte brick = GL.CubeWorld[x, y, z];
        switch (brick)
        {
            case 0:
                return true;
            default:
                return false;
        }
    }



    //旧式　ボックスを新たに作成　その中に8*8*8のキューブをセット
    void SetCubeInBox(int x, int z)
    {
        GameObject obj = (GameObject)Instantiate(CubeBox);
        obj.name = "CubeBox(" + x + "," + z + ")";
        obj.transform.position = new Vector3(16 * x, 0, 16 * z);

        GL.BoxWorld[x, z] = obj;

        int cx = 0;
        int cy = 0;
        int cz = 0;

        for (int bx = 0; bx < 16; bx++)
        {
            for (int by = 0; by < 64; by++)
            {
                for (int bz = 0; bz < 16; bz++)
                {
                    cx = (16 * x) + bx;
                    cy = by;
                    cz = (16 * z) + bz;
                    int type = GL.CubeWorld[cx, cy, cz];
                    if (type == 0) continue;

                    GameObject cube = (GameObject)Instantiate(GL.CubeSet[type]);
                    cube.transform.parent = obj.transform;
                    cube.transform.localPosition = new Vector3(bx, by, bz);
                }
            }
        }

    }


}
