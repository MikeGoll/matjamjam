using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {
	// Use this for initialization
	private bool playerSpotted;
	private float fieldOfView = 90.0f;
	private SphereCollider col;
	private GameObject player;
	void Start () {
		
		playerSpotted = false;
		col = GetComponent<SphereCollider>();
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(playerSpotted){
			//check if player is still in sight
		}
	}


	void OnTriggerStay(Collider other) {
		if(other.gameObject == player){
			checkVisionCone();
		}
	}

	public void checkVisionCone(){

		Vector3 direction = player.transform.position - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);
		Debug.Log("Ang " + angle + " fieldview " + fieldOfView);
		if(angle < fieldOfView * 0.5f){
			RaycastHit hit = Utilities.raycastWrap(transform.position, direction, col.radius);
			if(hit.collider.gameObject == player){
				playerSpotted = true;
			}
		}
	}

	public bool seesPlayer(){
		return playerSpotted;
	}

	public void checkPlayerDistance(){
		Vector3 direction = player.transform.position - transform.position;
		RaycastHit hit = Utilities.raycastWrap(transform.position, direction, col.radius + 5);
		if(hit.distance > col.radius){
			playerSpotted = false;
		}
	}
}
