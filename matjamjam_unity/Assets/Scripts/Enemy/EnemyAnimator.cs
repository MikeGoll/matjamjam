using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playRunAnimation(){
		anim.Play("Run");
	}

	public void playAttackAnimation(){
		anim.Play("Attack01");
	}

	public void playDeathAnimation(){
		anim.Play("Die");
	}
}
