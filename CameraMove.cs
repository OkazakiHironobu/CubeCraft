using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    bool Mouse_R = false;
    Vector3 sight_pos = new Vector3(0,0,0);
    Vector3 sight_vec = new Vector3(0, 0, 0);

    // Update is called once per frame
    void Update () {

        float d = 0.5f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += this.transform.up * d;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += this.transform.up * -d;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += this.transform.right * -d;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += this.transform.right * d;
        }

        float MSW = Input.GetAxis("Mouse ScrollWheel");
        if (MSW > 0.01f)
        {
            this.transform.position += this.transform.forward * 2.0f * d;
        }
        if (MSW < -0.01f)
        {
            this.transform.position += this.transform.forward * -2.0f * d;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Mouse_R = true;
            sight_pos = Input.mousePosition;
            sight_vec = this.transform.localEulerAngles;
        }
        if (Mouse_R)
        {
            Vector3 dvec = Input.mousePosition - sight_pos;
            Vector3 vec = new Vector3(-dvec.y, dvec.x, 0) * 0.25f;

            this.transform.localEulerAngles = sight_vec + vec;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Mouse_R = false;
        }

        

    }


}
