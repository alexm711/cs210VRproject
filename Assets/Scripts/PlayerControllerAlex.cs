using UnityEngine;
using System.Collections;

public class PlayerControllerAlex : MonoBehaviour {



    public float translation_acceleration = 500f;
    public float boost_multiplier = 2f;
    public float rotation_acceleration = 0.2f;
    public float braking_rotation_threshold = 0.01f;
    public float braking_translation_threshold = 0.5f;
    //public float roll_magnitude = 50;
    //public float pitch_magnitude = 50;
    //public float yaw_magnitude = 50;

    public float braking_magnitude = 5;
    public float brake_sleep_threshold = 0.25f;
    public float camera_body_reorientation_time = 1;
    public float world_reorientation_time = 1;
    //public float vertical_boost_thrust = 5;
    public float thrust_up_or_down_flag = 1;
    private Vector3 movementVector;
    private bool navigationDisabled = false;
    private bool mapping1 = true;
    private Quaternion camera_rotation;

    public AudioClip hitSound;
    public AudioClip gameOverSound;
    public AudioClip forwardSound;
    public AudioClip backSound;
    private Rigidbody rb;


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
        if (Input.GetButton("Xbox_360_A")) { force *= boost_multiplier; }
        return force;
    }

    void move2()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 cameraForwardDirection = GameObject.FindObjectOfType<Camera>().transform.forward;
        Vector3 cameraRightDirection = GameObject.FindObjectOfType<Camera>().transform.right;
 

        Vector3 force;
        if (Input.GetButton("Xbox_360_Back")) { UnityEngine.VR.InputTracking.Recenter(); }

        //Thrust left right forward backward
        if (Input.GetAxis("Xbox_360_LeftJoystickX") != 0 || Input.GetAxis("Xbox_360_LeftJoystickY") != 0)
        {
            force = (-Input.GetAxis("Xbox_360_LeftJoystickY") * rb.transform.forward + Input.GetAxis("Xbox_360_LeftJoystickX") * rb.transform.right) * 150f * Time.deltaTime;
            force = applyBoost(force);
            rb.AddForce(force);
            playSound(force);
        }
        //yaw, pitch 

        if (Input.GetAxis("Xbox_360_RightJoystickX") != 0 || Input.GetAxis("Xbox_360_RightJoystickY") != 0)
        {
            //float yawTurnSpeed = Input.GetAxis("Xbox_360_RightJoystickX") * rotation_acceleration * rb.transform.up;
            //float pitchTurnSpeed = Input.GetAxis("Xbox_360_RightJoystickY") * rotation_acceleration * rb.transform.right;
            force = (Input.GetAxis("Xbox_360_RightJoystickX")  * rb.transform.up + Input.GetAxis("Xbox_360_RightJoystickY") * rb.transform.right*2)* rotation_acceleration * Time.deltaTime;
            //rb.AddTorque(transform.up * torque * turn);
            rb.AddTorque(force);
            //transform.Rotate(Vector3.up * yawTurnSpeed * rotation_speed * Time.deltaTime);
            //transform.Rotate(Vector3.right * pitchTurnSpeed * rotation_speed * Time.deltaTime);
            //playSound(force);
        }
        //if (Input.GetAxis("Xbox_360_RightJoystickX") != 0 || Input.GetAxis("Xbox_360_RightJoystickY") != 0)
        //{
        //    float yawTurnSpeed = Input.GetAxis("Xbox_360_RightJoystickX") * 1;
        //    float pitchTurnSpeed = Input.GetAxis("Xbox_360_RightJoystickY") * 1;
        //    transform.Rotate(Vector3.up * yawTurnSpeed * rotation_speed * Time.deltaTime);
        //    transform.Rotate(Vector3.right * pitchTurnSpeed * rotation_speed * Time.deltaTime);
        //    //playSound(force);
        //}
        //Air-Braking
        if (Input.GetButton("Xbox_360_B")) {
            rb.AddForce(applyBoost(rb.velocity.normalized) * -150f * Time.deltaTime);
            rb.AddTorque(rb.angularVelocity.normalized  * -rotation_acceleration * Time.deltaTime);
            if (rb.angularVelocity.magnitude < braking_rotation_threshold*0.2f) rb.angularVelocity = Vector3.zero;
            if (rb.velocity.magnitude < braking_translation_threshold) rb.velocity = Vector3.zero;
            //Debug.Log(rb.velocity.magnitude);
            //Debug.Log(rb.angularVelocity.magnitude);

        }

        if (Input.GetButtonDown("Xbox_360_Y")){ thrust_up_or_down_flag *= -1;}

        //if (Input.GetButton("Xbox_360_LeftBumper") || Input.GetButton("Xbox_360_RightBumper"))
        //{
        //    int leftBoolInt = Input.GetButton("Xbox_360_LeftBumper") ? 1 : 0;
        //    int rightBoolInt = Input.GetButton("Xbox_360_RightBumper") ? 1 : 0;
        //    float rollTurnSpeed = (leftBoolInt - rightBoolInt) * rotation_speed;
        //    transform.Rotate(Vector3.forward * rollTurnSpeed * Time.deltaTime);
        //    force = Input.GetAxis("Xbox_360_RightJoystickX") * rb.transform.up + Input.GetAxis("Xbox_360_RightJoystickY") * rb.transform.right;
        //    //rb.AddTorque(transform.up * torque * turn);
        //    rb.AddTorque(force * 0.05f);
        //}
        if (Input.GetButton("Xbox_360_LeftBumper") || Input.GetButton("Xbox_360_RightBumper"))
        {
            float leftBoolInt = Input.GetButton("Xbox_360_LeftBumper") ? 1f : 0;
            float rightBoolInt = Input.GetButton("Xbox_360_RightBumper") ? 1f : 0;
            force = rb.transform.forward *2f* (leftBoolInt - rightBoolInt) * Time.deltaTime*rotation_acceleration;
            rb.AddTorque(force);
            //force = Input.GetAxis("Xbox_360_RightJoystickX") * rb.transform.up + Input.GetAxis("Xbox_360_RightJoystickY") * rb.transform.right;
            //rb.AddTorque(transform.up * torque * turn);
            //rb.AddTorque(force * 0.05f);
        }


        if (Input.GetButton("Xbox_360_LeftStickClick")) {
            if (rb.angularVelocity.magnitude > braking_rotation_threshold) rb.AddTorque(rb.angularVelocity.normalized * -rotation_acceleration * Time.deltaTime);
            else {
                rb.angularVelocity = Vector3.zero;
                float y = rb.rotation.eulerAngles.y;
                rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(0, y, 0), 0.02f);
            }
        }
        if (Input.GetButtonDown("Xbox_360_RightStickClick"))
        {
            camera_rotation = GameObject.FindObjectOfType<Camera>().transform.rotation;
        }
        if (Input.GetButton("Xbox_360_RightStickClick"))
        {
            if (rb.angularVelocity.magnitude > braking_rotation_threshold) rb.AddTorque(rb.angularVelocity.normalized * -rotation_acceleration * Time.deltaTime);
            else {
                rb.angularVelocity = Vector3.zero;
                rb.rotation = Quaternion.Slerp(rb.rotation, camera_rotation, 0.02f);
            }
        }
        if (Input.GetAxis("Xbox_360_RightTrigger") != 0 && Input.GetAxis("Xbox_360_LeftTrigger") < 0.5)
        {
            Vector3 vertical_force = rb.transform.up * Input.GetAxis("Xbox_360_RightTrigger") *  thrust_up_or_down_flag* translation_acceleration;
            vertical_force = applyBoost(vertical_force);
            rb.AddForce(vertical_force);
        }
        if(Input.GetAxis("Xbox_360_LeftTrigger") != 0)
        {
            //Zoom-in function + make cursor visible
        }
 

        //else
        //{
        //    SoundManager.instance.stopSoundEffect();
        //}
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
