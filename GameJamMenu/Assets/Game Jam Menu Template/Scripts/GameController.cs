using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    GameController instance;

    public static string state = "playing";

    void Start () {

        if (instance == null)
            instance = this;

	}

}
