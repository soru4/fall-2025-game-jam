using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public static GameManager inst; 
	int day = 0; // 0, 1, 2, 3, 4, 5. 
	int hour = 0; // 0 - 24
	

	public QuestTNode currentTNode; 
    
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		inst = this; 
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    CheckHour();
    }
	void CheckHour(){
		day += hour > 24 ? 1 : 0;  
		hour = hour > 24 ? 0 : hour; 
		
	}
	//Syntax in questTNode: 0.3 (percentage of day to elapse) : 0.1 (decimal percentage of tiredness to increase)
	
	public void parseQuestInteractions(string x){
		string[] diff = x.Split(":"); 
		hour += (int)(double.Parse(diff[0]) * 24); 
		//deal with tiredness later. 
		
		
	}
}
