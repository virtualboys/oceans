using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	public float maxDist;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (Camera.main.transform.position);
		transform.RotateAround (transform.right, Mathf.PI/20);
		//transform.Rotate (new Vector3 (-90, 180, 0));

		Vector3 d = Camera.main.transform.position - transform.position;
		d.y = transform.position.y;
		float dMag = d.magnitude;
		if (dMag > maxDist) {
			Debug.Log ("too Far");
			d.Normalize ();
			d *= maxDist;
			Vector3 newPos = Camera.main.transform.position - d;
			newPos.y = transform.position.y;
			transform.position = newPos;
		}
	}
}
