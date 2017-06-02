using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {

	public Text DialogueText;
	public Text RoomText;
	public Text OptionText;

	private enum States { start, cell, mirror, cell_mirror, lock_0, lock_1, sheets_0, sheets_1, freedom };
	private States currentState;

	// Use this for initialization
	void Start () {
		currentState = States.start;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState)
		{
		case States.start:
			state_start ();
			break;
		case States.cell:
			state_cell ();
			break;
		default:
			state_start ();
			break;
		}
	//	if (currentState == States.cell)
	//		state_cell ();
	}

	void state_start () {
		DialogueText.text = "";
		RoomText.text = "";
		OptionText.text = "Press (Space) to start.";

		if (Input.GetKeyDown (KeyCode.Space))
			currentState = States.cell;
	}

	void state_cell () {
		DialogueText.text = "You awake in a dark cell. A single pinprick of light shines through the smallest of cracks in the ceiling. "
			+ "In the near-dark you can make out a pile of Sheets on the floor; a cracked and dirty Mirror on the wall; "
				+ "a rusted - and very much locked - Lock on the wooden door that leads to...somewhere.";
		RoomText.text = "The Cell";
		OptionText.text = "(M) Look at Mirror - (S) Look at Sheets - (L) Look at Lock";
	}
}
