using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "Hacking", menuName = "Scriptable Objects/Hacking")]
public class Hacking : ScriptableObject
{
	public int dayTriggered; 
	
	public List<Files> files; 
	[System.Serializable]
	public struct Files{
		[SerializeReference] string fileName; 
		[SerializeReference] string fileContents; 
	}
}
