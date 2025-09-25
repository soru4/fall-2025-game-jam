using UnityEngine;
using TMPro; 

public class QuestManager : MonoBehaviour
{
	
	public static QuestManager inst; 
	public bool inInterrogation; 
	public bool listeningIn; 
	public QuestTNode currQuestTNode; 
	public ListeningIn currListeningIn; 
	public UnityEngine.UI.Button continueForward; 	public TextMeshProUGUI charSpeaking; 
	public TextMeshProUGUI questText; 
	
	public UnityEngine.UI.Button response1; 
	public UnityEngine.UI.Button response2;
	
	public TextMeshProUGUI response1Text; 
	public TextMeshProUGUI response2Text; 
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
	    if(Input.GetKeyDown(KeyCode.Escape)){
	    	//menu
	    }else if(Input.GetKeyDown(KeyCode.Alpha1)){
	    	//menu
	    	print("1");
	    }else if(Input.GetKeyDown(KeyCode.Alpha2)){
	    	//menu
	    	print("2");
	    }
	    else if(Input.GetKeyDown(KeyCode.Alpha3)){
	    	//menu
	    	print("3");
	    }
    }
    
	public void startQuest(){
		
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


