using UnityEngine;
using Vuforia;
using System.Collections;
using Random = UnityEngine.Random;


public class VBGolfBallEventHandler : MonoBehaviour, IVirtualButtonEventHandler {

	#region PRIVATE_MEMBER_VARIABLES
	
	private GameObject mGolfball;
	private bool mIsRolling = false;
	private float mTimeRolling = 0.0f;
	private float mForce = 0.4f;
	private float[] hitForce = {1,2,3,4,5,6,7,8,9,10};
	private float applyForce;
	private string checkField;
	private float addDirt = 0.2f;
	private float addMoisture = 0.7f;
	private float windDir , windDir1 = 1.0f;
	private string windVal;


	
	#endregion // PRIVATE_MEMBER_VARIABLES
	
	
	
	#region PUBLIC_METHODS
	

	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
	{
		Debug.Log("OnButtonPressed");
		HitGolfball();
	}
	

	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
	{
		Debug.Log("OnButtonReleased");
	}
	
	#endregion // PUBLIC_METHODS
	
	
	
	#region UNTIY_MONOBEHAVIOUR_METHODS
	
	void Start()
	{

		mGolfball = transform.FindChild("Golfball_VB").gameObject;
		

		VirtualButtonBehaviour vb =
			GetComponentInChildren<VirtualButtonBehaviour>();
		if (vb)
		{
			vb.RegisterEventHandler(this);

		}
		

		mForce *= transform.localScale.x;
	}
	
	
	void Update()
	{
		mTimeRolling += Time.deltaTime;
		

		if (mIsRolling && mTimeRolling > 1.0f &&
		    mGolfball.GetComponent<Rigidbody>().velocity.magnitude < 5)
		{
			mGolfball.GetComponent<Rigidbody>().Sleep();
			mIsRolling = false;
		}
	}
	
	#endregion // UNTIY_MONOBEHAVIOUR_METHODS
	
	
	
	#region PRIVATE_METHODS
	

	public float GetRandomForce()
	{

		float Frc = hitForce [Random.Range (1, hitForce.Length)];
		return Frc;

	}
	

	public string GetField()
	{

		if (GameObject.FindGameObjectsWithTag ("Grass") == null) 
		{
			if (GameObject.FindGameObjectsWithTag ("Beach") == null) 
			{
				if (GameObject.FindGameObjectsWithTag ("Road") == null)
				{
					return "No Field";
				} 
				else 
				{
					return "Road";
				}
			} 
			else 
			{
				return "Beach";
			}
		} 
		else 
		{
			return "Grass";
		}

	}


	private void HitGolfball()
	{


		applyForce = GetRandomForce ();

		checkField = GetField ();

		Wind w = new Wind ();

		if (checkField == "Beach") 
		{
			applyForce = applyForce * addDirt;
			windVal = w.GetWindDirection();
			windDir = w.GetNSValues(windVal);
			windDir1 = w.GetEWValues(windVal);

		} 
		else if (checkField == "Road") 
		{
			applyForce = applyForce * addMoisture;
			windVal = w.GetWindDirection();
			windDir = w.GetNSValues(windVal);
			windDir1 = w.GetEWValues(windVal);
		} 
		else if (checkField == "Grass") 
		{
			windVal = w.GetWindDirection();
			windDir = w.GetNSValues(windVal);
			windDir1 = w.GetEWValues(windVal);
		}


		Bounds targetBounds = this.GetComponent<Collider>().bounds;
		Rect targetRect = new Rect( -targetBounds.extents.x,
		                           -targetBounds.extents.z,
		                           targetBounds.size.x,
		                           targetBounds.size.z);
		

		Vector2 randomDir = new Vector2();
		for (int i = 0; i < 20; i++)
		{
			randomDir = Random.insideUnitCircle.normalized;
			

			Vector3 pos = mGolfball.transform.localPosition *
				this.transform.localScale.x;
			
		
			Vector2 finalPos = new Vector2(pos.x, pos.z) +
				randomDir * mForce * 1.5f;
			
			if (targetRect.Contains(finalPos))
			{
				break;
			}
		}
		

		Vector3 shotDir = new Vector3(20*windDir, 0, 20*windDir1).normalized;
		

		Vector3 torqueDir = Vector3.Cross(Vector3.up, shotDir).normalized;
		

		Rigidbody rb = mGolfball.GetComponent<Rigidbody>();
		rb.AddForce(shotDir * applyForce, ForceMode.VelocityChange);
		rb.AddTorque(torqueDir * applyForce, ForceMode.VelocityChange);
		
		mIsRolling = true;
		mTimeRolling = 0.0f;

	}
	
	#endregion // PRIVATE_METHODS
}
