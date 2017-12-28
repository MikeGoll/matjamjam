using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {
	// Use this for initialization
	private bool playerSpotted;
	private float fieldOfView = 90.0f;
	private SphereCollider col;
	void Start () {
		playerSpotted = false;
		col = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerStay(Collider other) {
        Debug.Log(other.tag);
		if(other.gameObject.tag == "Player"){
			Debug.Log("Player in sphere");
			checkVisionCone(other.gameObject);
		}
	}

	public void checkVisionCone(GameObject player){

		Vector3 direction = player.transform.position - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);

		if(angle < fieldOfView * 0.5f){
				if(Utilities.raycastWrap(transform.position, direction, col.radius, player)){
					Debug.Log("Player Spotted");
				}
		}
	}

	public bool seesPlayer(){
		return playerSpotted;
	}
}
