using UnityEngine;
using System.Collections;

public class DestoryDoorOnConversationEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnConversationEnd(Transform actor){
		GameObject Door = GameObject.Find ("Door");
		Debug.Log(string.Format("Ending Conversatio1n"));
		if (Door != null) {
			Destroy (Door);
			Debug.Log(string.Format("Ending Conversatio 2n"));
			Destroy (this);
		}
	}
}

