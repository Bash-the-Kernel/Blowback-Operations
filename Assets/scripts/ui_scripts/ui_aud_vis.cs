using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_aud_vis : MonoBehaviour
{
    private const int SAMPLE_SIZE = 1024;

    public float rmsValue;
    public float dbValue;
    public float pitchValue;

    public bool is_circle;

    public float maxVisualScale = 25.0f;
    public float visualModifier = 50.0f;
    public float smoothSpeed = 10.0f;
    public float keepPercentage = 0.5f;

    public GameObject ui_dot;

    public GameObject player;

    public GameObject _start;

    public GameObject _end;

    private LineRenderer Lr;

    private AudioSource Source;
    private float[] samples;
    private float[] spectrum;
    private float sampleRate;

    private Transform[] visualList;
    private float[] visualScale;
    private Vector3[] visualPos;
    private int amnVisual = 16;
    // Start is called before the first frame update
    void Start()
    {
        Lr = GetComponent<LineRenderer>();
        Lr.positionCount = amnVisual;
        _start = GameObject.Find("start");
        _end = GameObject.Find("finish");
        Source = player.GetComponent<AudioSource>();
        samples = new float[SAMPLE_SIZE];
        spectrum = new float[SAMPLE_SIZE];
        sampleRate = AudioSettings.outputSampleRate;
        spawnLine();
    }

    // Update is called once per frame
    void Update()
    {
        AnalyseSound();
        UpdateVisual();
        set_points();

    }
    private void spawnLine()
    {
        Vector3 direction = _end.transform.position - _start.transform.position;

        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];
        visualPos = new Vector3[amnVisual];

        for (int i = 0; i < amnVisual; i++)
        {
            GameObject go = Instantiate(ui_dot, gameObject.transform) as GameObject;
            visualList[i] = go.transform;
            visualList[i].position = direction/amnVisual * i + _start.transform.position;
            visualPos[i] = visualList[i].position;
        }
    }

    private void UpdateVisual()
    {
        Vector3 direction = _end.transform.position - _start.transform.position;
        int visualIndex = 0;
        int spectrumIndex = 0;
        int averageSize = (int)((SAMPLE_SIZE * keepPercentage) / amnVisual);

        while (visualIndex < amnVisual)
        {
            int j = 0;
            float sum = 0;
            while (j < averageSize)
            {
                sum += spectrum[spectrumIndex];
                spectrumIndex++;
                j++;
            }

            float scaleY = sum / averageSize * visualModifier;
            visualScale[visualIndex] -= Time.deltaTime * smoothSpeed;

            if (visualScale[visualIndex] < scaleY)
            {
                visualScale[visualIndex] = scaleY;
            }


            if (visualScale[visualIndex] > maxVisualScale)
            {
                visualScale[visualIndex] = maxVisualScale;
            }

            visualList[visualIndex].position = visualPos[visualIndex] + visualScale[visualIndex]
                * Vector3.Cross(Vector3.forward, direction/amnVisual);
            visualIndex++;
        }
    }

    public void set_points()
    {
        for (int i = 0; i < amnVisual; i++)
        {
            Lr.SetPosition(i, visualList[i].position);
        }
    }
    private void AnalyseSound()
    {
        Source.GetOutputData(samples, 0);

        // get the rms

        float sum = 0;
        for (int i = 0; i < SAMPLE_SIZE; i++)
        {
            sum = samples[i] * samples[i];
        }
        rmsValue = Mathf.Sqrt(sum / SAMPLE_SIZE);

        // get the db value

        dbValue = Mathf.Log10(rmsValue / 0.1f);

        // get sound spectrum

        Source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    }
}
