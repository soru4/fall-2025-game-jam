using UnityEngine;

[CreateAssetMenu(fileName = "QuestTNode", menuName = "Scriptable Objects/QuestTNode")]
[System.Serializable]
public class QuestTNode : ScriptableObject{
	public string name; 
	public QuestManager.Dialogue currentDialogue;
	public string playerResponse1; 
	public string playerResponse2; 
	[SerializeReference] public QuestTNode response1Next; 
	[SerializeReference] public QuestTNode response2Next; 
	
	[SerializeReference] public QuestTNode next; 
	
	public QuestTNode(QuestManager.Dialogue x, string res1, string res2, QuestTNode res1N = null, QuestTNode res2N=null, QuestTNode n = null){
		this.currentDialogue = x; 
		this.playerResponse1 = res1;
		this.playerResponse2 = res2;
		this.response1Next = res1N;
		this.response2Next = res2N; 
		this.next = n; 
	}
	
}
