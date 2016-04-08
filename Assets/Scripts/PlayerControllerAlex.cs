using UnityEngine;
using System.Collections;

public class PlayerControllerAlex : MonoBehaviour {

    public AudioClip hitSound;
    public AudioClip gameOverSound;
    public AudioClip forwardSound;
    public AudioClip backSound;

    public float forceConstant = 5;
    public float boost_multiplier = 10;
    public float rollMagnitude = 100;
    public float pitchMagnitude = 200;
    public float braking_magnitude = 5;
    public float yawMagnitude = 200;
    public float brake_sleep_threshold = 2;
    public float camera_body_reorientation_time = 1;
    public float world_reorientation_time = 1;
    public float vertical_boost_thrust = 100;
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

    //void move1()
    //{
    //    Vector3 forwardDirection = GameObject.FindObjectOfType<Camera>().transform.forward;
    //    Vector3 rightDirection = GameObject.FindObjectOfType<Camera>().transform.right;


    //    Vector3 force = forwardDirection * forceConstant;

    //    if (Input.GetKey(KeyCode.UpArrow))
    //    {
    //        GetComponent<Rigidbody>().AddForce(force);
    //    }
    //    if (Input.GetAxis("Xbox_360_LeftJoystickX") != 0 || Input.GetAxis("Xbox_360_LeftJoystickY") != 0)
    //    {
    //        force = -Input.GetAxis("Xbox_360_LeftJoystickY") * forceConstant * forwardDirection + Input.GetAxis("Xbox_360_LeftJoystickX") * rightDirection * forceConstant;
    //        GetComponent<Rigidbody>().AddForce(force);
    //        playSound(force);
    //    }

    //    if (Input.GetKey(KeyCode.DownArrow))
    //    {
    //        force *= -1;
    //        GetComponent<Rigidbody>().AddForce(force);
    //    }

    //    else if (Input.GetKey(KeyCode.Space) || Input.GetButtonDown("Xbox_360_A"))
    //    {
    //        GetComponent<Rigidbody>().Sleep();
    //    }

    //    else
    //    {
    //        SoundManager.instance.stopSoundEffect();
    //    }
    //}
    Vector3 applyBoost(Vector3 force){
        if (Input.GetButton("Xbox_360_A")) { force *= boost_multiplier; }
        return force;
    }

    void move2()
    {
        Vector3 cameraForwardDirection = GameObject.FindObjectOfType<Camera>().transform.forward;
        Vector3 cameraRightDirection = GameObject.FindObjectOfType<Camera>().transform.right;
        Vector3 bodyForwardDirection = GetComponent<Rigidbody>().transform.forward;
        Vector3 bodyRightDirection = GetComponent<Rigidbody>().transform.right;
        Vector3 bodyVerticalDirection = GetComponent<Rigidbody>().transform.up;

        Vector3 force = bodyForwardDirection * forceConstant;

        //Thrust left right forward backward
        if (Input.GetAxis("Xbox_360_LeftJoystickX") != 0 || Input.GetAxis("Xbox_360_LeftJoystickY") != 0)
        {
            force = -Input.GetAxis("Xbox_360_LeftJoystickY") * forceConstant * bodyForwardDirection + Input.GetAxis("Xbox_360_LeftJoystickX") * bodyRightDirection * forceConstant;
            force = applyBoost(force);
            GetComponent<Rigidbody>().AddForce(force);
            playSound(force);
        }
        //yaw, pitch 
        if (Input.GetAxis("Xbox_360_RightJoystickX") != 0 || Input.GetAxis("Xbox_360_RightJoystickY") != 0)
        {
            //force = -Input.GetAxis("Xbox_360_LeftJoystickY") * forceConstant * bodyForwardDirection + Input.GetAxis("Xbox_360_LeftJoystickX") * bodyRightDirection * forceConstant;
            //force = applyBoost(force);
            float yawTurnSpeed = Input.GetAxis("Xbox_360_RightJoystickX");
            float pitchTurnSpeed = Input.GetAxis("Xbox_360_RightJoystickY");
            GetComponent<Rigidbody>().transform.Rotate(Vector3.up* yawTurnSpeed * 200* Time.deltaTime);
            GetComponent<Rigidbody>().transform.Rotate(bodyRightDirection* pitchTurnSpeed * 200* Time.deltaTime);
            //playSound(force);
        }
        //Air-Braking
        if (Input.GetButton("Xbox_360_B"))
        {
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            if (velocity.magnitude < brake_sleep_threshold){
                GetComponent<Rigidbody>().Sleep();
            }else{
                GetComponent<Rigidbody>().AddForce(applyBoost(velocity.normalized) * -forceConstant);
            }
        }

        if (Input.GetButtonDown("Xbox_360_Y")){ thrust_up_or_down_flag *= -1;}

        if (Input.GetButton("Xbox_360_LeftBumper") || Input.GetButton("Xbox_360_RightBumper"))
        {
            int leftBoolInt = Input.GetButton("Xbox_360_LeftBumper") ? 1 : 0;
            int rightBoolInt = Input.GetButton("Xbox_360_RightBumper") ? 1 : 0;
            float rollTurnSpeed = (leftBoolInt - rightBoolInt)*rollMagnitude;
            transform.Rotate(bodyForwardDirection, rollTurnSpeed * Time.deltaTime);
            GetComponent<Rigidbody>().transform.Rotate(bodyForwardDirection * 200* Time.deltaTime);


        }

        if (Input.GetButton("Xbox_360_LeftStickClick"))
        {
            GetComponent<Rigidbody>().rotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, Quaternion.identity,  0.02f);
        }
        if (Input.GetButton("Xbox_360_RightStickClick"))
        {
            GetComponent<Rigidbody>().rotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, GameObject.FindObjectOfType<Camera>().transform.rotation,  0.02f);
        }
        if (Input.GetAxis("Xbox_360_RightTrigger") != 0 && Input.GetAxis("Xbox_360_LeftTrigger") < 0.5)
        {
            Vector3 vertical_force = bodyVerticalDirection * Input.GetAxis("Xbox_360_RightTrigger") * vertical_boost_thrust * thrust_up_or_down_flag*forceConstant;
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
