using UnityEngine;
using System.Collections;

public class EthanScript : MonoBehaviour {
    public Animation anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        anim.playAutomatically = false;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
