using UnityEngine;
using System.Collections;

public class Viewability : MonoBehaviour {

    GameObject target;
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<Renderer>().IsVisibleFrom(Camera.main))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Destroy(gameObject);
                Debug.Log("We picked up the blow torch");
            }
        }
    }

}
