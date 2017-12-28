using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	private bool attacked;
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
		if (!attacked)
			anim.Play("Attack01");
	}

	public void playDeathAnimation(){
		anim.Play("Die");
	}

	public void takeDamageAnimation() {
		attacked = true;
		anim.StopPlayback();
		StartCoroutine (InvulWait ());
	}

	 private IEnumerator InvulWait() {
		anim.Play("GetHit");
		yield return new WaitForSeconds (1.33f); //invul time
		attacked = false;
	}
}
