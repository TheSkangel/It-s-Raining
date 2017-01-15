using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public MenuNavigation menuNav;
    public GameObject[] players;

    public void SpawnPlayers(int nb) {

        for(int i = 0; i < nb; i++) {

            GameObject newPlayer = Instantiate(players[i]);

        }

    }

    public void RestartGame() {

        DestroyPlayers();

        GameController.state = "playing";

        SpawnPlayers(GameController.numberOfPlayers);

    }

    void DestroyPlayers() {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++) {

            Destroy(players[i]);

        }

    }

}
