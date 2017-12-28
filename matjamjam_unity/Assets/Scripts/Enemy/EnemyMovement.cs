using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour {
	
	private Transform target;
	private int destPoint = 0;
	public Transform[] patrol;
	private NavMeshAgent agent;
	private bool chasingPlayer = false;

	private GameObject player;
	
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.autoBraking = false;
		destPoint = 0;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void runPatrol(){
		if(!agent.pathPending && agent.remainingDistance < 0.8f){
			goToNextPoint();
		}
	}

	public void goToNextPoint(){
		if(patrol.Length == 0){
			Debug.Log("No Patrol");
			return;
		}

		agent.destination = patrol[destPoint].transform.position;

		//Sets next index of patrol that enemy has to move to
		if(destPoint != patrol.Length - 1){
			destPoint++;
		} else {
			destPoint = 0;
		}
	}

	public void chasePlayer(){
		agent.destination = player.transform.position;
	}

	public void stopMoving(){
		agent.isStopped = true;

	}

	public void startMoving(){
		agent.isStopped = false;
	}

	public void facePlayer(){
		Vector3 targetDir = player.transform.position - transform.position;
        float step =  4 * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
	}
}
