using UnityEngine;
using System.Collections;

public class JetpackSoundManager : MonoBehaviour {
    public static JetpackSoundManager instance = null;
    public float multiple = 0.1f;


    // Use this for initialization
    AudioSource FUR;
    AudioSource FUL;
    AudioSource BUR;
    AudioSource BUL;
    AudioSource FLR;
    AudioSource FLL;
    AudioSource BLR;
    AudioSource BLL;
    public float tol = 0.2f;
    public float tol2 = 0.2f;
    private Vector3 prev_rot;
    private Vector3 prev_lin;
    void Start () {

        FUR = GameObject.Find("FUR").GetComponent<AudioSource>();
        FUL = GameObject.Find("FUL").GetComponent<AudioSource>();
        BUR = GameObject.Find("BUR").GetComponent<AudioSource>();
        BUL = GameObject.Find("BUL").GetComponent<AudioSource>();
        FLR = GameObject.Find("FLR").GetComponent<AudioSource>();
        FLL = GameObject.Find("FLL").GetComponent<AudioSource>();
        BLR = GameObject.Find("BLR").GetComponent<AudioSource>();
        BLL = GameObject.Find("BLL").GetComponent<AudioSource>();
        FUR.Play();
        FUL.Play();
        FLL.Play();
        FLR.Play();
        BUR.Play();
        BUL.Play();
        BLR.Play();
        BLL.Play();
        //Debug.Log("HIU");

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
    public void PlayJetpack(Vector3 linforce,  bool isLinear)
    {
        if (linforce == Vector3.zero)
        {
            //Debug.Log("Don't do anything'");
            if (!FUR.isPlaying) FUR.Stop();
            if (!FUL.isPlaying) FUL.Stop();
            if (!BUR.isPlaying) BUR.Stop();
            if (!BUL.isPlaying) BUL.Stop();
            if (!FLR.isPlaying) FLR.Stop();
            if (!FLL.isPlaying) FLL.Stop();
            if (!BLR.isPlaying) BLR.Stop();
            if (!BLL.isPlaying) BLL.Stop();

        }
        float linposx = ((linforce.x > tol) ? linforce.x : 0);
        float linnegx = ((-linforce.x > tol) ? -linforce.x : 0);
        float linposy = ((linforce.y > tol) ? linforce.y : 0);
        float linnegy = ((-linforce.y > tol) ? -linforce.y : 0);
        float linposz = ((linforce.z > tol) ? linforce.z : 0);
        float linnegz = ((-linforce.z > tol) ? -linforce.z : 0);
        //float rotposx = ((angforce.x > tol) ? angforce.x : 0);
        //float rotnegx = ((-angforce.x > tol) ? -angforce.x : 0);
        //float rotposy = ((angforce.y > tol) ? angforce.y : 0);
        //float rotnegy = ((-angforce.y > tol) ? -angforce.y : 0);
        //float rotposz = ((angforce.z > tol) ? angforce.z : 0);
        //float rotnegz = ((-angforce.z > tol) ? -angforce.z : 0);

        changeVolume(FUR, linnegx + linnegy + linnegz);
        changeVolume(FUL, linposx + linnegy + linnegz );
        changeVolume(BUR, linnegx + linnegy + linposz );
        changeVolume(BUL, linposx + linnegy + linposz );
        changeVolume(FLR, linnegx + linposy + linnegz );
        changeVolume(FLL, linposx + linposy + linnegz );
        changeVolume(BLR, linnegx + linposy + linposz );
        changeVolume(BLL, linposx + linposy + linposz );
    }
    void changeVolume(AudioSource audio, float volume)
    {
        volume *= multiple;
        //Debug.Log("testing: " + volume + " is greater than " + tol2 + "is : " + (volume > tol2));
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
        //Debug.Log("PLAYING: " + audio + "at volume: " + volume);

    }
}
