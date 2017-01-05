using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {

	public Vector3 waterCenter;
	public float waterScale;
	public float offset;
	bool above=false;
	public bool canSink;
	public AudioClip underWaterN;
	public AudioClip aboveWaterN;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource.PlayOneShot (underWaterN);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 d = transform.position - waterCenter;
		float yPos = Mathf.Sin (d.x * 2 * Mathf.PI / 16.0f + Time.time);
		yPos += Mathf.Sin (d.z * 2 * Mathf.PI / 16.0f + Time.time);
		yPos += offset;

		if (canSink) {
			// above the water
			//if (yPos < transform.position.y-.5f) {
			//	transform.position -= new Vector3 (0, .01f, 0);
			//	return;
			//} else 
			if (Mathf.Abs (yPos - transform.position.y) < .165f || yPos < transform.position.y) {
				Debug.Log ("Above");
				if (!above) {
					float time = audioSource.time;
					audioSource.Stop ();
					audioSource.PlayOneShot (aboveWaterN);
					audioSource.time = time;
					above = true;
				}
				transform.position = new Vector3 (transform.position.x, yPos, transform.position.z);
				return;
			} else {
				if (above) {
					float time = audioSource.time;
					audioSource.Stop ();
					audioSource.PlayOneShot (underWaterN);
					audioSource.time = time;
					above = false;
				}
				Debug.Log ("Noise");
				transform.position -= Vector3.up * .02f;
			}
		} else {
			transform.position = new Vector3 (transform.position.x, yPos, transform.position.z);
		}
	}
}
