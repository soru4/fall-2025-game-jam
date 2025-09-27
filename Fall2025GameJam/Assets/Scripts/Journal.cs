using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class Journal : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public static Journal inst; 
	public List<JournalEntry> entries; 
	public List<Choice> choices; 
	public TextMeshProUGUI personText; 
	
    void Start()
    {
	    inst = this; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public  void addJournalEntry(string person, string text){
		entries.Add(
			new JournalEntry(GameManager.inst.day, person, text)
		);
	}
	
	public void showJournalPerson(string person){
		
		string text = ""; 
		foreach(JournalEntry x in entries){
			if(x.person == person){
				text += "Day: " + x.day + "\n" + x.text + "\n";
			}
		}
		personText.text = text;
	}
	
	
	[System.Serializable]
	public struct JournalEntry{
		public int day; 
		public string person; 
		public string text; 
		public JournalEntry(int d, string p, string t){
			day = d; 
			person = p; 
			text = t; 
		}
		
	}
	public struct Choice{
		public string person; 
		public bool thought; 
	}
}
