using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour {

	public static bool raycastWrap(Vector3 position, Vector3 direction, float distance, GameObject target) {
		RaycastHit hit;
		Debug.DrawRay(position, direction, Color.red);
		if(Physics.Raycast(position, direction, out hit, distance)){
			if(hit.collider.gameObject == target){
				return true;
			}
		}
		return false;
	}
}
