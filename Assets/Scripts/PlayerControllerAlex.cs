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

    private float gravity = 1f;
    private float slerpSpeed = 10;
    private float deltaGround = 0.1f;
    private float distGround = 1f;
    private float maxGroundDistance = 10f;
    private bool detach = false;
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
            move();
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
		
    void translationalMovement(Rigidbody rb, bool touching_floor = false)
    {
        //Up and down
        float rt = Input.GetAxis("Xbox_360_RightTrigger");
        float lt = Input.GetAxis("Xbox_360_LeftTrigger");
        float lsx = Input.GetAxis("Xbox_360_LeftJoystickX");
        float lsy = Input.GetAxis("Xbox_360_LeftJoystickY");
        if (((rt != 0) != (lt != 0)) || (lsx != 0 || lsy != 0)) {
            Vector3 magnitude = (rb.transform.up * (rt - lt) + (-lsy * rb.transform.forward + lsx * rb.transform.right)) * Time.deltaTime;
            if (fixed_linear_speed == true || (fix_to_floor && touching_floor))  {
                rb.velocity = applyBoost(magnitude * translation_velocity);
            } else {
                rb.AddForce(applyBoost(magnitude * translation_acceleration));
            }
            playSound(magnitude,true);
        }
        else if (fixed_linear_speed == true || (fix_to_floor && touching_floor)) { rb.velocity = Vector3.zero; }
    }
    void rotationalMovement(Rigidbody rb, bool touching_floor = false)
    {
        Vector3 force;
        float rsx = Input.GetAxis("Xbox_360_RightJoystickX");
        int invert = invert_yaxis ? -1 : 1;
        float rsy = allowPitch ? invert*Input.GetAxis("Xbox_360_RightJoystickY"): 0;
        float leftBoolInt = Input.GetButton("Xbox_360_LeftBumper") ? 1f : 0;
        float rightBoolInt = Input.GetButton("Xbox_360_RightBumper") ? 1f : 0;
        float bumper_value = allowRoll ? (leftBoolInt - rightBoolInt) : 0;
        if ( (bumper_value != 0 || rsx != 0 || rsy!=0)) {
            force = (rb.transform.forward * 2f * bumper_value + (rb.transform.up * rsx + rb.transform.right * rsy)) * Time.deltaTime ;
            if (fix_to_floor && touching_floor)
            {
                transform.RotateAround(transform.position, transform.up, rsx * rotation_velocity * Time.deltaTime);
            }else if (fixed_angular_speed == true){
                rb.angularVelocity = force * rotation_velocity;

            }else {
                rb.AddTorque(force * rotation_acceleration);
            }
            playSound(force, false);

        }
        else if (fixed_angular_speed == true || (fix_to_floor && touching_floor)) { rb.angularVelocity = Vector3.zero; }
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

 
    void move() {
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
            if (Input.GetButtonDown("Xbox_360_Y") || detach == true)
            {
                detach = false;
                rb.isKinematic = false;
                rb.freezeRotation = false;
                fix_to_floor = false;
            }else {

                //Fall to normal
                //Ray ray;
                RaycastHit hit;
                Vector3 myNormal = transform.up;
                bool isGrounded = false;
                if (Physics.Raycast(transform.position, -transform.up, out hit))
                {
                    isGrounded = hit.distance <= distGround + deltaGround;
                    detach = hit.distance >= distGround + deltaGround + maxGroundDistance;
                    surfaceNormal = hit.normal;
                    //Debug.Log("is grounded value: " + (hit.distance - (distGround + deltaGround)));
                } else {
                    isGrounded = false;
                    detach = true;
                    surfaceNormal = transform.up;
                }
                translationalMovement(rb, isGrounded);
                rotationalMovement(rb, isGrounded);
                braking(rb);
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
			if ((fixed_angular_speed == false && fixed_linear_speed == false) && Time.time >= pressTime && transitioning == true && rb.angularVelocity.magnitude < braking_rotation_threshold) {
				transitioning = false;
				fix_to_floor = true;
				rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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

    void playSound(Vector3 force,bool isLinear)
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
