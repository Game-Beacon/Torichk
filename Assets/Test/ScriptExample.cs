using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WaitForSeconds example.
//
// Create three cubes. Press the "Move cubes normally" button to animate them.
// Press the "Move cubes quickly" button to animate them faster.
// Have WaitForSeconds wait for different amounts of time. The cubes
// move upward or downward one at a time.

public class ScriptExample : MonoBehaviour
{
    private GameObject[] gameObjects;
    private int numberOfGameObjects = 3;
    private bool exampleInUse = false;
    private int width, height;
    private float xPos, yPos;
    private float xSize, ySize;
    private int fontSize;

    void Awake()
    {
        gameObjects = new GameObject[numberOfGameObjects];

        for (int i = 0; i < numberOfGameObjects; i++)
        {
            // Create a cube, place in the world, and set the name.
            gameObjects[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObjects[i].transform.position = new Vector3(-1.5f + i * 1.5f, 0.5f, 0.0f);
            gameObjects[i].name = i.ToString();
        }

        // The size and position of the buttons.
        width = Screen.width;
        height = Screen.height;
        xPos = width - width / 2.5f;
        yPos = height - height / 5;
        xSize = 250 * width / 800;
        ySize = 40 * height / 600;
        fontSize = 24 * width / 800;

        // Place the camera in a good location and looking towards the cubes.
        Camera.main.transform.position = new Vector3(2.6f, 2.0f, 2.5f);
        Camera.main.transform.localEulerAngles = new Vector3(25.0f, -135.0f, 0.0f);
    }

    // Move selected cube up or down.
    void ChangeSize(int gameObject, float x)
    {
        if (gameObjects[gameObject].transform.position.y > 0.95f)
        {
            gameObjects[gameObject].transform.position = new Vector3(x, 0.5f, 0.0f);
        }
        else
        {
            gameObjects[gameObject].transform.position = new Vector3(x, 1.0f, 0.0f);
        }
    }

    IEnumerator Example()
    {
        // Start control of the three cubes.
        exampleInUse = true;

        // Move the first cube up or down.
        yield return new WaitForSeconds(0.5f);
        ChangeSize(0, -1.5f);

        // Move the second cube up or down.
        yield return new WaitForSeconds(1.5f);
        ChangeSize(1, 0.0f);

        // Move the third cube up or down.
        yield return new WaitForSeconds(0.75f);
        ChangeSize(2, 1.5f);

        // Pause for a second.
        yield return new WaitForSeconds(1.0f);

        exampleInUse = false;
    }

    void OnGUI()
    {
        GUI.enabled = !exampleInUse;
        GUI.skin.button.fontSize = 24 * width / 800;

        if (GUI.Button(new Rect(xPos, yPos, xSize, ySize), "Move cubes normally"))
        {
            Time.timeScale = 1.0f;
            StartCoroutine(Example());
        }

        // Set the speed of the cubes to be more fast than normal.
        if (GUI.Button(new Rect(xPos, yPos + ySize * 1.1f, xSize, ySize), "Move cubes quickly"))
        {
            Time.timeScale = 4.0f;
            StartCoroutine(Example());
        }
    }
}