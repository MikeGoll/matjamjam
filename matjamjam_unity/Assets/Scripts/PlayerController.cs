using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private CharacterController controller;
	private Vector3 moveDirection;
	public float gravity;
	private bool sprinting;
	public float walkSpeed;
	public float runSpeed;
	public Camera playerCamera;

	private Vector3 cameraPos;

	// Use this for initialization
	void Start () {
		sprinting = false;
		moveDirection = Vector3.zero;
		cameraPos = new Vector3(.66f, 10.5f, 6.5f);
	}
	
	// Update is called once per frame
	void Update () {
		controller = GetComponent<CharacterController>();

		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);

			if (Input.GetKeyDown(KeyCode.LeftShift)) {
				sprinting = !sprinting;
			}

			if (sprinting) {
				moveDirection.x *= runSpeed;
				moveDirection.z *= runSpeed;
			} else {
				moveDirection.x *= walkSpeed;
				moveDirection.z *= walkSpeed;
			}
		}

		playerCamera.transform.position = new Vector3(transform.position.x - cameraPos.x, transform.position.y + cameraPos.y, transform.position.z - cameraPos.z);
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}
