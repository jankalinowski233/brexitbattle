using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour {

    public RandomButton keys;
    public DetectionController detect;

    public bool player1;
    public GameObject destroyPoint;
    public Animator animator;

    int keyValidCounter = 0;

    [Header("UI")]
    [Space(15f)]
    public Text score;

    void Start()
    {
        //detect = GameObject.Find("DetectorLeft").GetComponent<DetectionController>();
        //num = GameObject.Find("Spawner").GetComponent<RandomButton>();

        score.text = "0";


        if (player1)
        {
            StartCoroutine(KeyCheckerP1());

        }
        else
        {
            StartCoroutine(KeyCheckerP2());

        }
    }


    IEnumerator KeyCheckerP1()
    {
        float t = 0;
        keyValidCounter = 0;

        while (t < 0.1f)
        {

            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    keyValidCounter++;
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    keyValidCounter++;

                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    keyValidCounter++;

                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    keyValidCounter++;

                }

            }
     
            t += Time.fixedDeltaTime;
            yield return null;

        }

        StartCoroutine(KeyCheckerP1());

    }



    IEnumerator KeyCheckerP2()
    {
        float t = 0;
        keyValidCounter = 0;

        while (t < 0.1f)
        {

            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    keyValidCounter++;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    keyValidCounter++;

                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    keyValidCounter++;

                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    keyValidCounter++;

                }

            }

            t += Time.fixedDeltaTime;
            yield return null;

        }

        StartCoroutine(KeyCheckerP2());

    }


    public void StreakDance()
    {
        animator.SetTrigger("Streak");
    }

    public void LooseDance()
    {
        animator.SetTrigger("Loose");
    }

    public void StartDance()
    {
        animator.SetTrigger("Start");
    }

    public void StartDanceStop()
    {
        animator.ResetTrigger("Start");
    }

    void LateUpdate()
    {
        if (keys.resetGame)
        {
            score.text = "0";

        }

        animator.ResetTrigger("Key1");
        animator.ResetTrigger("Key2");
        animator.ResetTrigger("Key3");
        animator.ResetTrigger("Key4");


        if (player1)
        {

            for (int i = 0; i < keys._GC.Count; i++)
            {
                if (keyValidCounter <= 1)
                {


                    if (Input.GetKeyDown(KeyCode.W) && keys._GC[i].GetComponent<MoveLetters>().objects == "w" && detect.CheckInBoundary(keys._GC[i].transform.position))
                    {
                        detect.CheckScore(keys._GC[i].transform.position);
                        Destroy(keys._GC[i]);
                        animator.SetTrigger("Key1");
                        keys._GC.Remove(keys._GC[i]);
                        score.text = detect._score.ToString();

                        break;
                    }

                    if (Input.GetKeyDown(KeyCode.A) && keys._GC[i].GetComponent<MoveLetters>().objects == "a" && detect.CheckInBoundary(keys._GC[i].transform.position))
                    {
                        detect.CheckScore(keys._GC[i].transform.position);
                        Destroy(keys._GC[i]);
                        animator.SetTrigger("Key2");
                        keys._GC.Remove(keys._GC[i]);
                        score.text = detect._score.ToString();
                        break;

                    }

                    if (Input.GetKeyDown(KeyCode.S) && keys._GC[i].GetComponent<MoveLetters>().objects == "s" && detect.CheckInBoundary(keys._GC[i].transform.position))
                    {
                        detect.CheckScore(keys._GC[i].transform.position);
                        Destroy(keys._GC[i]);
                        animator.SetTrigger("Key3");
                        keys._GC.Remove(keys._GC[i]);
                        score.text = detect._score.ToString();
                        break;
                    }

                    if (Input.GetKeyDown(KeyCode.D) && keys._GC[i].GetComponent<MoveLetters>().objects == "d" && detect.CheckInBoundary(keys._GC[i].transform.position))
                    {
                        detect.CheckScore(keys._GC[i].transform.position);
                        Destroy(keys._GC[i]);
                        animator.SetTrigger("Key4");
                        keys._GC.Remove(keys._GC[i]);
                        score.text = detect._score.ToString();
                        break;
                    }

                    else if (keys._GC[i].transform.position.y <= destroyPoint.transform.position.y)
                    {
                        detect.StreakCancel();
                        Destroy(keys._GC[i]);
                        keys._GC.Remove(keys._GC[i]);
                        animator.ResetTrigger("Streak");

                        score.text = detect._score.ToString();
                        break;
                    }

                }
                else
                {
                    detect.StreakCancel();
                    Destroy(keys._GC[i]);
                    keys._GC.Remove(keys._GC[i]);

                    animator.ResetTrigger("Streak");

                    break;
                }

            }

        }

        else
        {
            for (int i = 0; i < keys._AC.Count; i++)
            {

                if (keyValidCounter <= 1)
                {


                    if (Input.GetKeyDown(KeyCode.UpArrow) && keys._AC[i].GetComponent<MoveLetters>().objects == "up" && detect.CheckInBoundary(keys._AC[i].transform.position))
                    {
                        detect.CheckScore(keys._AC[i].transform.position);
                        Destroy(keys._AC[i]);
                        animator.SetTrigger("Key1");
                        keys._AC.Remove(keys._AC[i]);
                        score.text = detect._score.ToString();
                        break;
                    }

                    if (Input.GetKeyDown(KeyCode.LeftArrow) && keys._AC[i].GetComponent<MoveLetters>().objects == "left" && detect.CheckInBoundary(keys._AC[i].transform.position))
                    {
                        detect.CheckScore(keys._AC[i].transform.position);
                        Destroy(keys._AC[i]);
                        animator.SetTrigger("Key2");
                        keys._AC.Remove(keys._AC[i]);
                        score.text = detect._score.ToString();
                        break;

                    }

                    if (Input.GetKeyDown(KeyCode.DownArrow) && keys._AC[i].GetComponent<MoveLetters>().objects == "down" && detect.CheckInBoundary(keys._AC[i].transform.position))
                    {
                        detect.CheckScore(keys._AC[i].transform.position);
                        Destroy(keys._AC[i]);
                        animator.SetTrigger("Key3");
                        keys._AC.Remove(keys._AC[i]);
                        score.text = detect._score.ToString();
                        break;
                    }

                    if (Input.GetKeyDown(KeyCode.RightArrow) && keys._AC[i].GetComponent<MoveLetters>().objects == "right" && detect.CheckInBoundary(keys._AC[i].transform.position))
                    {
                        detect.CheckScore(keys._AC[i].transform.position);
                        Destroy(keys._AC[i]);
                        animator.SetTrigger("Key4");
                        keys._AC.Remove(keys._AC[i]);
                        score.text = detect._score.ToString();
                        break;
                    }

                    else if (keys._AC[i].transform.position.y <= destroyPoint.transform.position.y)
                    {
                        detect.StreakCancel();
                        Destroy(keys._AC[i]);
                        keys._AC.Remove(keys._AC[i]);
                        score.text = detect._score.ToString();

                        animator.ResetTrigger("Streak");

                        break;
                    }
                }
                else
                {
                    detect.StreakCancel();
                    Destroy(keys._AC[i]);
                    keys._AC.Remove(keys._AC[i]);

                    animator.ResetTrigger("Streak");

                    break;
                }
            }

        }

    }



}
