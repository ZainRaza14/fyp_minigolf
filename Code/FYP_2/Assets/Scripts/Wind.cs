using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour 
{
	private float addDirt = 0.2f;
	private float addMoisture = 0.7f;
	private string[] windDirections = {"N","E","W","S"};
	private float[] NValues = {0.1f,0.2f,0.3f,0.4f,0.5f,0.6f,0.7f,0.8f,0.9f};
	private float[] EValues = {-0.1f,-0.2f,-0.3f,-0.4f,-0.5f,-0.6f,-0.7f,-0.8f,-0.9f};
	private float[] WValues = {0.1f,0.2f,0.3f,0.4f,0.5f,0.6f,0.7f,0.8f,0.9f};
	private float[] SValues = {-0.1f,-0.2f,-0.3f,-0.4f,-0.5f,-0.6f,-0.7f,-0.8f,-0.9f};


	public Wind()
	{

	}

	public void InitAll_windVariables()
	{

	}

	public string GetWindDirection()
	{
		string wnd = windDirections [Random.Range (1, windDirections.Length)];
		return wnd;
	}
	
	public float GetNSValues(string dir)
	{
		if (dir == "N") 
		{
			return NValues [Random.Range (1, NValues.Length)];
		}
		
		else if (dir == "S")
		{
			return SValues [Random.Range (1, SValues.Length)];
		}
		
		return 1.0f;
	}
	
	public float GetEWValues(string dir)
	{
		
		if (dir == "E")
		{
			return EValues [Random.Range (1, EValues.Length)];
		}
		else if (dir == "W")
		{
			return WValues [Random.Range (1, WValues.Length)];
		}
		
		return 1.0f;
	}

}
