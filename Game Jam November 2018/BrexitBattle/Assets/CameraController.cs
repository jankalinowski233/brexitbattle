using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {



    public Transform startPos;
    public Transform gamePos;
    public Transform p1Win;
    public Transform p2Win;
    private Vector3 currenVelocity;

    // Use this for initialization
    void Start () {
        transform.position = startPos.position;
        transform.rotation = startPos.rotation;
	}
	


    public void CameraMoveToGame()
    {
        StartCoroutine(MoveToGame());
    }
     

    IEnumerator MoveToGame()
    {
        float t = 0;

        while (t < 1)
        {
            transform.position = Vector3.Lerp(transform.position, gamePos.position, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, gamePos.rotation, t);

            t += Time.deltaTime;

            yield return null;
        }
    }


    public void CameraMoveToP1Win()
    {
        StartCoroutine(MoveToP1Win());
    }


    IEnumerator MoveToP1Win()
    {
        float t = 0;

        while (t < 1)
        {
            transform.position = Vector3.Lerp(transform.position, p1Win.position, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, p1Win.rotation, t);

            t += Time.deltaTime;

            yield return null;
        }
    }

        
    public void CameraMoveToP2Win()
    {
        StartCoroutine(MoveToP2Win());
    }


    IEnumerator MoveToP2Win()
    {
        float t = 0;

        while (t < 1)
        {
            transform.position = Vector3.Lerp(transform.position, p2Win.position, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, p2Win.rotation, t);

            t += Time.deltaTime;

            yield return null;
        }
    }



    public void CameraMoveToStart()
    {
        StartCoroutine(MoveToStart());
    }


    IEnumerator MoveToStart() 
    {
        float t = 0;

        while (t < 1)
        {
            transform.position = Vector3.SmoothDamp(transform.position, startPos.position, ref currenVelocity, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, startPos.rotation, t);

            t += Time.deltaTime;

            yield return null;
        }
    }
}
