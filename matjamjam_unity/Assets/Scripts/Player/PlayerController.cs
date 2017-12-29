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
	private float timeSinceLastAttack;
	private float timeSinceLastRegen;
	private float timeSinceLastStaminaDecrease;
	private float attackDuration;
	private bool isMoving;

	private PlayerStats playerStats;

	// Use this for initialization
	void Start () {
		sprinting = false;
		moveDirection = Vector3.zero;
		cameraPos = new Vector3(.66f, 10.5f, 6.5f);
		anim = GetComponent<Animator>();
		attackDuration = 1.33f;
		playerStats = GetComponent<PlayerStats>();
		isMoving = false;
	}
	
	void Update () {
		checkMovement();
		checkAttack();
	}

	void FixedUpdate()
	{
		checkStats();
	}

	// Update is called once per frame
	private void checkMovement() {
		controller = GetComponent<CharacterController>();

		if (controller.isGrounded) {

			if (Input.GetKeyDown(KeyCode.LeftShift)) {
				if (playerStats.getStamina() > 0) {
					sprinting = !sprinting;
				}
			}

			if (Input.GetKey(KeyCode.W)) {
				checkSprint();
				moveDirection.z = 1f;
				isMoving = true;
			}

			if (Input.GetKey(KeyCode.S)) {
				checkSprint();
				moveDirection.z = -1f;
				isMoving = true;
			}

			if (Input.GetKey(KeyCode.A)) {
				checkSprint();
				moveDirection.x = -1f;
				isMoving = true;
			}

			if (Input.GetKey(KeyCode.D)) {
				checkSprint();
				moveDirection.x = 1f;
				isMoving = true;
			}

			float sprintTime = Time.time;
			float sprintOffset = 1;

			if (sprinting) {

				if (playerStats.getStamina() > 0) {

					if (timeSinceLastStaminaDecrease + sprintOffset < sprintTime) {
						playerStats.useStamina(1);
						timeSinceLastStaminaDecrease = sprintTime;
					}

					moveDirection.x *= runSpeed;
					moveDirection.z *= runSpeed;
				} else {
					sprinting = !sprinting;
				}
			} else {
				moveDirection.x *= walkSpeed;
				moveDirection.z *= walkSpeed;
			}

			if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)) {
				Utilities.playAnimation(anim, "Idle");
				isMoving = false;
			}
		}

		//move the camera to center over the player
		playerCamera.transform.position = new Vector3(transform.position.x - cameraPos.x, transform.position.y + cameraPos.y, transform.position.z - cameraPos.z);
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

		//reset the movement direction
		moveDirection = Vector3.zero;
	}

	private void checkAttack() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			if (playerStats.getStamina() >= 10) {
				float tempTime = Time.time;

				if (timeSinceLastAttack + attackDuration < tempTime) {
					timeSinceLastAttack = tempTime;
					
					//start attack animation and wait a little bit
					Utilities.playAnimation(anim, "Attack01");
					playerStats.useStamina(10);
					StartCoroutine(attackWait());
				}
			} else {
				Debug.Log("No stamina");
			}
		}
	}

	private void checkSprint() {
		if (sprinting)
			Utilities.playAnimation(anim, "Run");
		else
			Utilities.playAnimation(anim, "Walk");
	}

	private void checkStats() {
		float coolDownTime = 4;
		float tempTime = Time.time;

		if (!sprinting) {
			if (timeSinceLastRegen + coolDownTime < Time.time) {
				playerStats.regenerateStamina(1);
				timeSinceLastRegen = Time.time;
			}
		}
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


