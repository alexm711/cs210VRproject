using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public AudioClip hitSound;

    public float forceConstant = 1;

	
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            SoundManager.instance.playSoundEffect(hitSound);
            other.gameObject.SetActive(false);
            Debug.Log("Target object hit");
        }
        else
            Debug.Log("object was not Target");
        
    }
}
