using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour 
{
	List<GameObject> characters;

	public Vector2 zoomLimits = new Vector2(0.5f, 3.0f);
	public float yOffset = 3.0f;
	public float zoomFactor = 1.0f;

	void Start () 
	{
		characters = new List<GameObject>(GameObject.FindGameObjectsWithTag ("Player"));
		characters.Add( GameObject.FindGameObjectWithTag ("Pizza") );
	}

	void Update () 
	{
		Vector2 min, max;
		GetBounds(out min, out max);
		Vector2 center = (min + max) / 2.0f;

		Vector2 extents = (max - min) / 2.0f;
		float orthoSize = Mathf.Clamp (extents.magnitude / zoomFactor, zoomLimits.x, zoomLimits.y);
		GetComponent<Camera> ().orthographicSize = orthoSize;

		transform.position = new Vector3(center.x, center.y + yOffset * orthoSize, transform.position.z);
	}

	void GetBounds(out Vector2 min, out Vector2 max)
	{
		float minX = characters [0].transform.position.x;
		float maxX = characters [0].transform.position.x;
		float minY = characters [0].transform.position.y;
		float maxY = characters [0].transform.position.y;

		foreach(GameObject c in characters)
		{
			minX = Mathf.Min(c.transform.position.x, minX);
			maxX = Mathf.Max(c.transform.position.x, maxX);
			minY = Mathf.Min(c.transform.position.y, minY);
			maxY = Mathf.Max(c.transform.position.y, maxY);
		}

		min = new Vector2 (minX, minY);
		max = new Vector2 (maxX, maxY);
	}
}
