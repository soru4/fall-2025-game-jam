using UnityEngine;

public class NPCPathfinding : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public  System.Collections.Generic.List<PathfindingTile> allPathfindingTiles; 
	public Vector3 destination;
	public PathfindingTile movingTowardsTile; 
	public float speed; 
	public Vector3 previousPosition; 
	//Finds the closest pathfinding tile. Look at all of the bordering tiles and see which one leads to a closer pos to desired Destination. 
    void Start()	
	{
		previousPosition = transform.position; 
		foreach(GameObject x in GameObject.FindGameObjectsWithTag("Tile"))
			allPathfindingTiles.Add(x.GetComponent<PathfindingTile>());
		
    }

    // Update is called once per frame
    void Update()
    {
	    Vector3 currentPosition = transform.position;
	    Vector3 movementDirection = (currentPosition - previousPosition).normalized;
	    previousPosition = currentPosition; // Update for the next frame
	    
	    movingTowardsTile.transform.position = new Vector3(movingTowardsTile.transform.position.x, transform.position.y, movingTowardsTile.transform.position.z);
	    transform.position = Vector3.MoveTowards(transform.position, movingTowardsTile.transform.position, speed*Time.deltaTime);
	    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementDirection), 0.1f);
	    if(Vector3.Distance(transform.position, destination) <= 4 && Vector3.Distance(transform.position, destination) > 0.3f){
		    transform.position = Vector3.MoveTowards(transform.position, destination, 2*Time.deltaTime);
		    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementDirection), 0.1f);
	    	speed = 0; 
	    	
	    }
	    else if(Vector3.Distance(transform.position, movingTowardsTile.transform.position) <= 1f){
	    	movingTowardsTile = ChooseNextTile();
	    }
	    
	    
    }
	public void SetDestination(Vector3 dest){
		speed = 2; 
		destination = dest; 
		float dist = 7f;
		PathfindingTile lowestDistTile = null;
		foreach(PathfindingTile x in allPathfindingTiles){
			if(Vector3.Distance(transform.position, x.transform.position) <= dist){
				lowestDistTile = x; 
				dist = Vector3.Distance(transform.position, x.transform.position);
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
