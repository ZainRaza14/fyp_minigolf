using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;





public class FieldManager : MonoBehaviour 
{
	
	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;
		
		public Count(int min,int max)
		{
			minimum = min;
			maximum = max;
			
		}
		
	}
	
	public int columns = 8;
	
	public int rows = 8;
	

	//public GameObject exit;
	public GameObject[] Flags;
	public GameObject[] Golfballs;
	public GameObject[] Obstacles;
	public GameObject[] Tracks;

	

	private GameObject Trackspawn;
	private GameObject BallSpawn;
	private Transform fieldHolder;
	private List <Vector3>fieldPositions = new List<Vector3>();
	
	void InitialiseList()
	{
		fieldPositions.Clear();
		
		for (int x = 1; x < columns - 1; x++) 
		{
			for(int y = 1; y < rows -1; y++)
			{
				
				fieldPositions.Add(new Vector3(x,y,8f));
				
			}
			
		}
		
		
	}
	
	void FieldSetup()
	{
		
		fieldHolder = new GameObject ("Field").transform;
		
		for (int x = -1; x < columns + 1; x++) 
		{
			for(int y = -1; y < rows + 1; y++)
			{
				GameObject toInstantiate = Tracks[Random.Range (0,1)];
				//if(x == -1 || x == columns || y == -1 || y == rows )
				//	toInstantiate = Tracks[Random.Range(0,Tracks.Length)];
				
				GameObject instance = Instantiate(toInstantiate , new Vector3(x,y,0f) , Quaternion.identity) as GameObject;
				
				instance.transform.SetParent(fieldHolder);
				
				
			}
			
			
		}
		
		
	}
	
	Vector3 RandomPosition()
	{
		
		int randomIndex = Random.Range(0, fieldPositions.Count);
		Vector3 randomPosition = fieldPositions [randomIndex];
		fieldPositions.RemoveAt(randomIndex);
		return randomPosition;
		
	}
	
	void LayoutObjectAtRandom_1(GameObject[] req_Array, int minimum, int maximum)
	{
		//int objectCount = Random.Range (minimum, maximum + 1);
		
		//for(int i = 0; i < objectCount ; i++)
		//{
		var My_plane = GameObject.FindGameObjectsWithTag ("for_Field");

		//Vector3 randomPosition = RandomPosition();
		Vector3 randomPosition = My_plane [Random.Range (0, My_plane.Length)].transform.position;
		GameObject tileChoice = req_Array [Random.Range (0, req_Array.Length)];
		Trackspawn =(GameObject) Instantiate (tileChoice, randomPosition, Quaternion.identity);
		Trackspawn.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
		Trackspawn.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
		Trackspawn.transform.Rotate (Vector3.back * Time.deltaTime);


	}
	void LayoutObjectAtRandom_2(GameObject[] req_Array, int minimum, int maximum)
	{

		var My_plane1 = GameObject.FindGameObjectsWithTag ("for_Ball");
		
		//Vector3 randomPosition = RandomPosition();
		Vector3 randomPosition = My_plane1 [Random.Range (0, My_plane1.Length)].transform.position;
		GameObject tileChoice = req_Array [Random.Range (0, req_Array.Length)];
		BallSpawn  =(GameObject) Instantiate (tileChoice, randomPosition, Quaternion.identity);

		BallSpawn.transform.Translate(Vector3.up * 0.1f, Space.World);
	}

	void LayoutObjectAtRandom_3(GameObject[] req_Array, int minimum, int maximum)
	{


		var My_plane2 = GameObject.FindGameObjectsWithTag ("for_Obstacles");
		
		//Vector3 randomPosition = RandomPosition();
		Vector3 randomPosition = My_plane2 [Random.Range (0, My_plane2.Length)].transform.position;
		GameObject tileChoice = req_Array [Random.Range (0, req_Array.Length)];
		Instantiate (tileChoice, randomPosition, Quaternion.identity);

	}

	void LayoutObjectAtRandom_4(GameObject[] req_Array, int minimum, int maximum)
	{

		var My_plane3 = GameObject.FindGameObjectsWithTag ("for_Flags");
		
		//Vector3 randomPosition = RandomPosition();
		Vector3 randomPosition = My_plane3 [Random.Range (0, My_plane3.Length)].transform.position;
		GameObject tileChoice = req_Array [Random.Range (0, req_Array.Length)];
		Instantiate (tileChoice, randomPosition, Quaternion.identity);

			
		//}

	}
		

	
	public void SetupScene(int level)
	{
		
		//FieldSetup ();
		//InitialiseList ();
		LayoutObjectAtRandom_2 (Golfballs, 1, 1);
		LayoutObjectAtRandom_4 (Flags, 1, 1);
		int obstacleCount = (int)Mathf.Log (level, 2f);
		LayoutObjectAtRandom_3 (Obstacles, 1, 1);
		LayoutObjectAtRandom_1 (Tracks, 1, 1);

		
	}
	
	
}
