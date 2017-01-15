using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour {

    public GameObject deahtEffect;


    public void AnimateDeath() {

        GameObject explode = Instantiate(deahtEffect, transform.position, Quaternion.identity) as GameObject;

    }
    

}
