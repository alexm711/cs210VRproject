using UnityEngine;
using System.Collections;

public class JetpackSoundManager : MonoBehaviour {
    public static JetpackSoundManager instance = null;
    public float multiple = 10f;


    // Use this for initialization
    public AudioSource FUR;
    public AudioSource FUL;
    public AudioSource FLR;
    public AudioSource FLL;
    public AudioSource BUR;
    public AudioSource BUL;
    public AudioSource BLR;
    public AudioSource BLL;
    public float tol = 0.2f;
    public float tol2 = 0.2f;
    private Vector3 prev_rot;
    private Vector3 prev_lin;
    void Start () {

    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
 //   void Update () {
	
	//}
    //float ons(float val)
    //{
    //    ((val > tol) ? val : 0) ;
    //}
    //float fourside (float val1, float val2, float val3, float val4)
    //{
    //    return ons(val1) + ones(val2) + ons(val3) + ons(val4);
    //}
    public void PlayJetpack(Vector3 linforce, Vector3 angforce, bool isAngular, bool isLinear)
    {
        float linposx = ((-linforce.x > tol) ? -linforce.x : 0);
        float linnegx = ((-linforce.x > tol) ? -linforce.x : 0);
        float linposy = ((linforce.y > tol) ? linforce.y : 0);
        float linnegy = ((-linforce.y > tol) ? -linforce.y : 0);
        float linposz = ((linforce.z > tol) ? linforce.z : 0);
        float linnegz = ((-linforce.z > tol) ? -linforce.z : 0);
        float rotposx = ((angforce.x > tol) ? angforce.x : 0);
        float rotnegx = ((-angforce.x > tol) ? -angforce.x : 0);
        float rotposy = ((angforce.y > tol) ? angforce.y : 0);
        float rotnegy = ((-angforce.y > tol) ? -angforce.y : 0);
        float rotposz = ((angforce.z > tol) ? angforce.z : 0);
        float rotnegz = ((-angforce.z > tol) ? -angforce.z : 0);

        changeVolume(FUR, linnegx + linnegy + linnegz );
        changeVolume(FUL, linposx + linnegy + linnegz);
        changeVolume(FLR, linnegx + linposy + linnegz);
        changeVolume(FLL, linposx + linposy + linnegz);
        changeVolume(BUR, linnegx + linnegy + linposz );
        changeVolume(BUL, linposx + linnegy + linposz);
        changeVolume(BLR, linnegx + linposy + linposz );
        changeVolume(BLL, linposx + linposy + linposz );

        //FUR.volume = ((linforce.x > tol) ? linforce.x : 0) + angforce.
        // usedd to be val
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
        //if (linforce.x > 0)
        //Up.Play();
        //Down.Play();
        //Left.Play();
        //Right.Play();
        //val = val * multiple;
        //float absz = val.z > 0.1f ? val.z : -val.z;
        //Left.volume = absz + (val.x > 0.1f ?  val.x  : 0f);
        //Right.volume = absz +  (val.x < -0.1f ?  -val.x  : 0f);
        //Down.volume = absz +  (val.y > 0.1f ? val.y : 0f);
        //Up.volume = absz + (val.y < -0.1f ? - val.y : 0f);
        //GetComponent<AudioSource>().volume = val;
    }
    void changeVolume(AudioSource audio, float volume)
    {
        if (volume < tol2)
        {
            audio.Stop();
            return;
        }
        if (!audio.isPlaying)
        {
            audio.Play();
        }
        audio.volume = volume;
    }

    //bool[] magchange( Vector3 v1, Vector3 v2)
    //{
    //    bool[] array = new bool[3];
    //    array[0] = (Mathf.Abs(v1.x - v2.x) > tol);
    //    array[1] = (Mathf.Abs(v1.y - v2.y) > tol);
    //    array[2] = (Mathf.Abs(v1.z - v2.z) > tol);
    //    return array;
    //}

}
