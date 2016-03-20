using UnityEngine;
using System.Collections;

public class WithinBounds : MonoBehaviour {

    public int bounds = 10;
	
	// Update is called once per frame
	void Update () {
	    if(Vector3.Distance(gameObject.transform.position, Vector3.zero) > bounds)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-gameObject.transform.position);
        }
	}
}
