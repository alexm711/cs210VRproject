using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public AudioClip hitSound;
    public AudioClip gameOverSound;
    public AudioClip forwardSound;
    public AudioClip backSound;

    public float forceConstant = 1;

	
	// Update is called once per frame
	void Update () {
        Vector3 direction = GameObject.FindObjectOfType<Camera>().transform.forward;
        Vector3 force = direction * forceConstant;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody>().AddForce(force);
            SoundManager.instance.playSoundEffect(forwardSound);
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            force *= -1;
            GetComponent<Rigidbody>().AddForce(force);
            SoundManager.instance.playSoundEffect(backSound);
        }

        else if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Rigidbody>().Sleep();
        }

        else
        {
            SoundManager.instance.stopSoundEffect();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            SoundManager.instance.playSoundEffectOnce(hitSound);
            other.gameObject.SetActive(false);
            Debug.Log("Target object hit");
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            //TODO: we die
            SoundManager.instance.setBackgroundMusic(gameOverSound);
        }
        
    }
}
