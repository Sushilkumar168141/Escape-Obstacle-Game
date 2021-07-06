using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void goToHomeScene() {
        SceneManager.LoadScene(0);
    }

    public void goToHelpScene() {
        SceneManager.LoadScene(3);
    }
}
