using UnityEngine;
using System.Collections;

public class PizzaAIMozzDiceThrower : MonoBehaviour 
{
	[SerializeField]
	private MozzDice mozzDicePrefab;

	private float time;

	private float nextThrowMozzDiceTime;
	public float minThrowMozzDiceTime, 
				 maxThrowMozzDiceTime;

	void Start () 
	{
		RecalcNextThrowMozzDiceTime();
	}

	void Update () 
	{
		time += Time.deltaTime;

		// Throw MozzDices
		if (time >= nextThrowMozzDiceTime)
		{
			ThrowMozzDice();
			RecalcNextThrowMozzDiceTime();
		}
	}

	void ThrowMozzDice()
	{
		GameObject mozzDice = 
			GameObject.Instantiate(mozzDicePrefab.gameObject,
								   transform.position, 
								   Quaternion.identity) as GameObject;
	}

	void RecalcNextThrowMozzDiceTime()
	{
		time = 0.0f;

		//The closer some player is, the fastest it throws dices
		float proximityMult = 0.0f; 
		float minDist = 999.9f;
		foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
		{
			minDist = Mathf.Min(minDist, Vector2.Distance(p.transform.position, transform.position));
		}
		

		proximityMult = minDist / 20.0f;
		nextThrowMozzDiceTime = Random.Range(minThrowMozzDiceTime, maxThrowMozzDiceTime) * proximityMult;
	}
}
