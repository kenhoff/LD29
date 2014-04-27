using UnityEngine;
using System.Collections;

public class SharkCounter : MonoBehaviour {

	public static int score;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Score: " + score);	
	}

	public static void IncrementScore () {
		score += 1;
	}

	void OnGUI () {
		GUIStyle style = new GUIStyle();
		style.fontSize = 100;
		Rect rect = new Rect(Screen.width/2 - 50, Screen.height - 100, 50, 50);
		GUI.Label(rect, score.ToString(), style);
	}

}
