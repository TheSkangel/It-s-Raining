using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public MenuNavigation menuNav;

	void Update () {

        if (Input.GetButtonDown("p1Jump"))
            menuNav.GoToScreen("Game Mode");

	}

}
