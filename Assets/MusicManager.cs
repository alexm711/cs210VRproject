using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	public AudioClip[] songs;
	int currentSong = 0;
	bool changeSong = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		AudioSource audio = GetComponent<AudioSource> ();
		if (audio.isPlaying == false || changeSong==true) {
			currentSong++;
			audio.clip = songs[currentSong % songs.Length];
			audio.Play ();
			changeSong = false;
		}
	}
	public void ChangeSong(){
		changeSong = true;
	}	
	public void SetVolume(float val){
		Debug.Log("Volume val: " + val);

		GetComponent<AudioSource>().volume = val;
	}
}
