using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    
    float myTimeScale = 1;
    bool isPaused = false;
    public GUISkin guiSkin;
    GUIStyle pauseMenuButton;
    float nativeVerticalResolution = 768;
    
    public bool getPaused()
    {
        return isPaused;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                //grande botao de pausa
                if (touch.position.y > 800 && !isPaused)
                {
                    pause();
                }
            }
        }

        if (Input.GetKeyDown("escape") && !isPaused)
        {
            pause();
        }
        else if (Input.GetKeyDown("escape") && isPaused)
        {
            print("Unpaused");
            Time.timeScale = myTimeScale;
            isPaused = false;
        }
    }

    void pause()
    {
        print("Paused");
        myTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        isPaused = true;
    }

    void OnGUI()
    {
        pauseMenuButton = new GUIStyle("button");

        pauseMenuButton.fontSize = 50;

        // Set up gui skin
        GUI.skin = guiSkin;

        // Our GUI is laid out for a 1600 x 900 pixel display. The next line makes sure it rescales nicely to other resolutions.
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(Screen.height / nativeVerticalResolution, Screen.height / nativeVerticalResolution, 1));

        if (isPaused)
        {
            RenderSettings.fogDensity = 1;
            if (GUI.Button(new Rect(200, 500, 350, 150), "Quit", pauseMenuButton))
            {
                print("Quit to Menu");
                Time.timeScale = 1f;
                isPaused = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            if (GUI.Button(new Rect(200, 300, 350, 150), "Restart", pauseMenuButton))
            {
                print("Restart");
                Time.timeScale = 1f;
                isPaused = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
            if (GUI.Button(new Rect(200, 100, 350, 150), "Continue", pauseMenuButton))
            {
                print("Continue");
                Time.timeScale = myTimeScale;
                isPaused = false;
            }
        }
        else
        { // !isPaused
            RenderSettings.fogDensity = 0;
        }
    }
}
