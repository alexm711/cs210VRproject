using UnityEngine;
using System.Collections;

public class JetpackSoundManager : MonoBehaviour {
    public static JetpackSoundManager instance = null;
    public float multiple = 10f;


    // Use this for initialization
    AudioSource Up;
    AudioSource Down;
    AudioSource Left;
    AudioSource Right;
    void Start () {
        Up = GameObject.Find("Up").GetComponent<AudioSource>() ;
        Down = GameObject.Find("Down").GetComponent<AudioSource>();
        Left = GameObject.Find("Left").GetComponent<AudioSource>();
        Right = GameObject.Find("Right").GetComponent<AudioSource>();
        Up.Play();
        Down.Play();
        Left.Play();
        Right.Play();
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
	
	}
    public void PlayLatSound(Vector3 val)
    {
        //AudioSource audio = Left.GetComponent<AudioSource>();
        //Up.PlayOneShot(clip, 1f);
        //GetComponent<AudioSource>().volume = val;

        //if (val.x > 0)
        //{
        //    Left.volume = val.x;
        //    Right.volume = 0;

        //}
        //else {
        //    Right.volume = val.x;
        //    Left.volume = 0;
        //}
        //if (val.y > 0)
        //{
        //    Up.volume = val.y;
        //}else
        //{
        //    Down.volume = val.y;

        //}
        val = val * multiple;
        float absz = val.z > 0.1f ? val.z : -val.z;
        Left.volume = absz + (val.x > 0.1f ?  val.x  : 0f);
        Right.volume = absz +  (val.x < -0.1f ?  -val.x  : 0f);
        Down.volume = absz +  (val.y > 0.1f ? val.y : 0f);
        Up.volume = absz + (val.y < -0.1f ? - val.y : 0f);
        //GetComponent<AudioSource>().volume = val;
    }
}
