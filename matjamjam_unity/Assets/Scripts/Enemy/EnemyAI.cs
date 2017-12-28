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
	private EnemyVision enemyVision;
	void Start () {
		running = true;
		attacking = false;
		dead = false;
		enemyMovement = GetComponent<EnemyMovement>();
		enemyAnimator = GetComponent<EnemyAnimator>();
		enemyVision = GetComponent<EnemyVision>();
	}
	
	// Update is called once per frame
	void Update () {
		if(enemyVision.seesPlayer()){
			enemyMovement.chasePlayer();
			enemyVision.checkPlayerDistance();
		} else {
			enemyMovement.runPatrol();
		}
		if(running){
			enemyAnimator.playRunAnimation();
		}
	}
}
