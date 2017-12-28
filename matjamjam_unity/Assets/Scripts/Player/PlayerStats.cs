using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public int health = 3;
	private bool dead;
	// Use this for initialization
	void Start () {
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(dead){
			//play death animation
		}
	}

	public void decrementHealth(){
		if(health > 0){
			health--;
		} else {
			dead = true;
		}
	}

	public void getHealth(){
		
	}
}
