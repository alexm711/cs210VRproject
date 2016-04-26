using UnityEngine;
using System.Collections;

public class Follow_Torso : MonoBehaviour {

    // Use this for initialization
    GameObject Player_torso;
	void Start () {
        Player_torso = GameObject.Find("Ellie");

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = Player_torso.transform.forward;
        //Player_torso.transform.position;
        transform.position = Player_torso.transform.position + direction;
        transform.rotation = Player_torso.transform.rotation;
        //transform.LookAt(Player_torso.transform);

        //transform.rotation = Quaternion.LookRotation(transform.position - Player_torso.transform.position) ;

    }
}
