using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlatform : MonoBehaviour {

    public LayerMask ignoreLayer;

    public int yValueToKillSelf = -7;

	void Start () {

        Physics2D.IgnoreLayerCollision(9, 16);

	}
	
	void Update () {

        //kill when below certain y value
        if (transform.position.y < yValueToKillSelf)
            DestroyPlatform();

	}

    void DestroyPlatform() {

        Destroy(gameObject);

    }

}
