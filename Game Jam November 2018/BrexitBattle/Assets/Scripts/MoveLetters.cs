using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLetters : MonoBehaviour {

    public UserInput target;
    public float speed = 5.0f;
    public string objects = "";
	
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("user").GetComponent<UserInput>();
    }
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(transform.position.x, target.destroyPoint.transform.position.y, transform.position.z), step);
	}
}
