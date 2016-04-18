using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Xbox_360_A"))
        {
            Debug.Log("Pressed A on the XBox");
        }

    }
}
