using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

    public GameObject leftWall;
    public GameObject rightWall;

    public Vector3 leftWallInitialPosition;
    public Vector3 rightWallInitialPosition;


    public float wallCooldown = 10f;

    public float closeInTime = 2f;
    public float closeInSpeed = 10f;

    private float wallTimer;

    string state = "idle";

    void Start () {

        wallTimer = Time.time + wallCooldown;

    }


    void Update () {

        if (GameController.state != "playing")
            return;

        if(state == "idle") {

            if (Time.time > wallTimer) {

                StartClosing();

                wallTimer = Time.time + closeInTime;

            }

        } else if(state == "closing") {

            Close();

            if (Time.time > wallTimer) {

                StopClosing();

                wallTimer = Time.time + wallCooldown;

            }

        }

        

	}

    void StartClosing() {

        state = "closing";

    }

    void Close() {

        leftWall.transform.Translate(new Vector3(1, 0, 0) * closeInSpeed * Time.deltaTime);
        rightWall.transform.Translate(new Vector3(-1, 0, 0) * closeInSpeed * Time.deltaTime);

    }

    void StopClosing() {

        state = "idle";

    }

    public void ResetWalls() {

        state = "idle";
        wallTimer = Time.time + wallCooldown;

        leftWall.transform.position = leftWallInitialPosition;
        rightWall.transform.position = rightWallInitialPosition;

    }

}
