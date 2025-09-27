using UnityEngine;
using UnityEngine.Video; 
public class Cutscene : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public VideoPlayer videoPlayer; 
	public GameObject cutscene; 
	public static Cutscene inst; 
	void Start()
    
	
	{
		
		inst = this; 
		cutscene = QuestManager.inst.cutScene;
		if(Time.time < 20){
		videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"0001-0796.mp4"); 
	}
	   
    }

    // Update is called once per frame
    void Update()
    {
	    //videoPlayer.playbackSpeed = 1; 
    }
    
	public void PlayEndingCutscene( string person){
		
	}
	
	public void newsPaper(string person){
		QuestManager.inst.cutScene.SetActive(true);
		
		if(person == "Hackley"){
			videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"won_newspaper.mp4"); 
		}else{
			videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"lost_newspaper.mp4"); 
		}
		videoPlayer.Play();
		videoPlayer.playbackSpeed = 0.5f;
	}
}
