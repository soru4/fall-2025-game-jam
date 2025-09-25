using UnityEngine;
using TMPro; 
using System.Collections.Generic; 

public class QuestManager : MonoBehaviour
{
	
	public static QuestManager inst; 
	public bool inInterrogation; 
	public bool listeningIn; 
	public GameObject dialogueBox;
	public GameObject interogationBox;
	public GameObject hackingBox;
	public QuestTNode currQuestTNode; 
	public ListeningIn currListeningIn; 
		public TextMeshProUGUI charSpeaking; 
	public TextMeshProUGUI questText; 
	
	
	public TextMeshProUGUI response1Text; 
	public TextMeshProUGUI response2Text; 
	public GameObject button1; 
	public GameObject button2; 
	public List<QuestTNode> startingNodes; 
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
	    }else if(Input.GetKeyDown(KeyCode.Alpha1)&&!dialogueBox.activeInHierarchy){
	    	//menu
	    	hackingBox.SetActive(!hackingBox.activeInHierarchy);
	    	print("1");
	    }else if(Input.GetKeyDown(KeyCode.Alpha2)&&!dialogueBox.activeInHierarchy){
	    	//menu
	    	print("2");
	    }
	    else if(Input.GetKeyDown(KeyCode.Alpha3) && !dialogueBox.activeInHierarchy){
	    	//menu
	    	interogationBox.SetActive(!interogationBox.activeInHierarchy);
	    	print("3");
	    }
	    
	    if(Input.GetMouseButtonDown(0) && dialogueBox.activeInHierarchy && currQuestTNode != null){
	    	nextQuestClick();
	    }
    }
    
	public void startQuest(string person){
		GameManager.inst.actionsToday--;
		
		print("Hello: " + person);
		print("Searching for: " + person + " - Day " + (int)(GameManager.inst.day + 1));
		foreach(QuestTNode x in startingNodes){
			if(x.name == person + " - Day " + (int)(GameManager.inst.day + 1)){
				currQuestTNode = x; 
				break; 
			}
			
			
		}
		//find the charachter and move them to proper place;
		if(currQuestTNode != null){
			dialogueBox.SetActive(true); 
			charSpeaking.text = person;
			setUpDialogueLayout();
				
		}
	}
	public void setUpDialogueLayout(){
		interogationBox.SetActive(false);
		
				button1.SetActive(false);
				button2.SetActive(false);
				dialogueBox.SetActive(true); 
				questText.text = currQuestTNode.currentDialogue.text; 
				if(currQuestTNode.response1Next != null){
					button1.SetActive(true);
					response1Text.text = currQuestTNode.playerResponse1;
				}
				if(currQuestTNode.response2Next != null){
					button2.SetActive(true);
					response2Text.text = currQuestTNode.playerResponse2;
				}
		
	}
	public void nextQuestClick(){
		if(currQuestTNode.next!= null){
			currQuestTNode = currQuestTNode.next; 
			setUpDialogueLayout();
		}
		
		if(currQuestTNode.next == null && currQuestTNode.response1Next == null && currQuestTNode.response2Next == null){
			dialogueBox.SetActive(false);
		}
	}
	public void nextQuestClick(bool response){
		if(currQuestTNode.response1Next != null &&response){
			currQuestTNode = currQuestTNode.response1Next; 
			setUpDialogueLayout();
		}else if(currQuestTNode.response2Next != null && !response){
			currQuestTNode = currQuestTNode.response2Next; 
			setUpDialogueLayout();
		}
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


