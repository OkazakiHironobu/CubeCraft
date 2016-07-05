using UnityEngine;
using System.Collections;

public class CubeBoxUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    float death_timer = 0;

	// Update is called once per frame
	void Update () {

        death_timer+=Time.deltaTime;
        if (death_timer > 2.0f)
        {
            //GameObject obj = this.transform.parent.gameObject;
            //Destroy(obj);
            Destroy(this.gameObject);
        }

	}


    //カメラに入ってるとき
    void OnWillRenderObject()
    {

        if (Camera.current.name == "Main Camera") death_timer = 0;
    
    }




}
