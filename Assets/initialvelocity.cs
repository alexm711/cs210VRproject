using UnityEngine;
using System.Collections;

public class initialvelocity : MonoBehaviour {
    public Rigidbody rb;
    // Use this for initialization
    void Start () {
         rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, -1.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
