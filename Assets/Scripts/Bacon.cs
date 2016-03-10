using UnityEngine;
using System.Collections;

public class Bacon : MonoBehaviour 
{
	private Rigidbody2D rb;
	private bool grounded = false;

	public float speedDrop;
	public Vector2 speed;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update ()
	{
		if (Input.GetKey(KeyCode.D)) 
		{
			rb.velocity = new Vector2 (speed.x, rb.velocity.y);
		} 
		else if (Input.GetKey(KeyCode.A)) 
		{
			rb.velocity = new Vector2 (-speed.x, rb.velocity.y);
		} 
		else 
		{
			rb.velocity = new Vector2 (rb.velocity.x * speedDrop, rb.velocity.y);
		}	

		if (grounded && Input.GetKeyDown(KeyCode.Space)) 
		{
			rb.velocity = new Vector2(rb.velocity.x, speed.y);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		foreach (ContactPoint2D contact in col.contacts) 
		{
			if (contact.normal.y > 0.5f)
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
