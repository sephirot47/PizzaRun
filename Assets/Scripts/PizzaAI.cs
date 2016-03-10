using UnityEngine;
using System.Collections;

public class PizzaAI : MonoBehaviour 
{
	private Rigidbody2D rb;
	private bool grounded = false;

	public Vector2 speed;
	public float minGapWidthToJump;
	public float wallSensorDistance;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update () 
	{
		// Movement
		rb.velocity = new Vector2(speed.x, rb.velocity.y);

		Vector2 origin = new Vector2(transform.position.x,
			                         transform.position.y - GetComponent<SpriteRenderer> ().bounds.extents.y * 0.95f);
		RaycastHit2D hit;
		Debug.DrawRay (origin, Vector3.right * wallSensorDistance, Color.green);
		if ( (hit = Physics2D.Raycast(origin, Vector3.right, wallSensorDistance)) )
		{
			JumpObstacle(origin, hit.collider.gameObject);
		}


		RaycastHit2D[] hits;
		origin = new Vector2(transform.position.x,
							 transform.position.y - GetComponent<SpriteRenderer> ().bounds.extents.y * 2.0f);
		Debug.DrawRay (origin, Vector3.right * 99.9f);
		if ( (hits = Physics2D.RaycastAll(origin, Vector3.right, 99.9f)).Length != 0 )
		{
			if(hits.Length >= 2)
			{
				GameObject oLeft = hits[0].collider.gameObject;
				GameObject oRight = hits[1].collider.gameObject;
				float gapLeft = oLeft.transform.position.x + oLeft.GetComponent<SpriteRenderer> ().bounds.extents.x;
				if (gapLeft - transform.position.x < wallSensorDistance)
				{
					float gapRight = oRight.transform.position.x - oRight.GetComponent<SpriteRenderer> ().bounds.extents.x;
					float gapWidth = gapRight - gapLeft;
					if (gapWidth > minGapWidthToJump)
					{
						JumpGap (gapWidth);
					}
				}
			}
		}
		//
	}

	void JumpObstacle(Vector2 raycastOrigin, GameObject obs)
	{
		if (grounded)
		{
			float obsHeightAbovePizzaMiddle = 
				obs.transform.position.y +
				obs.GetComponent<SpriteRenderer>().bounds.extents.y - 
				raycastOrigin.y;
			
			float jumpForce = obsHeightAbovePizzaMiddle * speed.y;

			//if (Vector2.Distance (raycastOrigin, obs.transform.position) <
			//   wallSensorDistance * jumpForce * 0.3f) //The smaller the obstacle is, the later it has to jump
			{
				jumpForce = Mathf.Min (speed.y, jumpForce) * 1.2f;;
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
			}
		}
	}

	void JumpGap(float gapWidth)
	{
		if (grounded)
		{
			float jumpForce = Mathf.Min (speed.y, gapWidth * speed.y) * 1.2f;
			rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		foreach (ContactPoint2D contact in col.contacts) 
		{
			if (contact.normal.y > 0.9f)
			{
				grounded = true;
				break;
			}
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		foreach (ContactPoint2D contact in col.contacts) 
		{
			if (contact.normal.y > 0.9f)
			{
				grounded = false;
				break;
			}
		}
	}
}
