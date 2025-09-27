using UnityEngine;

public class Menu : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public GameObject credits; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void Quit(){
		Application.Quit();
	}
	
	public void Credits(){
		credits.SetActive(!credits.activeInHierarchy);
	}
}
