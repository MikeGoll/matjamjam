using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	private int health;
	private int stamina;
	private int mana;
	private int strength;
	private int level;
	private int experience;
	private bool dead;
	// Use this for initialization
	void Start () {
		dead = false;
		health = 3;
		stamina = 100;
		mana = 100;
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

	public void useStamina(int amount) {
		if (stamina > 0)
			stamina -= amount;
			if (stamina < 0)
				stamina = 0;
	}

	public int getStamina() {
		return stamina;
	}

	public void regenerateStamina(int amount) {
		if (stamina < 100) {

			stamina += amount;

			if (stamina > 100)
				stamina = 100;
		}
	}
}
