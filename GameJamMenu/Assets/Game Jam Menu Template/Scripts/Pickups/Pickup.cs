using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//every item will be on a pedestal with spikes
//so they fall with spikes

[RequireComponent(typeof(BoxCollider2D))]

public class Pickup : MonoBehaviour {

	private BoxCollider2D _triggerCollider;


	// Use this for initialization
	void Awake () {
		_triggerCollider = GetComponent<BoxCollider2D> ();
		_triggerCollider.isTrigger = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Destroy (this.gameObject);
		}
	}
}
