using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    GameController instance;

    public static string state = "menu";

    public static int numberOfPlayers;
    public static int playersAlive;

    public MenuNavigation menuNav;
    public Level level;

    void Start () {

        if (instance == null)
            instance = this;

	}

    public static void SetNumberOfPlayers(int nb) {

        numberOfPlayers = nb;

        playersAlive = nb;

    }

}
