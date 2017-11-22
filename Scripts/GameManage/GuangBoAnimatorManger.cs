using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuangBoAnimatorManger : MonoBehaviour {

    private GuangBoListener guangBoListener;
    private Animator animator;

    // Use this for initialization
    void Start () {
        guangBoListener = FindObjectOfType<GuangBoListener>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (guangBoListener.CheckMsg()) {
            animator.SetBool("newMSGCome",true);
        }
        else
        {
            animator.SetBool("newMSGCome", false);
        }
		
	}
}
