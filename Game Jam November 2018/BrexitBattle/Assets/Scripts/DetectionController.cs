using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour {


    //public variables
    [Header("Score Allocation")]
    [Tooltip("Points added to the score based on half or full alignment of letter with zone")]
    public int _halfPointScore = 5;
    public int _fullPointScore = 10;
    public int _streakPointScore = 20;

    [Space(5)]
    public int _streakNumber = 10;

    [Header("Boundary Values")]
    [Tooltip("Boundary vallues for detection zone and falling letter")]
    [Space(15f)]
    public float _letterBoundary = 1f;
    public float _halfPointBoundary = 0.6f;
    public float _fullPointBoundary = 0.2f;

    [Header("Score")]
    [Space(15f)]
    public int _score;
    public int _streak;

    public RandomButton randButton;
    public ParticleSystem streakPart;
    public UserInput userInput;
    public GameObject streakText;
    //private variables
    Vector2 _position;

    void Start()
    {
        _streak = 0;
        _position = transform.position;
        streakPart.Stop();

        streakText.SetActive(false);
    }

    private void Update()
    {
        _position = transform.position;
    }

    //Add half point
    void HalfPoint()
    {
        _score += _halfPointScore;
    }

    //Add full points
    void FullPoint()
    {
        _score += _fullPointScore;
    }

    //Check to see if the letter is in the boundary
    public bool CheckInBoundary(Vector2 position)
    {
        if (position.y - _letterBoundary <= _position.y + _halfPointBoundary && 
            position.y + _letterBoundary >= _position.y - _halfPointBoundary)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
     
       
    public void PlayCheer()
    {
        streakPart.Play();
        randButton.StreakCheer();
        userInput.StreakDance();
        streakText.SetActive(true);

    }

    public void PlayWinCheer()
    {
        streakPart.Play();
        randButton.WinCheer();
        streakText.SetActive(false);

    }

    public void PlayLoose()
    {
        userInput.LooseDance();
    }

    public void PlayStart()
    {
        userInput.StartDance();
    }


    public void ResetGame()
    {
        _score = 0;
        _streak = 0;
        PlayStart();
    }

    public void StartTriggOff()
    {
        userInput.StartDanceStop();
    }

    //Check the score based on letter position parameter
    public void CheckScore(Vector2 position)
    {
        if (position.y - _letterBoundary <= _position.y + _fullPointBoundary &&
            position.y + _letterBoundary >= _position.y - _fullPointBoundary)
        {
            FullPoint();
            _streak++;
            if(_streak == _streakNumber)
            {
                _score += _streakPointScore;
                _streak = 0;
                PlayCheer();
            }
        }
        else if (position.y - _letterBoundary <= _position.y + _halfPointBoundary && 
                 position.y + _letterBoundary >= _position.y - _halfPointBoundary)
        {
            HalfPoint();
            StreakCancel();
        }
    }

    public void StreakCancel()
    {
        _streak = 0;
        streakPart.Stop();
        streakText.SetActive(false);


    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(transform.position, new Vector3(1, _halfPointBoundary, 1));

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawCube(transform.position, new Vector3(1, _fullPointBoundary, 1));
    //}
}
