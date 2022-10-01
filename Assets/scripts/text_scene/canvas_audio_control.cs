using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvas_audio_control : MonoBehaviour
{
    private GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("cut_scene_text");
    }

    // Update is called once per frame
    void Update()
    {
        if (text.GetComponent<text_typing>().is_done)
        {
            gameObject.GetComponent<AudioSource>().Pause();
        }
    }
}
