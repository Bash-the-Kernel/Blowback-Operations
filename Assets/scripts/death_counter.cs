using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class death_counter : MonoBehaviour
{
    public float deaths;
    public GameObject Tally;
    public float tally_num;
    private List<GameObject> Tally_list = new List<GameObject>();
    public Sprite n_2_;
    public Sprite n_3_;
    public Sprite n_4_;
    public Sprite n_5_;
    // Start is called before the first frame update
    void Start()
    {
        deaths = 0;
        tally_num = 0;
    }

    IEnumerator score_anim()
    {
        RectTransform rectTransform = Tally_list[Tally_list.Count - 1].GetComponent<RectTransform>();
        for(float i = 5; i >= 1; i -= 0.05f)
        {
            rectTransform.localScale = new Vector3(i, i, i);
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if ((deaths-1)%5 == 0  && tally_num < deaths)
        {
            GameObject Tally_inst = Instantiate(Tally, gameObject.transform);
            Tally_inst.GetComponent<RectTransform>().anchoredPosition = new Vector3((tally_num/5)*30, 0, 0);
            Tally_list.Add(Tally_inst);
            tally_num++;
            StartCoroutine(score_anim());
        }
        else if((deaths-2)%5 == 0 && tally_num < deaths )
        {
            tally_num++;
            print("AUUUGHHH");

            Tally_list[Tally_list.Count - 1].GetComponent<Image>().sprite = n_2_;
            StartCoroutine(score_anim());
        }
        else if ((deaths - 3) % 5 == 0 && tally_num < deaths)
        {
            tally_num++;
            Tally_list[Tally_list.Count - 1].GetComponent<Image>().sprite = n_3_;
            StartCoroutine(score_anim());
        }
        else if ((deaths - 4) % 5 == 0 && tally_num < deaths)
        {
            tally_num++;
            Tally_list[Tally_list.Count - 1].GetComponent<Image>().sprite = n_4_;
            StartCoroutine(score_anim());
        }
        else if ((deaths - 5) % 5 == 0 && tally_num < deaths && deaths != 0)
        {
            tally_num++;
            Tally_list[Tally_list.Count - 1].GetComponent<Image>().sprite = n_5_;
            StartCoroutine(score_anim());
        }
    }
}
