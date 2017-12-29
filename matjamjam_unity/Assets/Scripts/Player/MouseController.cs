using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

	private float mouseX;
     private float mouseY;
     private float cameraDif;

	private Vector3 _target;
	public Camera playerCamera;

	public GameObject player;

	// Use this for initialization
	void Start () {
		cameraDif = playerCamera.transform.position.y - player.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        _target = playerCamera.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cameraDif));
        Vector3 lookDirection = new Vector3 (_target.x, player.transform.position.y, _target.z); 
        transform.LookAt(lookDirection);
	}
}
