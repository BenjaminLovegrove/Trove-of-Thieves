using UnityEngine;
using System.Collections;

public class s_Character : MonoBehaviour {
	
	Vector3 newPos;
	bool canMove = true;

	void Start () {
		newPos = transform.position;
	}

	void Update () {
		CollisionDetection ();
		if (canMove) {
			transform.position = Vector3.Lerp (transform.position, newPos, 0.7f * Time.deltaTime);
		}
	}

	void DoMove(int steps){
		newPos = transform.position + Vector3.right * steps / 3;
	}

	void CollisionDetection(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.right, out hit, 1)) {
			if (hit.collider.tag == "Block"){
				canMove = false;
			} else {
				canMove = true;
			}
		}
	}
}
