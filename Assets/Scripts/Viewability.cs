using UnityEngine;
using System.Collections;

public class Viewability : MonoBehaviour {

    GameObject target;
	
	// Update is called once per frame
	//void Update () {
 //       if (gameObject.GetComponent<Renderer>().IsVisibleFrom(Camera.main))
 //       {
 //           if (Input.GetKeyDown(KeyCode.A) ||  Input.GetButton("Xbox_360_A"))
 //           {
 //               Destroy(gameObject);
 //               Debug.Log("We picked up the blow torch");
 //           }
 //       }
 //   }
    //function OnTriggerEnter(other : Collider)
    //{
    //    if (other.gameObject.tag == "ball")
    //        Destroy(gameObject);
    //}
    void OnTriggerStay(Collider other)
    {
        Debug.Log("in trigger");
        if (Input.GetButton("Xbox_360_A"))
            Destroy(gameObject);
    }
}
