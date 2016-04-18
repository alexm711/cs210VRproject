using UnityEngine;
using System.Collections;

public class InitialRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 4;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
