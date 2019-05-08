using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatPulseScript : MonoBehaviour
{

    public bool ticks = false;

    [Space(15)]
    public int tempo;
    public float pulseScale = 1.1f;
    decimal timePassed;

    bool beat = false;
    bool bar = false;


    int beatNo = 1;

    public AudioSource tick;
    public AudioSource gameMusic;


    public double songSampleRate;
    public double samplesPerBeat;
    public int totalBeats;
    public double lastSampledBeat;
    public float songLength;
    public double totalSamples;
    public double[] beatAtSample;
    public float loopDelay;
    public int beatPassed;
    int fourBeats = 1;


    public Transform[] musicShape;

    Transform[] musicShapeHolder;


    // Use this for initialization
    void Start()
    {

        musicShapeHolder = musicShape;

        songSampleRate = gameMusic.clip.frequency;
        samplesPerBeat = (double)60 / tempo * songSampleRate;
        songLength = gameMusic.clip.length;
        totalSamples = songLength * songSampleRate;
        totalBeats = (int)(totalSamples / samplesPerBeat);

        beatAtSample = new double[totalBeats];

        for (int i = 0; totalBeats > i; i++)
        {
            beatAtSample[i] = samplesPerBeat * (i + 1);
        }

        StartCoroutine(Timer());

    }



    public void ResetBeat()
    {
        beatPassed = 0;
    }


    IEnumerator Timer()
    {

        if (beatPassed == totalBeats - 1)
        {

            beatPassed = 4;
            fourBeats = 1;
            yield return new WaitForSeconds(loopDelay);
        }
         

        if (gameMusic.timeSamples >= beatAtSample[beatPassed])
        {
            beatPassed++;
            fourBeats++;
            if (fourBeats == 5)
            {
                StartCoroutine(Bar());

                fourBeats = 1;
            }
            else
            {
                StartCoroutine(Beat());

            }

        }
        yield return new WaitForEndOfFrame();

        StartCoroutine(Timer());


    }

    IEnumerator Beat()
    {
        beat = true;
        StartCoroutine(Pulse());
        if (ticks)
        {
            tick.pitch = 1.5f;
            tick.Play();
        }
        yield return new WaitForSeconds(0.5f);
        beat = false;
        if (ticks)
        {
            tick.Stop();
        }
    }

    IEnumerator Bar()
    {
        bar = true;
        beat = true;
        StartCoroutine(Pulse());

        if (ticks)
        {
            tick.pitch = 2;
            tick.Play();
        }


        yield return new WaitForSeconds(0.5f);
        bar = false;
        beat = false;

        if (ticks)
        {
            tick.Stop();

        }


    }



    IEnumerator Pulse()
    {
        float t = 0;

        while (t < 1)
        { 

            for(int i = 0; i < musicShape.Length; i++)
            {
                musicShape[i].localScale = Vector3.Lerp(musicShape[i].localScale, Vector3.one * pulseScale, t);
            }

            t += Time.deltaTime * 15;

            yield return null;

        }

        t = 0; 

        while (t < 1)
        {

            for (int i = 0; i < musicShape.Length; i++)
            {
                musicShape[i].localScale = Vector3.Lerp(Vector3.one, musicShape[i].localScale, t);
            }

            t += Time.deltaTime * 15;
            yield return null;

        }
    }


    }


