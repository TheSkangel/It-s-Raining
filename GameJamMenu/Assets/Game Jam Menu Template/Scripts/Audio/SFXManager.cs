using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringToInt {

    public string name;
    public int key;

}

public class SFXManager : MonoBehaviour {

    public StringToInt[] keys;

    public AudioSource[] sounds;

    public static SFXManager instance;

    void Start() {

        if (instance == null)
            instance = this;

    }

    public void PlaySound(string name) {

        int index = -1;

        for(int i = 0; i < keys.Length; i++) {

            if(keys[i].name == name) {

                index = keys[i].key;

            }

        }

        if(index != -1) {

            sounds[index].Play();
            print("played " + sounds[index]);

        }

    }

}
