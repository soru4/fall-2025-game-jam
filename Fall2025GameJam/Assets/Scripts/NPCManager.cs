using UnityEngine;

public class NPCManager : MonoBehaviour
{
	public string name; 
	public Transform cubiclePos;
	public Transform waterCoolerPos; 
	public Transform ceoOfficeOutside; 
	public Transform depart; 
	public int lunchTime; 
	public int departTime; 
	public int enterTime; 
	public QuestTNode Dialogue; 
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
	    lunchTime = (int)Random.Range(12,14); 
	    departTime = (int) Random.Range(16, 18);
	    enterTime = (int)Random.Range(8,10);
	    //  GetComponent<NPCPathfinding>().SetDestination(ceoOfficeOutside.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
	    TimeCheck();
    }
    
	public void TimeCheck(){
		
		
		
	}
    
	public void SetUpDialogue(){
	
		
		
	
	}
	
	public QuestTNode FindDialogue(int day){
		
		
		return dialogueHelper(day, Dialogue); 
	}
	
	public QuestTNode dialogueHelper(int day, QuestTNode prev){
		if(prev.currentDialogue.dayTriggered == day)
			return prev; 
			
		if(prev.playerResponse1!= null)
			return dialogueHelper(day, prev.response1Next);
		if(prev.playerResponse2!= null)
			return dialogueHelper(day, prev.response2Next);
		if(prev.playerResponse2 == null && prev.playerResponse1 == null)
			return dialogueHelper(day, prev.next );
			
		return null; 
			
			
	}

}
