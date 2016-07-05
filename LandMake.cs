using UnityEngine;
using System.Collections;

public class LandMake : MonoBehaviour {    

    

    
    //public GameObject CubeBox;

    // Use this for initialization
    void Start () {

        int[,] height_map = new int[2048, 2048];

        #region //ハイトマップ作成１
        ////z=64-72でハイトマップ作成
        //int[,] height_map = new int[128, 128];
        //int height0 = 4;
        //for (int x = 0; x < 128; x++)
        //{
        //    float rnd = Random.value;
        //    if (rnd < 0.2f) height0--;
        //    if (rnd > 0.8f) height0++;
        //    if (height0 < 0) height0 = 0;
        //    if (height0 > 8) height0 = 8;
        //    height_map[x, 0] = height0;
        //}
        //height0 = 4;
        //for (int z = 0; z < 128; z++)
        //{
        //    float rnd = Random.value;
        //    if (rnd < 0.2f) height0--;
        //    if (rnd > 0.8f) height0++;
        //    if (height0 < 0) height0 = 0;
        //    if (height0 > 8) height0 = 8;
        //    height_map[0, z] = height0;
        //}

        //int height1 = 4;
        //for (int x = 1; x < 128; x++)
        //{
        //    for (int z = 1; z < 128; z++)
        //    {
        //        height1 = height_map[x - 1, z] + height_map[x, z - 1];
        //        height1 = height1 / 2;
        //        float rnd = Random.value;
        //        if (rnd < 0.2f) height1--;
        //        if (rnd > 0.8f) height1++;
        //        if (height1 < 0) height1 = 0;
        //        if (height1 > 8) height1 = 8;

        //        height_map[x, z] = height1;
        //    }
        //}
        #endregion

        #region //ハイトマップ作成２
        //int[,] height_map = new int[1024, 1024];
        ////z=64-72でハイトマップ作成
        //int height0 = 4;
        //for (int x = 0; x < 1024; x++)
        //{
        //    float rnd = Random.value;
        //    if (rnd < 0.2f) height0--;
        //    if (rnd > 0.8f) height0++;
        //    if (height0 < 0) height0 = 0;
        //    if (height0 > 8) height0 = 8;
        //    height_map[x, 0] = height0;
        //}
        //height0 = 4;
        //for (int z = 0; z < 1024; z++)
        //{
        //    float rnd = Random.value;
        //    if (rnd < 0.2f) height0--;
        //    if (rnd > 0.8f) height0++;
        //    if (height0 < 0) height0 = 0;
        //    if (height0 > 8) height0 = 8;
        //    height_map[0, z] = height0;
        //}

        //int height1 = 4;
        //for (int x = 1; x < 1024; x++)
        //{
        //    for (int z = 1; z < 1024; z++)
        //    {
        //        height1 = height_map[x - 1, z] + height_map[x, z - 1];
        //        height1 = height1 / 2;
        //        float rnd = Random.value;
        //        if (rnd < 0.2f) height1--;
        //        if (rnd > 0.8f) height1++;
        //        if (height1 < 0) height1 = 0;
        //        if (height1 > 8) height1 = 8;

        //        height_map[x, z] = height1;
        //    }
        //}        
        #endregion

        #region //ハイトマップ作成３


        //ハイトマップ8x8
        int[,] height_map1 = new int[128, 128];
        int height0 = 4;
        for (int x = 0; x < 128; x++)
        {
            float rnd = Random.value;
            if (rnd < 0.2f) height0--;
            if (rnd > 0.8f) height0++;
            if (height0 < 0) height0 = 0;
            if (height0 > 8) height0 = 8;
            height_map1[x, 0] = height0;
        }
        height0 = 4;
        for (int z = 0; z < 128; z++)
        {
            float rnd = Random.value;
            if (rnd < 0.2f) height0--;
            if (rnd > 0.8f) height0++;
            if (height0 < 0) height0 = 0;
            if (height0 > 8) height0 = 8;
            height_map1[0, z] = height0;
        }

        int height1 = 4;
        for (int x = 1; x < 128; x++)
        {
            for (int z = 1; z < 128; z++)
            {
                height1 = height_map1[x - 1, z] + height_map1[x, z - 1];
                height1 = height1 / 2;
                float rnd = Random.value;
                if (rnd < 0.2f) height1--;
                if (rnd > 0.8f) height1++;
                if (height1 < 0) height1 = 0;
                if (height1 > 8) height1 = 8;

                height_map1[x, z] = height1;
            }
        }
        //ハイトマップ4x4
        int[,] height_map2 = new int[256, 256];
        for (int x = 0; x < 256; x++)
        {
            for (int z = 0; z < 256; z++)
            {
                height_map2[x, z] = height_map1[x / 2, z / 2];
                int height2 = height_map2[x, z];
                float rnd = Random.value;
                if (rnd < 0.2f) height2--;
                if (rnd > 0.8f) height2++;
                if (height2 < 0) height2 = 0;
                if (height2 > 8) height2 = 8;
                height_map2[x, z] = height2;
            }
        }
        //ハイトマップ2x2
        int[,] height_map3 = new int[512, 512];
        for (int x = 0; x < 512; x++)
        {
            for (int z = 0; z < 512; z++)
            {
                height_map3[x, z] = height_map2[x / 2, z / 2];
                int height3 = height_map3[x, z];
                float rnd = Random.value;
                if (rnd < 0.1f) height3--;
                if (rnd > 0.9f) height3++;
                if (height3 < 0) height3 = 0;
                if (height3 > 8) height3 = 8;
                height_map3[x, z] = height3;
            }
        }
        //ハイトマップ　本物
        for (int x = 0; x < 1024; x++)
        {
            for (int z = 0; z < 1024; z++)
            {
                height_map[x, z] = height_map3[x / 2, z / 2];
                int height4 = height_map[x, z];
                float rnd = Random.value;
                if (rnd < 0.01f) height4--;
                if (rnd > 0.99f) height4++;
                if (height4 < 0) height4 = 0;
                if (height4 > 8) height4 = 8;
                height_map[x, z] = height4;
            }
        }
        #endregion


        //CubeWorld 作成
        //平原
        int height = 8;
        for (int x = 1; x < GL.WorldLimit - 1; x++)
        {
            for (int z = 1; z < GL.WorldLimit - 1; z++)
            {                

                for (int k = 0; k < 16; k++)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        int h = height;
                                             
                        int dx = (16 * x) + k;
                        int dz = (16 * z) + i;
                       
                        h = height + height_map[dx,dz];

                        if (h > 0)
                        {
                            for (int cnt = 0; cnt < h; cnt++)
                            {
                                GL.CubeWorld[dx, cnt, dz] = 16;  //土
                            }
                        }

                        GL.CubeWorld[dx, h, dz] = 17;  //草
                    }
                }                

            }
        }
        //雲
        height = 16;
        for (int x = 1; x < GL.WorldLimit - 1; x++)
        {
            for (int z = 1; z < GL.WorldLimit - 1; z++)
            {
                int h = height + (Random.Range(0, 16));

                int dx = (16 * x) + (Random.Range(0, 15));
                int dz = (16 * z) + (Random.Range(0, 15));

                GL.CubeWorld[dx, h, dz] = 2;  //白色
                GL.CubeWorld[dx+1, h, dz] = 2;  //白色
                GL.CubeWorld[dx, h, dz+1] = 2;  //白色
                GL.CubeWorld[dx+1, h, dz+1] = 2;  //白色
            }
        }
        //星々
        height = 32;
        for (int x = 1; x < GL.WorldLimit - 1; x++)
        {
            for (int z = 1; z < GL.WorldLimit - 1; z++)
            {
                int h = height + (Random.Range(0, 16));

                int dx = (16 * x) + (Random.Range(0,15));
                int dz = (16 * z) + (Random.Range(0,15));

                GL.CubeWorld[dx, h, dz] = 4;  //黄色
            }
        }



    



    }

    // Update is called once per frame
    void Update () {
	
	}



}


