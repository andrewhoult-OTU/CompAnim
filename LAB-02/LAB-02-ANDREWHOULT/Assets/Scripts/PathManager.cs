using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
	[HideInInspector]
	[SerializeField]
	public List<Waypoint> path;


	public GameObject prefab;
	int currentPointIndex;

	public List<GameObject> prefabPoints;


	public Waypoint GetNextTarget()
	{
		int nextPointIndex = (currentPointIndex + 1) % path.Count;
		currentPointIndex = nextPointIndex;
		return path[nextPointIndex];
	}


	public List<Waypoint> GetPath()
	{
		path ??= new List<Waypoint>();

		return path;
	}

	public void CreateAddPoint()
	{
		Waypoint go = new Waypoint();
		path.Add(go);
	}




	void Start()
	{
		prefabPoints = new List<GameObject>();
		foreach (var p in path)
		{
			var go = Instantiate(prefab);
			go.transform.position = p.pos;
			prefabPoints.Add(go);
		}
	}

	void Update()
	{
		// update all of the paths to waypoints locations
		for (int i = 0; i < path.Count; i++)
		{
			var p = path[i];
			var g = prefabPoints[i];
			g.transform.position = p.pos;
		}
	}

}
