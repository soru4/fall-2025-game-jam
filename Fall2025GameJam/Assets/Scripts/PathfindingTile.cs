using UnityEngine;
using System.Collections.Generic; 
public class PathfindingTile : MonoBehaviour
{
	
	public Vector3 destination;
	public List<PathfindingTile> borderingTiles; 
	public List<GameObject> occupiedBy; 
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
		foreach(GameObject x in tiles){
			if(x == this.gameObject)
				continue; 
			if(Vector3.Distance(transform.position, x.transform.position) < 2.5f){
				borderingTiles.Add(x.GetComponent<PathfindingTile>());
			}
		}
	}
}
