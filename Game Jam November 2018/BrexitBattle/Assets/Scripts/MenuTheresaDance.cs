using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTheresaDance : MonoBehaviour {

    public Animator anim;
    float speedModifier = 1.00f;
    int i;
	
	void Update () {
        if (i == 3600)
        {
            speedModifier += 0.01f;
            anim.speed = speedModifier;
            i -= 30;
        }
        else
        {
            i++;
        }
	}
}
