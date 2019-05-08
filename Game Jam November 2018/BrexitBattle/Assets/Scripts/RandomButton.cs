 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomButton : MonoBehaviour {

    public GameObject[] buttons;
    public GameObject[] arrows;
    public Transform spawnPoint;
    public Transform arrowsSpawn;
	public int rNumber;
    int arrowNumber;

    float timeBtwSpawns = 2;

    bool running;

    [Header("Game manager")]
    [Space(15f)]
    public float timeLimit = 90;
    public Text timeText;


    public List<GameObject> _GC = new List<GameObject>();
    public List<GameObject> _AC = new List<GameObject>();

    public GameObject StartUi;
    public GameObject InGameUi;
    public GameObject TheresaWinsUi;
    public GameObject EUWinsUi;
    public GameObject DrawUi;
    public GameObject SettingsUi;
    public GameObject CreditsUi;
    public GameObject PauseUi;
    public GameObject HelpUi;



    public AudioSource menuMusic;
    public AudioSource gameMusic;
    public AudioSource lightSound;
    public AudioSource cheerSound;
    public AudioSource endGameSound;


    public Light p1Light;
    public Light p2Light;
    public Light mainLight;

    public GameObject zoneP1;
    public GameObject zoneP2;

    public DetectionController p1Detection;
    public DetectionController p2Detection;

    public CameraController camController;

    float timeLimitHolder;

    public bool resetGame = false;
    void Start()
    {

        timeLimitHolder = timeLimit;
        menuMusic.Play();
        gameMusic.Stop();
        cheerSound.Play();


        p1Light.enabled = false;
        p2Light.enabled = false;

        timeText.text = "";

        StartUi.SetActive(true);
        InGameUi.SetActive(false);

        zoneP1.SetActive(false);
        zoneP2.SetActive(false);

        mainLight.enabled = true;
        p1Light.enabled = false;
        p2Light.enabled = false;

        TheresaWinsUi.SetActive(false);
        EUWinsUi.SetActive(false);
        DrawUi.SetActive(false);
        SettingsUi.SetActive(false);
        CreditsUi.SetActive(false);
        PauseUi.SetActive(false);
        HelpUi.SetActive(false);
    }



    public void StartGame () {


        StartUi.SetActive(false);

        mainLight.enabled = false;

        StartCoroutine(StartGameMode());
        StartCoroutine(FadeMusic());

          
        TheresaWinsUi.SetActive(false);
        EUWinsUi.SetActive(false);
        DrawUi.SetActive(false);
        SettingsUi.SetActive(false);
        CreditsUi.SetActive(false);
        PauseUi.SetActive(false);
        HelpUi.SetActive(false);

    }


    public void ResetGame()
    {
        resetGame = true;
        TheresaWinsUi.SetActive(false);
        EUWinsUi.SetActive(false);
        DrawUi.SetActive(false);

        p1Light.enabled = false;
        p2Light.enabled = false;

        timeLimit = timeLimitHolder;

        StartUi.SetActive(false);
        InGameUi.SetActive(false);


        SettingsUi.SetActive(false);
        CreditsUi.SetActive(false);
        PauseUi.SetActive(false);
        HelpUi.SetActive(false);


        mainLight.enabled = false;

        camController.CameraMoveToGame();

        p1Detection.ResetGame();
        p2Detection.ResetGame();

        timeBtwSpawns = 2;

        StartCoroutine(StartGameMode());
        StartCoroutine(FadeMusic());
    }


    IEnumerator FadeMusic()
    {
        float t = 1;

        while (t > 0.1)
        {
            menuMusic.volume = t;
            cheerSound.volume = t;
            t -= Time.time * 0.01f;

            yield return null;
        }
        menuMusic.Stop();
    }
     
    IEnumerator StartGameMode()
    {
        yield return new WaitForSeconds(1);
        p1Light.enabled = true;
        lightSound.pitch = 1f;
        lightSound.panStereo = -0.65f;
        lightSound.Play();
        p1Detection.StartTriggOff();
        p2Detection.StartTriggOff();

        yield return new WaitForSeconds(1);
        p2Light.enabled = true;
        lightSound.pitch = 0.92f;
        lightSound.panStereo = 0.65f;
        lightSound.Play();

        yield return new WaitForSeconds(1);
        resetGame = false;
        running = true;
        Invoke("PickButton", timeBtwSpawns);
        InGameUi.SetActive(true);
     
        gameMusic.Play();

        zoneP1.SetActive(true);
        zoneP2.SetActive(true);

    }

    public void WinCheer()
    {
        cheerSound.Stop();
        cheerSound.volume = 1;
        cheerSound.Play();
        StopCoroutine("FadeCheer");
    }

    public void StreakCheer()
    {
        cheerSound.Stop();
        cheerSound.volume = 1;
        cheerSound.Play();
        StartCoroutine("FadeCheer"); 
    }

    IEnumerator FadeCheer()
    {
        yield return new WaitForSeconds(5);
        float t = 1;

        while (t > 0.1)
        {
            cheerSound.volume = t;
            t -= Time.time * 0.001f;
            yield return null;
        }
    }

     
    void Update() 
    {
        if (running)
        {
            timeText.text = ((int)(timeLimit)).ToString();
            timeLimit -= Time.deltaTime;

            if (timeLimit <= 0 && running == true)
            {
                running = false;
                //show panel with scores
                //slide menu back in
                if(p1Detection._score > p2Detection._score)
                {
                    //p1wins
                    p1Detection.PlayWinCheer();
                    p2Detection.PlayLoose();
                    camController.CameraMoveToP1Win();

                    gameMusic.Stop();
                    endGameSound.Play();

                    TheresaWinsUi.SetActive(true);
                    EUWinsUi.SetActive(false);
                    DrawUi.SetActive(false);


                    zoneP1.SetActive(false);
                    zoneP2.SetActive(false);

                }
                else if (p1Detection._score < p2Detection._score)
                { 
                    p2Detection.PlayWinCheer();
                    p1Detection.PlayLoose();

                    camController.CameraMoveToP2Win();
                    gameMusic.Stop();
                    endGameSound.Play();


                    TheresaWinsUi.SetActive(false);
                    EUWinsUi.SetActive(true);
                    DrawUi.SetActive(false);

                    zoneP1.SetActive(false);
                    zoneP2.SetActive(false);
                     
                }
                else
                {
                    TheresaWinsUi.SetActive(false);
                    EUWinsUi.SetActive(false);
                    DrawUi.SetActive(true);
                    gameMusic.Stop();
                    endGameSound.Play();
                    p1Detection.PlayLoose();
                    p2Detection.PlayLoose();


                    zoneP1.SetActive(false);
                    zoneP2.SetActive(false);

                }

            }

            if (Input.GetKey(KeyCode.Escape) && running == true)
            {
                //show the pause panel
                Time.timeScale = 0;
                PauseUi.SetActive(true);
            }

        }
    }   

	public void PickButton()
	{

        if (running == true)
        {
            rNumber = Random.Range(1, 5);
            switch (rNumber)
            {
                case 1:
                    _GC.Add(Instantiate(buttons[0], spawnPoint.position, Quaternion.identity));
                    timeBtwSpawns -= 0.05f;
                    break;

                case 2:
                    _GC.Add(Instantiate(buttons[1], spawnPoint.position, Quaternion.identity));
                    timeBtwSpawns -= 0.05f;
                    break;

                case 3:
                    _GC.Add(Instantiate(buttons[2], spawnPoint.position, Quaternion.identity));
                    timeBtwSpawns -= 0.05f;
                    break;

                case 4:
                    _GC.Add(Instantiate(buttons[3], spawnPoint.position, Quaternion.identity));
                    timeBtwSpawns -= 0.05f;
                    break;

                default:
                    Debug.Log("Error");
                    break;
            }

            arrowNumber = Random.Range(1, 5);
            switch (arrowNumber)
            {
                case 1:
                    _AC.Add(Instantiate(arrows[0], arrowsSpawn.position, Quaternion.identity));
                    //print("second switch " + timeBtwSpawns);
                    break;

                case 2:
                    _AC.Add(Instantiate(arrows[1], arrowsSpawn.position, Quaternion.identity));
                    break;

                case 3:
                    _AC.Add(Instantiate(arrows[2], arrowsSpawn.position, Quaternion.identity));
                    break;

                case 4:
                    _AC.Add(Instantiate(arrows[3], arrowsSpawn.position, Quaternion.identity));
                    break;

                default:
                    Debug.Log("Error");
                    break;
            }

           // print(timeBtwSpawns);
            if (timeBtwSpawns <= 0.45f)
            {
                timeBtwSpawns = 0.45f;
            }

            Invoke("PickButton", timeBtwSpawns);
        }
    }
     

    public void UnpauseGame()
    {
        PauseUi.SetActive(false);
        Time.timeScale = 1;
    }
}
