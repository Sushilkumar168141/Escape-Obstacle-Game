using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeBackgroundColor : MonoBehaviour
{
    public Color color1 = Color.red;
    public Color color2 = Color.blue;
    public float duration = 3.0f;
    public float t;
    public Camera cam;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        color1 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        color2 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        StartCoroutine(changeColor());
    }

    // Update is called once per frame
    void Update()
    {
        /*float t = Mathf.PingPong(Time.time, duration) / duration;
        cam.backgroundColor = Color.Lerp(color1, color2, t);*/
        /*color1 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        color2 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));*/

        //t += Time.deltaTime / duration;
        t = Mathf.PingPong(Time.time, duration) / duration;
        cam.backgroundColor = Color.Lerp(color1, color2, t);
        //Color color = Color.Lerp(color1, color2, t);
        //Camera.main.backgroundColor = color;
    }

    IEnumerator changeColor()
    {
        
        while (true)
        {
            color1 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            color2 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            /*t = Mathf.PingPong(Time.time, duration) / duration;
            color = Color.Lerp(color1, color2, t);
            Camera.main.backgroundColor = color;*/
            yield return new WaitForSeconds(5f);
        }
    }
}
