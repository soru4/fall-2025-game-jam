using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "Hacking", menuName = "Scriptable Objects/Hacking")]
public class Hacking : ScriptableObject
{
	public int dayTriggered; 
	
	public List<Files> files; 
	public GameObject reference; 
	[System.Serializable]
	public struct Files{
		public string fileName; 
		public string fileContents; 
	}
}
