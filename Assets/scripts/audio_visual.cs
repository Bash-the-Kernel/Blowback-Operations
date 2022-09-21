using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_visual : MonoBehaviour
{
    private const int SAMPLE_SIZE = 1024;

    public float rmsValue;
    public float dbValue;
    public float pitchValue;

    public float maxVisualScale = 25.0f;
    public float visualModifier = 50.0f;
    public float smoothSpeed = 10.0f;
    public float keepPercentage = 0.5f;

    public GameObject aud_vis_ball;

    private AudioSource Source;
    private float[] samples;
    private float[] spectrum;
    private float sampleRate;

    private Transform[] visualList;
    private float[] visualScale;
    private int amnVisual = 16;


    public Renderer enemy_render;
    private void Start()
    {
        Source = GetComponent<AudioSource>();
        samples = new float[SAMPLE_SIZE];
        spectrum = new float[SAMPLE_SIZE];
        sampleRate = AudioSettings.outputSampleRate;
        enemy_render = GetComponent<Renderer>();

        //spawnLine();
        spawnCircle();
    }

    private void spawnLine()
    {
        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];

        for (int i = 0; i < amnVisual; i++)
        {
            GameObject go = Instantiate(aud_vis_ball) as GameObject;
            visualList[i] = go.transform;
            visualList[i].position = Vector3.right * i + transform.position;
        }
    }

    private void spawnCircle()
    {
        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];

        Vector3 center = transform.position;
        float radius = 4.0f;

        for(int i = 0; i < amnVisual; i++)
        {
            float ang = i * 1.0f / amnVisual;
            ang = ang * Mathf.PI * 2;

            float x = Mathf.Cos(ang) * radius;
            float z = Mathf.Sin(ang) * radius;

            Vector3 pos = center + new Vector3(x, 0, z);
            Vector3 facing = new Vector3(x, 0, z);

            GameObject go = Instantiate(aud_vis_ball) as GameObject;
            go.transform.position = pos;
            go.transform.rotation = Quaternion.LookRotation(Vector3.up, facing);
            visualList[i] = go.transform;
        }
    }
    private void Update()
    {
        AnalyseSound();
        UpdateVisual();
        UpdatePos();
        is_enemy_visible();
        is_dead();
    }

    private void is_dead()
    {
        if (gameObject.GetComponent<player_control>())
        {
            if (!gameObject.GetComponent<player_control>().is_alive)
            {
                for (int i = 0; i < amnVisual; i++)
                {
                    Destroy(visualList[i].gameObject);
                }
            }

        }
    }
    private void is_enemy_visible()
    {
        //does the opposite of the enemy
        if (enemy_render.isVisible)
        {
            for (int i = 0; i < amnVisual; i++)
            {
                visualList[i].gameObject.GetComponentInChildren<Renderer>().enabled = false;
            }

        }
        else
        {
            for (int i = 0; i < amnVisual; i++)
            {
                visualList[i].gameObject.GetComponentInChildren<Renderer>().enabled = true;
            }
        }
    }

    private void is_alive()
    {
        for (int i = 0; i < amnVisual; i++)
        {
            visualList[i].gameObject.GetComponentInChildren<Renderer>().enabled = false;
        }
    }



    private void UpdatePos()
    {
        Vector3 center = transform.position;
        float radius = 4.0f;

        for (int i = 0; i < amnVisual; i++)
        {
            float ang = i * 1.0f / amnVisual;
            ang = ang * Mathf.PI * 2;

            float x = Mathf.Cos(ang) * radius;
            float z = Mathf.Sin(ang) * radius;

            Vector3 pos = center + new Vector3(x, 0, z);
            visualList[i].transform.position = pos;
        }
    }

    private void UpdateVisual()
    {
        int visualIndex = 0;
        int spectrumIndex = 0;
        int averageSize = (int)((SAMPLE_SIZE * keepPercentage) / amnVisual);

        while(visualIndex < amnVisual)
        {
            int j = 0;
            float sum = 0;
            while(j < averageSize)
            {
                sum += spectrum[spectrumIndex];
                spectrumIndex++;
                j++;
            }

            float scaleY = sum / averageSize * visualModifier;
            visualScale[visualIndex] -= Time.deltaTime * smoothSpeed;
            if(visualScale[visualIndex] < scaleY)
            {
                visualScale[visualIndex] = scaleY;
            }


            if(visualScale[visualIndex] > maxVisualScale)
            {
                visualScale[visualIndex] = maxVisualScale;
            }

            visualList[visualIndex].localScale = Vector3.one + Vector3.up * visualScale[visualIndex];
            visualIndex++;
        }
    }
    private void AnalyseSound()
    {
        Source.GetOutputData(samples, 0);

        // get the rms

        float sum = 0;
        for(int i = 0; i < SAMPLE_SIZE; i++)
        {
            sum = samples[i] * samples[i];
        }
        rmsValue = Mathf.Sqrt(sum/SAMPLE_SIZE);

        // get the db value

        dbValue = Mathf.Log10(rmsValue / 0.1f);

        // get sound spectrum

        Source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    }
}
