using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {

	public Text DialogueText;
	public Text RoomText;
	public Text OptionText;

	private enum States { start, cell, mirror, cell_mirror, cell_lock_0, cell_lock_1, sheets_0, sheets_1, hall_start, hall_floor, hall_lockers, hall_lockers_lock, hall_lockers_lock_open, hall_stairs, gameover_cell, gameover_stairs, freedom };
	private States currentState;

	public bool hasMirror = false;
	public bool hasUniform = false;
	public bool hasCombination = false;

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
		case States.gameover_cell:
			gameover_cell ();
			break;
		case States.cell:
			state_cell ();
			break;
		case States.sheets_0:
			sheets_0 ();
			break;
		case States.mirror:
			mirror ();
			break;
		case States.cell_lock_0:
			cell_lock_0 ();
			break;
		case States.cell_lock_1:
			cell_lock_1 ();
			break;
		case States.hall_start:
			hall_start ();
			break;
		case States.hall_lockers:
			hall_lockers ();
			break;
		case States.hall_lockers_lock:
			hall_lockers_lock ();
			break;
		case States.hall_lockers_lock_open:
			hall_lockers_lock_open ();
			break;
		case States.hall_floor:
			hall_floor ();
			break;
		case States.hall_stairs:
			hall_stairs ();
			break;
		case States.gameover_stairs:
			gameover_stairs ();
			break;
		case States.freedom:
			freedom ();
			break;
		default:
			state_start ();
			break;
		}
	}

	void state_start () {
		DialogueText.text = "";
		RoomText.text = "";
		OptionText.text = "Press (Space) to start.";

		if (Input.GetKeyDown (KeyCode.Space))
			currentState = States.cell;
	}

	void state_cell () {

		if (hasMirror == true) {
			DialogueText.text = "You're still in the god-forsaken cell, but you are now holding the small mirror. For some reason, you think it might be useful.";
		} else if (hasMirror == false) {
			DialogueText.text = "You awake in a dark cell. A single pinprick of light shines through the smallest of cracks in the ceiling. "
			+ "In the near-dark you can make out a pile of Sheets on the floor; a cracked and dirty Mirror on the wall; "
				+ "a rusted - and very much locked - Lock on the wooden door that leads to...somewhere.";
		}

		RoomText.text = "The Cell";
		OptionText.text = "(M) Look at Mirror - (S) Look at Sheets - (L) Look at Lock";

		if (Input.GetKeyDown (KeyCode.M))
			currentState = States.mirror;

		if (Input.GetKeyDown (KeyCode.S))
			currentState = States.sheets_0;

		if (Input.GetKeyDown (KeyCode.L))
			currentState = States.cell_lock_0;
	}

	void cell_lock_0 () {
		DialogueText.text = "Rusted shut; locked from the other side. Reaching through the bars, you can feel something in the lock. It's not a key, it's..."
			+ "you can't tell. You can't get a good look at whatever it is from this side of the lock.";
		RoomText.text = "The Cell: Lock.";

		if (hasMirror == true) {
			OptionText.text = "(R) Return - (M) Use Mirror";
		} else if (hasMirror == false) {
			OptionText.text = "(R) - Return";
		}

		if (Input.GetKeyDown (KeyCode.R))
			currentState = States.cell;

		if (hasMirror == true && Input.GetKeyDown (KeyCode.M))
			currentState = States.cell_lock_1;
	}

	void cell_lock_1 () {
		DialogueText.text = "Using the mirror in one hand, you can now see what was stuck in the lock: a rusted pin, and a screwdriver. "
			+ "After a few tries, you are able to turn the lock, and the cell door swings wide. A darkened corridor awaits. "
		 	+ "Just beyond the cell door lies your freedom, and the unknown.";
		RoomText.text = "The Cell: Open!";
		OptionText.text = "(R) Return to Cell - (L) Leave Cell";

		if (Input.GetKeyDown (KeyCode.R))
			currentState = States.gameover_cell;

		if (Input.GetKeyDown (KeyCode.L))
			currentState = States.hall_start;
	}

	void sheets_0 () {
		DialogueText.text = "The sheets are filthy, stained from god-knows-how-many years, and with god-knows-what. "
			+ "Realizing that you may have slept in these makes you feel sick.";
		RoomText.text = "The Cell: Sheets.";
		OptionText.text = "(R) Return";

		if (Input.GetKeyDown (KeyCode.R))
			currentState = States.cell;

	}

	void mirror () {
		DialogueText.text = "Barely bigger than a pocket mirror, and covered in a layer of...something. But you can still clearly make out the details of your face. You look like hell.";
		RoomText.text = "The Cell: Mirror.";
		OptionText.text = "(R) Return - (T) Take Mirror";

		if (Input.GetKeyDown (KeyCode.R))
			currentState = States.cell;

		if (Input.GetKeyDown (KeyCode.T)) {
			hasMirror = true;
			currentState = States.cell;
		}
	}

	void hall_start () {
		DialogueText.text = "Before you, a long, narrow hallway leads to a lighted staircase. The floor is cold brick; you think you may have seen something shining in the light. "
			+ "To your right, gray steel lockers line the wall.";
		RoomText.text = "The Hall";
		OptionText.text = "(L) Examine Lockers - (F) Examine Floor - (S) Examine Stairs";

		if (hasUniform) {
			DialogueText.text = "You are dressed as a janitor. In front of you, the hallway leads to the lighted stairway. To your right are the empty lockers.";
		}

		if (Input.GetKeyDown (KeyCode.L))
			currentState = States.hall_lockers;

		if (Input.GetKeyDown (KeyCode.F))
			currentState = States.hall_floor;

		if (Input.GetKeyDown (KeyCode.S))
			currentState = States.hall_stairs;
	}

	void hall_lockers () {

		DialogueText.text = "All of the gray, steel lockers are open and empty...except for one. A small combination lock is keeping the last locker shut up tight."; 
		RoomText.text = "The Hall: Lockers";
		OptionText.text = "(R) Return to Hall - (L) Examine Lock";

		if (hasUniform == true) {
			DialogueText.text = "All of the lockers now stand open and empty. There's nothing else to see here.";
			OptionText.text = "(R) Return to Hall";
		}

		if (Input.GetKeyDown (KeyCode.R))
			currentState = States.hall_start;

		if (Input.GetKeyDown (KeyCode.L))
			currentState = States.hall_lockers_lock;
	}

	void hall_lockers_lock () {

		RoomText.text = "The Hall: Lock";
		DialogueText.text = "Without the combination, you'd need a pair of bolt-cutters to get this thing open.";

		if (hasUniform == false && hasCombination == false) {
			OptionText.text = "(R) Return to Lockers";
		} else if (hasUniform == false && hasCombination == true) {
			OptionText.text = "(R) Return to Lockers - (O) Open Locker";
		} else {
			DialogueText.text = "The lock is still hanging there, unlocked from earlier.";
			OptionText.text = "(R) Return to Lockers";
		}

		if (Input.GetKeyDown (KeyCode.R))
			currentState = States.hall_lockers;

		if (Input.GetKeyDown (KeyCode.O) && hasCombination == true) {
			currentState = States.hall_lockers_lock_open;
		}

	}

	void hall_lockers_lock_open () {
		RoomText.text = "The Hall: Open Locker";

		if (!hasUniform) {
			DialogueText.text = "The locker is open. A janitor's uniform hangs inside, complete with overalls, a ballcap, and a set of building keys.";
			OptionText.text = "(R) Return to Lockers - (T) Take Uniform";
		} else {
			DialogueText.text = "The locker is empty. There's nothing more to see here.";
			OptionText.text = "(R) Return to Lockers";
		}


		if (Input.GetKeyDown (KeyCode.R))
			currentState = States.hall_lockers;

		if (Input.GetKeyDown (KeyCode.T)) {
			hasUniform = true;
			currentState = States.hall_start;
		}

	}

	void hall_floor () {
		RoomText.text = "The Hall: Floor";

		if (!hasCombination) {
			DialogueText.text = "Stooping down to examine the floor, your hands stumble across a small scrap of paper. It's crumpled, like it's been in someone's pocket. "
				+ "On it is written what looks like a combination: 14r-10l-22r";
			OptionText.text = "(R) Return to Hall - (T) Take Combination";
		} else {
			DialogueText.text = "Nothing down here but cold, dirty bricks.";
			OptionText.text = "(R) Return to Hall";
		}

		if (Input.GetKeyDown (KeyCode.R))
			currentState = States.hall_start;

		if (Input.GetKeyDown (KeyCode.T) && !hasCombination) {
			hasCombination = true;
			currentState = States.hall_start;
		}
	}

	void hall_stairs () {
		RoomText.text = "The Hall: Stairs";
		DialogueText.text = "From the bottom of the stairs, you can hear voices and footsteps. It sounds like some kind of party. Why did they lock you up? ";
		OptionText.text = "(R) Return to Hall - (C) Climb the Stairs";

		if (!hasUniform)
			DialogueText.text += "You don't think it's a good idea to go up there; what if someone sees you?";

		if (hasUniform)
			DialogueText.text += "Dressed as a janitor, you think you just might be able to move about unnoticed.";

		if (Input.GetKeyDown (KeyCode.R))
			currentState = States.hall_start;

		if (Input.GetKeyDown (KeyCode.C)) {
			if (!hasUniform) {
				currentState = States.gameover_stairs;
			} else {
				currentState = States.freedom;
			}
		}
	}

	void freedom () {
		RoomText.text = "Freedom!";
		DialogueText.text = "You climb the staircase and exit into a grand ballroom. A hundred or more guests in party masks dance in relative quiet. There is no music, only footsteps and the occasional burst of chatter. "
			+ "No one seems to notice you; the janitor's uniform has had the exact effect you had hoped for. After a few tries, you find the exit to what appears to be nothing more than a giant warehouse in an industrial park. "
				+ "You aren't sure where you go from here, but it doesn't matter; all that matters is you're free.";
		OptionText.text = "(Space) Start Over";

		if (Input.GetKeyDown (KeyCode.Space))
			currentState = States.start;
	}

	void gameover_cell () {
		DialogueText.text = "Fear overtakes you, and you return to the craven comfort of your cell. As soon as you are back inside, the door swings shut with a ferocious BANG. "
			+ "You live out your few remaining days in your tiny prison.";
		RoomText.text = "GAME OVER";
		OptionText.text = "(Space) Start Over";

		if (Input.GetKeyDown (KeyCode.Space))
			currentState = States.start;
	}

	void gameover_stairs () {
		RoomText.text = "GAME OVER";
		DialogueText.text = "You climb the staircase and exit into a grand ballroom. A hundred guests or more in party masks all dance together in silence. They begin to slowly notice and turn toward you. "
			+ "You turn to run, but feel hands over your eyes, around your waiste, and holding each arm. It is the last thing you ever feel.";
		OptionText.text = "(Space) Start Over";

		if (Input.GetKeyDown (KeyCode.Space))
			currentState = States.start;
	}
	

}
