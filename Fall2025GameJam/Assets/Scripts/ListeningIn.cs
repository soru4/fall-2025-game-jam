using UnityEngine;
using System.Collections.Generic; 
[CreateAssetMenu(fileName = "ListeningIn", menuName = "Scriptable Objects/ListeningIn")]
public class ListeningIn : ScriptableObject
{
	public int dayTriggered; 
	
	public List<Dialogues> list; 
	
	
	
	
	
	
}


[System.Serializable]
	public class Dialogues{
		[SerializeReference]string person; 
		[SerializeReference]string text; 
	}