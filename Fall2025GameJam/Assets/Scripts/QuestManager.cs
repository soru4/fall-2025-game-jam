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
	}    
}

[System.Serializable]
public class QuestTNode{
	public QuestManager.Dialogue currentDialogue;
	public string playerResponse1; 
	public string playerResponse2; 
	public QuestTNode response1Next; 
	public QuestTNode response2Next; 
	
	public QuestTNode next; 
	
	public QuestTNode(QuestManager.Dialogue x, string res1, string res2, QuestTNode res1N = null, QuestTNode res2N=null, QuestTNode n = null){
		this.currentDialogue = x; 
		this.playerResponse1 = res1;
		this.playerResponse2 = res2;
		this.response1Next = res1N;
		this.response2Next = res2N; 
		this.next = n; 
	}
	
}

