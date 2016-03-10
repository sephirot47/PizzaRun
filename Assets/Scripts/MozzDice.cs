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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (transform.parent == null)
        {
            //SetParent(col.gameObject.transform);
            rb.velocity = Vector2.zero;
        }
    }
    /*
    public void SetParent(Transform parent) 
    {
        transform.rotation = Quaternion.identity;
        Vector2 lastScale = transform.localScale;
        transform.parent = parent;
        transform.localScale = 
            new Vector2(
            lastScale.x / transform.parent.lossyScale.x,
            lastScale.y / transform.parent.lossyScale.y);
    }
    */
}
