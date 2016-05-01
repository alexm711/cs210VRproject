using UnityEngine;
using System.Collections;

public class PlayerControllerAlex : MonoBehaviour {



    public static float translation_acceleration = 150f;
    public static float boost_multiplier = 2f;
    public static float rotation_acceleration = 10f;
	public static bool allowRoll = true;
	public static bool allowPitch = true;
	public static bool invert_yaxis = false;
    public static bool fixed_angular_speed = true;
    public static bool fixed_linear_speed = true;
    public static bool safety_collisions = true;
    public static float rotation_velocity = 60f;
    public static float translation_velocity = 160f;
    private bool fix_to_floor = false;
	private GameObject closestGravityWell;
	private Vector3 surfaceNormal;
	private float distGround;

	private float transition_downtime, transition_uptime, pressTime = 0f;
	private float transition_delta = 1.0f;
	private bool transition_ready = false;
	private bool transitioning = false;

	public float braking_rotation_threshold = 0.003f;
	public float braking_translation_threshold = 0.05f;
    //public float roll_magnitude = 50;
    //public float pitch_magnitude = 50;
    //public float yaw_magnitude = 50;

    public float braking_magnitude = 5;
    public float brake_sleep_threshold = 0.25f;
    public float camera_body_reorientation_time = 1;
    public float world_reorientation_time = 1;
    //public float vertical_boost_thrust = 5;
    //public float thrust_up_or_down_flag = 1;
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
    void Update() {
        if (!navigationDisabled) {
            if (mapping1) {
                move2();
            }
        }
    }

    public void disableNavigation() {
        navigationDisabled = true;
    }

    public void enableNavigation() {
        navigationDisabled = false;
    }

    Vector3 applyBoost(Vector3 force){
        if (Input.GetButton("Xbox_360_A")) { force *= boost_multiplier; }
        return force;
    }

	//GameObject ClosestObject(){
	//	GameObject[] cands;
	//	cands = GameObject.FindGameObjectsWithTag ("GravityWell");
	//	GameObject closest = cands[0];
	//	float distance = Mathf.Infinity;
	//	Vector3 position = transform.position;
	//	foreach (GameObject cand in cands) {
	//		Vector3 diff = cand.transform.position - position;
	//		float curDistance = diff.sqrMagnitude;
	//		if (curDistance < distance) {
	//			closest = cand;
	//			distance = curDistance;
	//		}
	//	}
	//	return closest;
	//}

	//void PickUpObject(){}
		
    void translationalMovement(Rigidbody rb)
    {
        //Up and down
        if ((Input.GetAxis("Xbox_360_RightTrigger") != 0) != (Input.GetAxis("Xbox_360_LeftTrigger") != 0)) {
            Vector3 magnitude = rb.transform.up * (Input.GetAxis("Xbox_360_RightTrigger") - Input.GetAxis("Xbox_360_LeftTrigger"))  * Time.deltaTime;

            if (fixed_linear_speed == true)  {
                rb.velocity = applyBoost(magnitude * translation_velocity);
            } else {
                rb.AddForce(applyBoost(magnitude * translation_acceleration));
            }
        }
        else if (fixed_linear_speed == true){ rb.velocity = Vector3.zero; }

        // Forward, backward, left, right
        if (Input.GetAxis("Xbox_360_LeftJoystickX") != 0 || Input.GetAxis("Xbox_360_LeftJoystickY") != 0)
        {
            Vector3 magnitude_vector = (-Input.GetAxis("Xbox_360_LeftJoystickY") * rb.transform.forward + Input.GetAxis("Xbox_360_LeftJoystickX") * rb.transform.right) * Time.deltaTime;
            if (fixed_linear_speed == true)  {
                rb.velocity = applyBoost(magnitude_vector * translation_velocity) + rb.velocity;
            }
            else {
                rb.AddForce(applyBoost(magnitude_vector * translation_acceleration));
                //playSound(force);
            }
        }
    }
    void rotationalMovement(Rigidbody rb)
    {
        Vector3 force;
        if (allowRoll && (Input.GetButton("Xbox_360_LeftBumper") || Input.GetButton("Xbox_360_RightBumper")))  {
            float leftBoolInt = Input.GetButton("Xbox_360_LeftBumper") ? 1f : 0;
            float rightBoolInt = Input.GetButton("Xbox_360_RightBumper") ? 1f : 0;
            force = rb.transform.forward * 2f * (leftBoolInt - rightBoolInt) * Time.deltaTime ;
            if (fixed_angular_speed == true) {
                rb.angularVelocity = force * rotation_velocity * 0.4f;
            } else {
                rb.AddTorque(force * rotation_acceleration);
            }
        } else if (fixed_angular_speed == true) { rb.angularVelocity = Vector3.zero; }

        if (Input.GetAxis("Xbox_360_RightJoystickX") != 0 || Input.GetAxis("Xbox_360_RightJoystickY") != 0) {
            int invert = invert_yaxis ? -1 : 1;
            int allowpitch = allowPitch ? 1 : 0;
            float yaw = Input.GetAxis("Xbox_360_RightJoystickX");
            float pitch = Input.GetAxis("Xbox_360_RightJoystickY") * invert * allowpitch;
            force = (rb.transform.up * yaw + rb.transform.right * pitch)  * Time.deltaTime;

            if (fixed_angular_speed == true)  {
                //force = force * rotation_velocity  + rb.angularVelocity;
                //rb.angularVelocity = force;
                if (fix_to_floor) {
                    //float yaw = Input.GetAxis("Xbox_360_RightJoystickX") * 1;
                    transform.RotateAround(transform.position, transform.up, Input.GetAxis("Xbox_360_RightJoystickX") * rotation_velocity * Time.deltaTime);
                    //rb.angularVelocity = Vector3.zero;
                } else {
                    force = force * rotation_velocity + rb.angularVelocity;
                    rb.angularVelocity = force;
                }
            } else {
                rb.AddTorque(force * rotation_acceleration);
            }
        }
    }

void braking(Rigidbody rb){
        if (Input.GetButton("Xbox_360_B") || ((Input.GetAxis("Xbox_360_RightTrigger") != 0) && (Input.GetAxis("Xbox_360_LeftTrigger") != 0))) {
            rb.AddForce(applyBoost(rb.velocity.normalized) * -translation_acceleration * Time.deltaTime);
            rb.AddTorque(rb.angularVelocity.normalized * -rotation_acceleration * Time.deltaTime);
            if (rb.angularVelocity.magnitude < braking_rotation_threshold)
                rb.angularVelocity = Vector3.zero;
            if (rb.velocity.magnitude < braking_translation_threshold)
                rb.velocity = Vector3.zero;
            //Debug.Log(rb.velocity.magnitude);

        }
    }

 
    void move2()
    {
        rb = GetComponent<Rigidbody>();
  //      Vector3 cameraForwardDirection = GameObject.FindObjectOfType<Camera>().transform.forward;
		//Vector3 cameraRightDirection = GameObject.FindObjectOfType<Camera>().transform.right;
		//Vector3 cameraUpDirection = GameObject.FindObjectOfType<Camera>().transform.up;

        if (safety_collisions)  {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }else if (!fix_to_floor) {
            rb.freezeRotation = false;
        }
        if (Input.GetButton("Xbox_360_Back")) { UnityEngine.VR.InputTracking.Recenter(); }

        //Thrust left right forward backward
		if (fix_to_floor) {
            if (Input.GetButtonDown("Xbox_360_Y"))
            {
                rb.isKinematic = false;
                //				rb.detectCollisions = true;
                //				rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationX;
                rb.freezeRotation = false;

                fix_to_floor = false;
            }
            else {
                translationalMovement(rb);
                rotationalMovement(rb);

                braking(rb);


                //Fall to normal
                //Ray ray;
                RaycastHit hit;
                Vector3 myNormal = transform.up;
                float gravity = 1f;
                float slerpSpeed = 10;
                //			bool isGrounded = false;
                //float deltaGround = 0.2f;
                //			ray = Ray (transform.position, -transform.up);
                if (Physics.Raycast(transform.position, -transform.up, out hit))
                {
                    //				isGrounded = hit.distance <= distGround + deltaGround;
                    surfaceNormal = hit.normal;
                }
                else {
                    //				isGrounded = false;
                    surfaceNormal = transform.up;
                }
                myNormal = Vector3.Slerp(myNormal, surfaceNormal, slerpSpeed * Time.deltaTime).normalized;
                Vector3 myForward = Vector3.Cross(transform.right, myNormal);
                Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, slerpSpeed * Time.deltaTime);
                if (fixed_linear_speed == true) {
                    rb.velocity = rb.velocity + -gravity * myNormal;
                } else{
                    rb.AddForce(-gravity * myNormal);
                }

            }
		} else {
            translationalMovement(rb);
            rotationalMovement(rb);
            braking(rb);
	

			if (Input.GetButtonDown ("Xbox_360_Y")) {
				if (transition_ready == false) {
					transition_downtime = Time.time;
					pressTime = transition_downtime + transition_delta;
					transitioning = true;
				}
			}
			if (Input.GetButtonUp ("Xbox_360_Y")) {
				transitioning = false;
			}
			if (Time.time >= pressTime && transitioning == true && rb.angularVelocity.magnitude < braking_rotation_threshold) {
				transitioning = false;
				fix_to_floor = true;
				rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
				//closestGravityWell = ClosestObject ();
			}


           if (Input.GetButton ("Xbox_360_LeftStickClick")) {
				if (rb.angularVelocity.magnitude > braking_rotation_threshold)
					rb.AddTorque (rb.angularVelocity.normalized * -rotation_acceleration * Time.deltaTime);
				else {
					rb.angularVelocity = Vector3.zero;
					float y = rb.rotation.eulerAngles.y;
					rb.rotation = Quaternion.Slerp (rb.rotation, Quaternion.Euler (0, y, 0), 0.01f);
				}
			}
		    if (Input.GetButtonDown ("Xbox_360_RightStickClick")) {
				camera_rotation = GameObject.FindObjectOfType<Camera> ().transform.rotation;
			}
			if (Input.GetButton ("Xbox_360_RightStickClick")) {
				if (rb.angularVelocity.magnitude > braking_rotation_threshold)
					rb.AddTorque (rb.angularVelocity.normalized * -rotation_acceleration * Time.deltaTime);
				else {
					rb.angularVelocity = Vector3.zero;
					rb.rotation = Quaternion.Slerp (rb.rotation, camera_rotation, 0.01f);
				}
			}

            //if (Input.GetAxis ("Xbox_360_LeftTrigger") != 0) {
            //	//Zoom-in function + make cursor visible
            //	PickUpObject();
            //}
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
