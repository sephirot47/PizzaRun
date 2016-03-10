using UnityEngine;
using System.Collections;

public class MozzDice : MonoBehaviour 
{
	private Rigidbody2D rb;

	public float maxInitialAngVel;
	public Vector2 maxSpeed;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		rb.angularVelocity = maxInitialAngVel * Random.Range (-maxInitialAngVel, maxInitialAngVel);
        rb.velocity = new Vector2(Random.Range(-maxSpeed.x, 0.0f), Random.Range(maxSpeed.y, 0.0f));
	}
	
	void Update ()
	{
		
	}
}
