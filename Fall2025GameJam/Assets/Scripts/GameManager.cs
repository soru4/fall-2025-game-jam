using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public static GameManager inst; 
	public int day = 0; // 0, 1, 2, 3, 4, 5. 
	public int hour = 0; // 0 - 24
	public int actionsToday = 5; 
	public int[] actionsPerDay = {5,4,3,3,3,1,1};
	public TextMeshProUGUI dayName; 
	public TextMeshProUGUI actionsLeft;
	public TextMeshProUGUI dayNumber; 

	public QuestTNode currentTNode; 
	public List<Toggle> toggles; 
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		inst = this; 
	}
	void Start()
    {
	    hour = 8; 
    }

    // Update is called once per frame
    void Update()
	{
		dayNumber.text = "Day: " +(day +1);
		dayName.text = day == 0 ? "Monday" : day == 1 ? "Tuesday" : day == 2? "Wednesday" : day == 3 ? "Thursday" : day == 4 ? "Friday" : "null";
		
		actionsLeft.text = actionsToday + " Actions Left";
	
		if(actionsToday <=0)
	    	CheckHour();
    }
	void CheckHour(){
		day += 1;
		if(day == 5){
			foreach(Toggle x in toggles){
				if(x.isOn){
					//Cutscene.inst.PlayEndingCutscene(x.gameObject.name);
					Cutscene.inst.newsPaper(x.gameObject.name);
					break;
				}
			}
		}
		actionsToday = actionsPerDay[day];
	
		
		
	}
	
}
