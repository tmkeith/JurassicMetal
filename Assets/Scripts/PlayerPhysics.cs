using UnityEngine;
using System.Collections;


[RequireComponent (typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {
	
	public LayerMask collisionMask;

	private BoxCollider collider;
	private Vector3 s;
	private Vector3 c;
	
	private float skin = .005f;
	
	[HideInInspector]
	public bool grounded;
	
	Ray ray;
	RaycastHit hit;
	
	void Start() {
		collider = GetComponent<BoxCollider>();
		s = collider.size;
		c = collider.center;
	}

	public void Move(Vector3 moveAmount) {
		
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		float deltaZ = moveAmount.z;
		Vector3 p = transform.position;
		
		// Check collisions above and below
		grounded = false;
		
		for (int i = 0; i<3; i ++) {
			float dir = Mathf.Sign(deltaY);
			float x = (p.x + c.x - s.x/2) + s.x/2 * i; // Left, centre and then rightmost point of collider
			float z = (p.z + c.z - s.x/2) + s.z/2 * i;
			float y = p.y + c.y + s.y/2 * dir; // Bottom of collider
			
			ray = new Ray(new Vector3(x,y,z), new Vector2(0,dir));
			Debug.DrawRay(ray.origin,ray.direction);
			if (Physics.Raycast(ray,out hit,Mathf.Abs(deltaY),collisionMask)) {
				// Get Distance between player and ground
				float dst = Vector3.Distance (ray.origin, hit.point);
				
				// Stop player's downwards movement after coming within skin width of a collider
				if (dst < skin) {
					deltaY = dst * dir + skin;
				}
				else {
					deltaY = 0;
				}
				
				grounded = true;
				
				break;
				
			}
		}
		
	
		
		Vector3 finalTransform = new Vector3(deltaX,deltaY,deltaZ);
		
		transform.Translate(finalTransform);
	}
	
}
