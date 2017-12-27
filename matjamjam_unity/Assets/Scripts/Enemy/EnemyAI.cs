using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	// Use this for initialization
	private bool running;
	private bool attacking;
	private bool dead;
	private EnemyMovement enemyMovement;
	private EnemyAnimator enemyAnimator;
	void Start () {
		running = true;
		attacking = false;
		dead = false;
		enemyMovement = GetComponent<EnemyMovement>();
		enemyAnimator = GetComponent<EnemyAnimator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(running){
			enemyMovement.runPatrol();
			enemyAnimator.playRunAnimation();
		}
	}
}
