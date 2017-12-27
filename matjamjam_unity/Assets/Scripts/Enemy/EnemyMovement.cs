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
	
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.autoBraking = false;
		destPoint = 0;
	}
	
	// Update is called once per frame
	void Update () {
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
}
