using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic; 
using System.Collections;
public class HackingMiniGame : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public static HackingMiniGame inst;
	public bool playingMinigame; 
	public List<string> shapesChoice;
	public List<string> randomShapes; 
	public List<string> inputtedShapes; 
	public GameObject minigame; 
	public GameObject buttonHolders; 
	public TextMeshProUGUI displayShape;
	public TextMeshProUGUI inputtedDisplay; 
	int clicks; 
	public GameObject hacking; 
	
    void Start()
    {
	    inst = this; 
	    minigame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	protected void FixedUpdate()
	{
		if(playingMinigame){
			string textInputted = "";
			foreach(string x in inputtedShapes){
				textInputted += x + " "; 
			}
			inputtedDisplay.text = textInputted; 
			string textInputted1 = "";
			foreach(string x in randomShapes){
				textInputted1 += x + " "; 
			}
			
			if(textInputted1 == textInputted && clicks >= 3){
				//minigameSuccess
				
				playingMinigame = false; 
				minigame.SetActive(false);
			}
			else if(clicks >= 5 && textInputted != textInputted1){
				hacking.SetActive(false);
				playingMinigame = false; 
				minigame.SetActive(false);
			}
		}
		
	}
	

	public void ChooseRandomShapes()
	{
		
		randomShapes.Clear();

	
		if (shapesChoice.Count == 0)
		{
			Debug.LogError("The shapesChoice list is empty. Please add shapes.");
			return;
		}

		for (int i = 0; i < 5; i++)
		{
			
			int randomIndex = Random.Range(0, shapesChoice.Count);
            
			
			randomShapes.Add(shapesChoice[randomIndex]);
		}
	}
	
	public void startNewMinigame(){
		minigame.SetActive(true);
		playingMinigame = true; 
		inputtedShapes = new List<string>();
		inputtedDisplay.text = "";
		clicks = 0; 
		buttonHolders.SetActive(false); 
		ChooseRandomShapes();
		StartCoroutine(DisplayShapesWithDelay());
	}
	
	
	private IEnumerator DisplayShapesWithDelay()
	{
		if (randomShapes.Count == 0)
		{
			Debug.LogWarning("No shapes to display. The list is empty.");
			yield break;
		}

		yield return new WaitForSeconds(1.3f);
		foreach (string shape in randomShapes)
		{
			displayShape.text = "";
			yield return new WaitForSeconds(0.3f);
			displayShape.text = shape.ToString();
            
			yield return new WaitForSeconds(2f);
		}
        
		displayShape.text = "";
		buttonHolders.SetActive(true);
	}
	
	
	public void addShape(string x){
		clicks ++; 
		inputtedShapes.Add(x);
	}
}
