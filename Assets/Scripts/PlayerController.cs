using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float forceConstant = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = GameObject.FindObjectOfType<Camera>().transform.forward;
        Vector3 force = direction * forceConstant;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody>().AddForce(force);
            //GetComponent<CharacterController>().Move(direction);
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction *= -1;
            GetComponent<Rigidbody>().AddForce(force);
            //GetComponent<CharacterController>().Move(direction);

        }
    }

    void 
}
