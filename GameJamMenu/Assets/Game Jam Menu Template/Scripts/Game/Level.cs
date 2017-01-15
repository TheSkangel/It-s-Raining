using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public MenuNavigation menuNav;
    public GameObject[] players;

    void Update() {
        
        if (GameController.state != "playing")
            return;

    }

    public void ShowGameOverMenu() {

        GameController.state = "gameover";

        menuNav.GoToScreen("Game Over");

    }

    public void SpawnPlayers(int nb) {

        for(int i = 0; i < nb; i++) {

            GameObject newPlayer = Instantiate(players[i]);

        }

        GameController.playersAlive = nb;

    }

    public void RestartGame() {
        
        DestroyPlayers();

        SpawnPlayers(GameController.numberOfPlayers);

        GameController.state = "playing";

    }

    public void DestroyPlayers() {
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        for(int i = 0; i < players.Length; i++) {

            Destroy(players[i]);

        }

    }

}
