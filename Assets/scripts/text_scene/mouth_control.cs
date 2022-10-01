using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouth_control : MonoBehaviour
{
    public Sprite mouth_open;
    public Sprite mouth_close;
    public float start_delay;
    public float delay_between_mouth;
    private GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("cut_scene_text");
        StartCoroutine(mouth_spasm());
    }

    IEnumerator mouth_spasm()
    {
        yield return new WaitForSeconds(start_delay);
        print(!text.GetComponent<text_typing>().is_done);
        while (!text.GetComponent<text_typing>().is_done)
        {
            print("oh god o fuck");
            if(gameObject.GetComponent<Image>().sprite == mouth_open)
            {
                gameObject.GetComponent<Image>().sprite = mouth_close;
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = mouth_open;
            }
            yield return new WaitForSeconds(delay_between_mouth);
        }
        gameObject.GetComponent<Image>().sprite = mouth_close;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
