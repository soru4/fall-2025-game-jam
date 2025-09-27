using UnityEngine;
using TMPro; 
using System.Collections.Generic; 

public class QuestManager : MonoBehaviour
{
	
	public static QuestManager inst; 
	
	public Vector3 defaultCamPos;
	public Vector3 waterCoolerCamPos;
	public Vector3 interogationCamPos; 
	public bool inInterrogation; 
	public bool listeningIn; 
	public Hacking currentHacking; 
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
	
	public List<Hacking> allHackings; 
	
	public GameObject hackingModal; 
	public TextMeshProUGUI nameHacking; 
	public TextMeshProUGUI fileDetailHacking; 
	
	public List<ListeningIn> listeningIns; 
	public int currListeninginProgress; 
	
	public GameObject cutScene; 
	public GameObject gameTitle;
	public GameObject listeningIn1; 
	public GameObject listeningIn2; 
	
	public AudioSource blah; 
	
	public GameObject journalBox; 
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	// Awake is called when the script instance is being loaded.
	
	protected void Awake()
	{
		inst = this; 
	}
    void Start()
    {
	    defaultCamPos = new Vector3(0f,32.5f,31f);
	    Camera.main.transform.position = defaultCamPos; 
	    cutScene.SetActive(true);
	    journalBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
	{
		if(Time.time < 33 && Input.GetKey(KeyCode.Backspace)){
			cutScene.SetActive(false); 
			gameTitle.SetActive(true);
			Time.timeScale = 20; 
		}
		
		if(Time.time >=33 && Time.time < 40){
			cutScene.SetActive(false); 
			gameTitle.SetActive(true);
			
		}
		else if(Time.time > 41 && Time.time < 42){
			gameTitle.SetActive(false);
			Time.timeScale = 1; 
		}
		else{
		
	    if(Input.GetKeyDown(KeyCode.Escape)){
	    	//menu
	    }else if(Input.GetKeyDown(KeyCode.Alpha1)&&!dialogueBox.activeInHierarchy && currListeninginProgress == 0){
	    	//menu
	    	hackingBox.SetActive(!hackingBox.activeInHierarchy);
	    	print("1");
	    }else if(Input.GetKeyDown(KeyCode.Alpha2)&&!dialogueBox.activeInHierarchy&& currListeninginProgress == 0){
	    	//menu
	    	Camera.main.transform.position = waterCoolerCamPos;
	    	startListeningIn();
	    	print("2");
	    }
	    else if(Input.GetKeyDown(KeyCode.Alpha3) && !dialogueBox.activeInHierarchy&& currListeninginProgress == 0){
	    	//menu
	    	interogationBox.SetActive(!interogationBox.activeInHierarchy&& currListeninginProgress == 0);
	    	Camera.main.transform.position = interogationCamPos;
	    	print("3");
	    }
	    else if(Input.GetKeyDown(KeyCode.Alpha4) && !dialogueBox.activeInHierarchy&& currListeninginProgress == 0){
	    	//menu
	    	journalBox.SetActive(!journalBox.activeInHierarchy);
	    	
	    	print("3");
	    }
	    
	    if(Input.GetMouseButtonDown(0) && dialogueBox.activeInHierarchy && currQuestTNode != null&& currListeninginProgress == 0){
	    	nextQuestClick();
	    }
	    if(Input.GetMouseButtonDown(0) && dialogueBox.activeInHierarchy  && currListeninginProgress != 0){
	    	continueListeningIn();
	    }
		}
    }
    
	public void startListeningIn(){
		GameManager.inst.actionsToday--;
		
		print("Hello: " );
		
		foreach(ListeningIn x in listeningIns){
			if(x.name == "Day - " + (int)(GameManager.inst.day + 1)){
				currListeningIn = x; 
				break; 
			}
			
			
		}
		foreach(GameObject x in GameObject.FindGameObjectsWithTag("NPC")){
			if(x.GetComponent<NPCManager>().name == currListeningIn.list[currListeninginProgress].person){
				Vector3 temp = Random.insideUnitSphere*2; 
				Vector3 t1 = new Vector3(temp.x, 0, temp.z); 
				listeningIn2 = x; 
				x.transform.position = x.GetComponent<NPCManager>().waterCoolerPos.transform.position + t1;
			}
		}
		foreach(GameObject x in GameObject.FindGameObjectsWithTag("NPC")){
			if(x.GetComponent<NPCManager>().name == currListeningIn.list[currListeninginProgress+1].person){
				Vector3 temp = Random.insideUnitSphere*2; 
				Vector3 t1 = new Vector3(temp.x, 0, temp.z); 
				listeningIn1 = x; 
				x.transform.position = x.GetComponent<NPCManager>().waterCoolerPos.transform.position + t1;
			}
		}
		
		
		if(currListeninginProgress <= currListeningIn.list.Count){
			dialogueBox.SetActive(true);
			questText.text = currListeningIn.list[currListeninginProgress].text; 
			charSpeaking.text = currListeningIn.list[currListeninginProgress].person; 
			currListeninginProgress++;
		}
		
		
	}
	public void continueListeningIn(){
		
		if(currListeninginProgress < currListeningIn.list.Count){
			dialogueBox.SetActive(true);
			questText.text = currListeningIn.list[currListeninginProgress].text; 
			charSpeaking.text = currListeningIn.list[currListeninginProgress].person; 
			currListeninginProgress++;
		}else{
			currListeninginProgress = 0; 
			dialogueBox.SetActive(false);
			Camera.main.transform.position = defaultCamPos; 
			listeningIn1.transform.position = listeningIn1.GetComponent<NPCManager>().cubiclePos.transform.transform.position;
			listeningIn2.transform.position = listeningIn2.GetComponent<NPCManager>().cubiclePos.transform.position;
		}
	}
	public void startHacking(string person){
		GameManager.inst.actionsToday--;
		
		print("Hello: " + person);
		print("Searching for: " + person + " - Day " + (int)(GameManager.inst.day + 1));
		foreach(Hacking x in allHackings){
			if(x.name == person + " - Day " + (int)(GameManager.inst.day + 1)){
				currentHacking = x; 
				break; 
			}
			
			
		}
		// first we need to go to hacking minigame. 
		//sequence memorizaation things. 
		// then open up a box with all the files. 
		hackingModal.SetActive(true);
		string finalFileDetail = "";
		foreach(Hacking.Files x in currentHacking.files){
			finalFileDetail += x.fileName + "\n" + x.fileContents + "\n"; 
		}
		nameHacking.text = person; 
		fileDetailHacking.text = finalFileDetail; 
		
		hackingBox.SetActive(false);
		
		
		
		
	}
	
	public void closeHackingModal(){
		hackingModal.SetActive(false);
		hackingBox.SetActive(false);
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
		
		foreach(GameObject x in GameObject.FindGameObjectsWithTag("NPC")){
			if(x.GetComponent<NPCManager>().name == person){
				x.transform.position = x.GetComponent<NPCManager>().ceoOfficeOutside.transform.position;
			}
		}
		//find the charachter and move them to proper place;
		if(currQuestTNode != null){
			dialogueBox.SetActive(true); 
			charSpeaking.text = person;
			blah.PlayOneShot(currQuestTNode.currentDialogue.voiceLine);
			setUpDialogueLayout();
				
		}
	}
	public void setUpDialogueLayout(){
		interogationBox.SetActive(false);
		
				button1.SetActive(false);
				button2.SetActive(false);
			dialogueBox.SetActive(true); 
		Journal.inst.addJournalEntry(charSpeaking.text, currQuestTNode.currentDialogue.text);
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
			Camera.main.transform.position = defaultCamPos; 
			foreach(GameObject x in GameObject.FindGameObjectsWithTag("NPC")){
				x.transform.position = x.GetComponent<NPCManager>().cubiclePos.transform.position;
			}
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


