using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasBehaviour : MonoBehaviour {

	//this is bad design but it's only temporary

	public Text staminaText;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		staminaText.text = "Stamina: " + player.GetComponent<PlayerStats>().getStamina();
	}
}
