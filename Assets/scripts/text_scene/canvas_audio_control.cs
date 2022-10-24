using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class canvas_audio_control : MonoBehaviour
{
    private GameObject text;
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
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
            PlayGame();
        }
    }

}
