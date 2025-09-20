using UnityEngine;

public class NPCPathfinding : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public static System.Collections.Generic.List<PathfindingTile> allPathfindingTiles; 
	public Vector3 destination;
	public PathfindingTile movingTowardsTile; 
	//Finds the closest pathfinding tile. Look at all of the bordering tiles and see which one leads to a closer pos to desired Destination. 
    void Start()	
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
		
	    transform.position = Vector3.MoveTowards(transform.position, movingTowardsTile.transform.position, 2*Time.deltaTime);
	    if(Vector3.Distance(transform.position, movingTowardsTile.transform.position) <= 1f){
	    	movingTowardsTile = ChooseNextTile();
	    }
    }
	public void SetDestination(Vector3 dest){
		destination = dest; 
		float dist = 100000;
		PathfindingTile lowestDistTile = null;
		foreach(PathfindingTile x in allPathfindingTiles){
			if(Vector3.Distance(transform.position, x.transform.position) <= dist){
				lowestDistTile = x; 
			}
		}
		movingTowardsTile = lowestDistTile; 
	}
	public PathfindingTile ChooseNextTile()
	{
		
		float minDistance = float.MaxValue;

		PathfindingTile closestTile = null;

		if (movingTowardsTile != null && movingTowardsTile.borderingTiles != null)
		{
			foreach (PathfindingTile tile in movingTowardsTile.borderingTiles)
			{
				float currentDistance = Vector3.Distance(destination, tile.transform.position);

				if (currentDistance < minDistance)
				{
					minDistance = currentDistance;

					closestTile = tile;
				}
			}
		}

		return closestTile;
	}
	
}
