using UnityEngine;
using System.Collections;

public class PlayerControllerAlex : MonoBehaviour {

    public AudioClip hitSound;
    public AudioClip gameOverSound;
    public AudioClip forwardSound;
    public AudioClip backSound;

    public float force_constant = 5;
    public float boost_magnitude = 3;
    public float roll_magnitude = 50;
    public float pitch_magnitude = 50;
    public float yaw_magnitude = 50;

    public float braking_magnitude = 5;
    public float brake_sleep_threshold = 0.25f;
    public float camera_body_reorientation_time = 1;
    public float world_reorientation_time = 1;
    public float vertical_boost_thrust = 5;
    public float thrust_up_or_down_flag = 1;
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

    Vector3 applyBoost(Vector3 force){
        if (Input.GetButton("Xbox_360_A")) { force *= boost_magnitude; }
        return force;
    }

    void move2()
    {
        Vector3 cameraForwardDirection = GameObject.FindObjectOfType<Camera>().transform.forward;
        Vector3 cameraRightDirection = GameObject.FindObjectOfType<Camera>().transform.right;
        Vector3 bodyForwardDirection = GetComponent<Rigidbody>().transform.forward;
        Vector3 bodyRightDirection = GetComponent<Rigidbody>().transform.right;
        Vector3 bodyVerticalDirection = GetComponent<Rigidbody>().transform.up;

        Vector3 force = bodyForwardDirection * force_constant;

        //Thrust left right forward backward
        if (Input.GetAxis("Xbox_360_LeftJoystickX") != 0 || Input.GetAxis("Xbox_360_LeftJoystickY") != 0)
        {
            force = -Input.GetAxis("Xbox_360_LeftJoystickY") * force_constant * bodyForwardDirection + Input.GetAxis("Xbox_360_LeftJoystickX") * bodyRightDirection * force_constant;
            force = applyBoost(force);
            GetComponent<Rigidbody>().AddForce(force);
            playSound(force);
        }
        //yaw, pitch 
        if (Input.GetAxis("Xbox_360_RightJoystickX") != 0 || Input.GetAxis("Xbox_360_RightJoystickY") != 0)
        {
            float yawTurnSpeed = Input.GetAxis("Xbox_360_RightJoystickX")* 1;
            float pitchTurnSpeed = Input.GetAxis("Xbox_360_RightJoystickY")* 1;
            transform.Rotate(Vector3.up* yawTurnSpeed * yaw_magnitude * Time.deltaTime);
            transform.Rotate(Vector3.right * pitchTurnSpeed * pitch_magnitude * Time.deltaTime);
            //playSound(force);
        }
        //Air-Braking
        if (Input.GetButton("Xbox_360_B")) { GetComponent<Rigidbody>().AddForce(applyBoost(GetComponent<Rigidbody>().velocity.normalized) * -force_constant);}

        if (Input.GetButtonDown("Xbox_360_Y")){ thrust_up_or_down_flag *= -1;}

        if (Input.GetButton("Xbox_360_LeftBumper") || Input.GetButton("Xbox_360_RightBumper"))
        {
            int leftBoolInt = Input.GetButton("Xbox_360_LeftBumper") ? 1 : 0;
            int rightBoolInt = Input.GetButton("Xbox_360_RightBumper") ? 1 : 0;
            float rollTurnSpeed = (leftBoolInt - rightBoolInt)*roll_magnitude;
            transform.Rotate(Vector3.forward * rollTurnSpeed * Time.deltaTime);
        }

        if (Input.GetButton("Xbox_360_LeftStickClick")) {
            float y= GetComponent<Rigidbody>().rotation.eulerAngles.y;
            GetComponent<Rigidbody>().rotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, Quaternion.Euler(0,y,0),  0.02f);
        }
        if (Input.GetButton("Xbox_360_RightStickClick"))
        {
            GetComponent<Rigidbody>().rotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, GameObject.FindObjectOfType<Camera>().transform.rotation,  0.02f);
        }
        if (Input.GetAxis("Xbox_360_RightTrigger") != 0 && Input.GetAxis("Xbox_360_LeftTrigger") < 0.5)
        {
            Vector3 vertical_force = bodyVerticalDirection * Input.GetAxis("Xbox_360_RightTrigger") *  thrust_up_or_down_flag*force_constant;
            vertical_force = applyBoost(vertical_force);
            GetComponent<Rigidbody>().AddForce(vertical_force);
        }
        if(Input.GetAxis("Xbox_360_LeftTrigger") != 0)
        {
            //Zoom-in function + make cursor visible
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            force *= -1;
            GetComponent<Rigidbody>().AddForce(force);
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
