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

	private Animator anim;
	private EnemyAI enemyAI;
	private float timeSinceLastAttack;
	private float attackDuration;

	// Use this for initialization
	void Start () {
		sprinting = false;
		moveDirection = Vector3.zero;
		cameraPos = new Vector3(.66f, 10.5f, 6.5f);
		anim = GetComponent<Animator>();
		attackDuration = 1.33f;
	}
	
	void Update () {
		checkMovement();
		checkAttack();
	}

	// Update is called once per frame
	private void checkMovement() {
		controller = GetComponent<CharacterController>();

		if (controller.isGrounded) {

			if (Input.GetKeyDown(KeyCode.LeftShift)) {
				sprinting = !sprinting;
			}

			if (Input.GetKey(KeyCode.W)) {
				checkSprint();
				moveDirection.z = 1f;
			}

			if (Input.GetKey(KeyCode.S)) {
				checkSprint();
				moveDirection.z = -1f;
			}

			if (Input.GetKey(KeyCode.A)) {
				checkSprint();
				moveDirection.x = -1f;
			}

			if (Input.GetKey(KeyCode.D)) {
				checkSprint();
				moveDirection.x = 1f;
			}

			if (sprinting) {
				moveDirection.x *= runSpeed;
				moveDirection.z *= runSpeed;
			} else {
				moveDirection.x *= walkSpeed;
				moveDirection.z *= walkSpeed;
			}

			if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)) {
				Utilities.playAnimation(anim, "Idle");
			}
		}

		playerCamera.transform.position = new Vector3(transform.position.x - cameraPos.x, transform.position.y + cameraPos.y, transform.position.z - cameraPos.z);
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

		//reset the movement direction
		moveDirection = Vector3.zero;
	}

	private void checkAttack() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {

			float tempTime = Time.time;

			if (timeSinceLastAttack + attackDuration < tempTime) {
				timeSinceLastAttack = tempTime;
				
				//start attack animation and wait a little bit
				Utilities.playAnimation(anim, "Attack01");
				StartCoroutine(attackWait());
			}
		}
	}

	private void checkSprint() {
		if (sprinting)
			Utilities.playAnimation(anim, "Run");
		else
			Utilities.playAnimation(anim, "Walk");
	}

	private IEnumerator attackWait() {
		yield return new WaitForSeconds (.33f);
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 3;
		RaycastHit temp = Utilities.raycastWrap(transform.position, forward, 3);
		
		if (temp.collider != null) {
			if (temp.collider.gameObject.tag == "Enemy") {
				temp.collider.gameObject.GetComponent<EnemyAI>().decrementHealth();
				temp.collider.gameObject.GetComponent<EnemyAnimator>().takeDamageAnimation();
			}
		}
	}
}


