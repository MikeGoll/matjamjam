using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour {

	public static RaycastHit raycastWrap(Vector3 position, Vector3 direction, float distance) {
		RaycastHit hit;
		Debug.DrawRay(position, direction, Color.red);
		if(Physics.Raycast(position, direction, out hit, distance)){
			return hit;
		}
		return hit;
	}

	public static void playAnimation(Animator anim, string animation) {
		anim.Play(animation);
	}
}
