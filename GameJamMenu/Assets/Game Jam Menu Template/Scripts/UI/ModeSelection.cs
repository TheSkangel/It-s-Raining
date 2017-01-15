using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelection : MonoBehaviour {

    public Level level;
    public MenuNavigation menuNav;

    public Transform[] selections;

    public Transform selectionBox;

    private int currentIndex = 0;

    private float scrollTimer;
    private float scrollSpeed = 0.2f;

	void Start () {

        selectionBox.position = selections[currentIndex].position;

        scrollTimer = Time.time;

	}
	
	void Update () {

        float xAxis = Input.GetAxisRaw("p1Horizontal");

        if(Time.time > scrollTimer) {

            if (xAxis > 0) {
                MoveSelection(1);
            }

            if (xAxis < 0) {
                MoveSelection(-1);
            }

        }

        if(GameController.state == "menu") {

            if (Input.GetButtonDown("p1Jump")) {

                SelectGameMode();

            }

        }

        if(GameController.state == "gameover") {

            if (Input.GetButtonDown("p1Jump")) {

                SelectGameOverOption();

            }

        }

        

	}

    void MoveSelection(int direction) {

        currentIndex += direction;

        if (currentIndex > selections.Length - 1)
            currentIndex = 0;

        selectionBox.position = selections[currentIndex].position;

        //set the scroll timer
        scrollTimer = Time.time + scrollSpeed;

    }

    void SelectGameMode() {

        int realNbOfPlayers = currentIndex + 2;

        //set the numebr of players in this game
        GameController.SetNumberOfPlayers(realNbOfPlayers);

        menuNav.StartGame();

        level.SpawnPlayers(realNbOfPlayers);

        currentIndex = 0;

    }

    void SelectGameOverOption() {

        if (currentIndex == 0) {

            level.RestartGame();

        } else {

            menuNav.GoToScreen("Main Menu");

        }

    }

}
