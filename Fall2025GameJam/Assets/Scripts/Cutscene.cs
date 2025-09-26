using UnityEngine;
using UnityEngine.Video; 
public class Cutscene : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public VideoPlayer videoPlayer; 
    
    void Start()
    {
	    videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"0001-0796.mp4"); 
	    videoPlayer.playbackSpeed = 1; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
