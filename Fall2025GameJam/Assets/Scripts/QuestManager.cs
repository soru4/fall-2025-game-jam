using UnityEngine;

public class QuestManager : MonoBehaviour
{
	
	public static QuestManager inst; 
	// Start is called once before the first execution of Update after the MonoBehaviour is created
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
        
    }
	[System.Serializable]
	public struct Dialogue{
		public string text; 
		public AudioClip voiceLine; 
		public int dayTriggered; 
		
		public Dialogue(string t, AudioClip x, int d){
			text = t; 
			voiceLine = x; 
			dayTriggered = d;
		}
		
	}    
}


