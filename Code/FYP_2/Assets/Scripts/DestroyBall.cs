using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroyBall : MonoBehaviour 

{
	public AudioClip ballHoledSound;
	public Text scoreText;

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "soccerball")
		{
			SoundManager.instance.PlaySingle(ballHoledSound);
			scoreText.text = "Score: " + 10;
			col.gameObject.SetActive(false);
	
		}
	}

}
