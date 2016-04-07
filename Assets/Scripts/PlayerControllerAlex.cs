using UnityEngine;
using System.Collections;

public class PlayerControllerAlex : MonoBehaviour {

    public AudioClip hitSound;
    public AudioClip gameOverSound;
    public AudioClip forwardSound;
    public AudioClip backSound;

    public float forceConstant = 5;
    private Vector3 movementVector;
    private bool navigationDisabled = false;
    private bool mapping1 = true;


    // Update is called once per frame
    void Update()
    {
        if (!navigationDisabled)
        {
            if (mapping1)
            {
                move2();
            }
        }
    }

    public void disableNavigation()
    {
        navigationDisabled = true;
    }

    public void enableNavigation()
    {
        navigationDisabled = false;
    }

    void move1()
    {
        Vector3 forwardDirection = GameObject.FindObjectOfType<Camera>().transform.forward;
        Vector3 rightDirection = GameObject.FindObjectOfType<Camera>().transform.right;


        Vector3 force = forwardDirection * forceConstant;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody>().AddForce(force);
        }
        if (Input.GetAxis("Xbox_360_LeftJoystickX") != 0 || Input.GetAxis("Xbox_360_LeftJoystickY") != 0)
        {
            force = -Input.GetAxis("Xbox_360_LeftJoystickY") * forceConstant * forwardDirection + Input.GetAxis("Xbox_360_LeftJoystickX") * rightDirection * forceConstant;
            GetComponent<Rigidbody>().AddForce(force);
            playSound(force);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            force *= -1;
            GetComponent<Rigidbody>().AddForce(force);
        }

        else if (Input.GetKey(KeyCode.Space) || Input.GetButtonDown("Xbox_360_A"))
        {
            GetComponent<Rigidbody>().Sleep();
        }

        else
        {
            SoundManager.instance.stopSoundEffect();
        }
    }

    void move2()
    {
        Vector3 cameraForwardDirection = GameObject.FindObjectOfType<Camera>().transform.forward;
        Vector3 cameraRightDirection = GameObject.FindObjectOfType<Camera>().transform.right;
        Vector3 bodyForwardDirection = GetComponent<Rigidbody>().transform.forward;
        Vector3 bodyRightDirection = GetComponent<Rigidbody>().transform.right;

        Vector3 force = bodyForwardDirection * forceConstant;

        //Thrust left right forward backward
        if (Input.GetAxis("Xbox_360_LeftJoystickX") != 0 || Input.GetAxis("Xbox_360_LeftJoystickY") != 0)
        {
            force = -Input.GetAxis("Xbox_360_LeftJoystickY") * forceConstant * bodyForwardDirection + Input.GetAxis("Xbox_360_LeftJoystickX") * bodyRightDirection * forceConstant;
            GetComponent<Rigidbody>().AddForce(force);
            playSound(force);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            force *= -1;
            GetComponent<Rigidbody>().AddForce(force);
        }

        else if (Input.GetKey(KeyCode.Space) || Input.GetButtonDown("Xbox_360_A"))
        {
            GetComponent<Rigidbody>().Sleep();
        }

        else
        {
            SoundManager.instance.stopSoundEffect();
        }
    }

    void playSound(Vector3 force)
    {
        SoundManager.instance.playSoundEffect(forwardSound);
        SoundManager.instance.playSoundEffect(backSound);

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
