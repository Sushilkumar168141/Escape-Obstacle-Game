using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void quit() {
        Debug.Log("Application Quitted");
        Application.Quit();
    }
}
