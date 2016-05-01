function LateUpdate () {
   transform.LookAt(GetComponent.<Camera>().main.transform.position, transform.up);
   //transform.Rotate( Vector3.left*-90);
}