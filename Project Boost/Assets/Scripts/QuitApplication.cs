using UnityEngine;

/*
 * Quits the game when the Escape key is pressed.
 */
public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
