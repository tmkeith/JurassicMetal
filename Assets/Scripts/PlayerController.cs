using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {
	
	// Player Handling
	public float gravity = 20;
	public float speed = 8;
	public float acceleration = 30;
	public float jumpHeight = 12;
	
	private float currentSpeedH;
	private float currentSpeedV;
	private float targetSpeedH;
	private float targetSpeedV;
	private Vector3 amountToMove;



	private PlayerPhysics playerPhysics;
	

	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
	}
	
	void Update () {
		targetSpeedH = Input.GetAxisRaw("Horizontal") * speed;
		targetSpeedV = Input.GetAxisRaw("Vertical") * speed;
		currentSpeedH = IncrementTowards(currentSpeedH, targetSpeedH,acceleration);
		currentSpeedV = IncrementTowards(currentSpeedV, targetSpeedV,acceleration);
		
		if (playerPhysics.grounded) {
			amountToMove.y = 0;

			// Jump
			if (Input.GetButtonDown("Jump")) {
				amountToMove.y = jumpHeight;	
			}
		}
		
		amountToMove.x = currentSpeedH;
		amountToMove.z = currentSpeedV;
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move(amountToMove * Time.deltaTime);
	}
	
	// Increase n towards target by speed
	private float IncrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; // if n has now passed target then return target, otherwise return n
		}
	}
}
