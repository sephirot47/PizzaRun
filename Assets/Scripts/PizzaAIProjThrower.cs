using UnityEngine;
using System.Collections;

public class PizzaAIProjThrower : MonoBehaviour 
{
	[SerializeField]
	private MozzDice mozzDicePrefab;

	private float time = 0.0f;

	public float minTimeBetweenProj = 1.0f;
	public float throwProbabilityMult = 0.1f;

	void Start () 
	{
	}

	void Update () 
	{
		time += Time.deltaTime;

		// Throw Projs
		if (time >= minTimeBetweenProj && 
			Random.Range(0.0f, 1000.0f) >= GetMinDistToPlayers() / throwProbabilityMult)
        {
            time = 0.0f;
			ThrowProjectile();
		}
	}

	void ThrowProjectile()
	{
		GameObject proj;
		proj = 	GameObject.Instantiate(mozzDicePrefab.gameObject,
								   	   transform.position, 
								       Quaternion.identity) as GameObject;
	}

	float GetMinDistToPlayers()
	{
		float minDist = 999.9f;
		foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
		{
			minDist = Mathf.Min(minDist, Vector2.Distance(p.transform.position, transform.position));
		}

		return minDist;
	}
}
