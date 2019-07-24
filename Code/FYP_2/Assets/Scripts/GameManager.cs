using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	
	public static GameManager instance = null;
	public FieldManager fieldScript;
	
	private int level = 3;
	
	// Use this for initialization
	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);
		
		fieldScript = GetComponent<FieldManager> ();
		InitGame ();
		
	}
	
	void InitGame()
	{
		
		fieldScript.SetupScene (level);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
}
