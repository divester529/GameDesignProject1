using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioControl : MonoBehaviour
{
    float volume=1.0f;

    bool isMuted = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Equals))
        {
                volume += 0.1f;

            gameObject.GetComponent<AudioSource>().volume=volume;
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            if (volume >= 0.0f)
                volume -= 0.1f;

            gameObject.GetComponent<AudioSource>().volume=volume;
        }
        else if (Input.GetKey(KeyCode.Alpha0))
        {
          if(isMuted) {
            gameObject.GetComponent<AudioSource>().mute=false;
            isMuted = false;
          } else {
            gameObject.GetComponent<AudioSource>().mute=true;
            isMuted = true;
          }

        }

    }

    void OnGUI()
    {
        GUI.HorizontalSlider(new Rect(25, 25, 200, 70), 10, 0.0f, 1.0f);
    }
}
