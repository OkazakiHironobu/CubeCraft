using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class touch_point : MonoBehaviour {

   

    

	// Use this for initialization
	void Start () {

    }

    float dt = 0.0f;

    void Update()
    {

		dt = Time.deltaTime * 1.0f;
		
        Vector3 pos = new Vector3(0, 0, 0);


        bool touch = false;
        
        //タッチ操作
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled)
                {
                    //Debug.Log("x=" + t.position.x + " y=" + t.position.y);

                    touch = true;
                    pos = t.position;
                }
            }
        }
        //デバッグ用クリック操作
        if (Input.GetMouseButtonUp(0))
        {
            touch = true;
            pos = Input.mousePosition;           
        }
        if (touch)
        {
            
            

            Debug.Log("Touch "+pos.x+","+pos.y);

            //マップ上の座標計算
            //Vector3 target = new Vector3(0, 0, 0);
            //Vector3 vec = pos;

            //Ray ray0 = new Ray();
            //RaycastHit hit0 = new RaycastHit();
            //ray0 = Camera.main.ScreenPointToRay(pos);
            //int layerMask0 = 1 << 8;  //8 Ground
            //if (Physics.Raycast(ray0.origin, ray0.direction, out hit0, 100.0f, layerMask0))
            //{
            //    target = hit0.point;

            //}
            //else
            //{

            //}


            Vector3 tpos = pos;
            tpos.x = (pos.x * (1920.0f / Screen.width)) - 960.0f;
            tpos.y = (pos.y * (1080.0f / Screen.height)) - 540.0f;

            GameObject canvas = GameObject.Find("Canvas");
            GameObject flower = GameObject.Find("Canvas/Flower");
            GameObject tmpobj = (GameObject)Instantiate(flower , new Vector3(0,0,0), Quaternion.identity);
            tmpobj.transform.parent = canvas.transform;
            tmpobj.transform.localPosition = tpos;
            tmpobj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }



        



    }


    //public static List<GameObject> selecting = new List<GameObject>();




}




