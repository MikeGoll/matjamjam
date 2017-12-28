using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	// Use this for initialization
	private bool attacking;
	private bool dead;
	private bool inRange;
	public int health = 5;
	public AnimationClip clip;
	private EnemyMovement enemyMovement;
	private EnemyAnimator enemyAnimator;
	private EnemyVision enemyVision;
	private PlayerStats playerStats;
	private float lastAtackTime = 0f;
	private float duration = 0.5f;
	private float stunDuration = 1.0f;
	private float lastStunnedTime = 0.0f;
	void Start () {
		inRange = false;
		attacking = false;
		dead = false;
		enemyMovement = GetComponent<EnemyMovement>();
		enemyAnimator = GetComponent<EnemyAnimator>();
		enemyVision = GetComponent<EnemyVision>();
		playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!dead){
			if(enemyVision.seesPlayer()){
				if(!inRange){

					enemyMovement.startMoving();
					enemyMovement.chasePlayer();
				} else {
					enemyMovement.facePlayer();
					enemyMovement.stopMoving();	
					if(!attacking){	
						StartCoroutine(attackPlayer());	
					}			
				}
				inRange = enemyVision.checkPlayerDistance();
			} else {
				enemyMovement.runPatrol();
			}
			if(!inRange){
				enemyAnimator.playRunAnimation();
			}
		} else if (dead){
			enemyMovement.stopMoving();
			enemyAnimator.playDeathAnimation();
		}
	}

	public void decrementHealth(){
		lastStunnedTime = Time.timeSinceLevelLoad;
		if(health > 0){	
			health--;
		} else {
			dead = true;
		}
	}


	public IEnumerator attackPlayer(){	

		if(canAttack() && notStunned()){
			enemyAnimator.playIdleAnimation();
			enemyAnimator.playAttackAnimation();
			attacking = true;
			lastAtackTime = Time.timeSinceLevelLoad;
			yield return new WaitForSeconds(1.0f);
			if(notStunned()){
				Debug.Log("hit");
				if(enemyVision.checkPlayerDistance()){
					playerStats.decrementHealth();
				}
			} 
			
		}
		attacking = false;
	}

	public bool canAttack(){
		if(Time.timeSinceLevelLoad > lastAtackTime + duration){
			return true;
		}
		return false;
	}

	public bool notStunned(){
		if(Time.timeSinceLevelLoad > lastStunnedTime + stunDuration){
			return true;
		}
		return false;
	}
}
