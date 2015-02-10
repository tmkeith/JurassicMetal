using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerMovement : MonoBehaviour {

	public float speed = 6f;
	Vector3 movement;
	Rigidbody playerRigidbody;
	int floorMask; 
	float camRayLength = 100f;

	// Use this for initialization
	void Start () {
		floorMask = LayerMask.GetMask ("Collisions");
		playerRigidbody = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);

		Turning ();
	}

	void Move (float h, float v){
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning (){
		Ray camRay = Camera.main.ScreenPointToRay (Input.GetAxisRaw ("Horizontal"));
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
						Vector3 playerToMouse = floorHit.point - transform.position;
						playerToMouse.y = 0f;
		}
	}
}
