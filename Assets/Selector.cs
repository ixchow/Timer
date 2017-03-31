using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

	public Timer[] Timers;
	public Timer current = null;

	public UnityEngine.UI.Button StartTalkButton, StartKeynoteButton, PauseButton, EndTalkButton, EndQuestionsButton;

	void Start () {
		/*Timers = FindObjectsOfType< Timer >();*/
		foreach (var t in Timers) {
			t.gameObject.SetActive(false);
		}
		StartTalkButton.onClick.AddListener( delegate() {
			StartTalk();
		});
		StartKeynoteButton.onClick.AddListener( delegate() {
			StartKeynote();
		});
		PauseButton.onClick.AddListener( delegate() {
			if (current) current.Paused = !current.Paused;
		});
		EndTalkButton.onClick.AddListener( delegate {
			if (current && (current.CurrentMode == Timer.Mode.Talk || current.CurrentMode == Timer.Mode.Bonus)) {
				current.FinishTalk();
			}
		});
		EndQuestionsButton.onClick.AddListener( delegate {
			if (current && (current.CurrentMode == Timer.Mode.Questions || current.CurrentMode == Timer.Mode.Finished)) {
				current.gameObject.SetActive(false);
				current = null;
			}
		});
	}

	void Update() {
		if (current == null) {
			StartTalkButton.interactable = true;
			StartKeynoteButton.interactable = true;
			PauseButton.interactable = false;
			EndTalkButton.interactable = false;
			EndQuestionsButton.interactable = false;
			if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Space)) {
				if (Input.GetKeyDown(KeyCode.K)) {
					StartKeynote();
				} else if (Input.GetKeyDown(KeyCode.S)) {
					PickCurrent(0);
					current.TalkSeconds = 10;
					current.BonusSeconds = 10;
					current.QuestionSeconds = 10;
					current.gameObject.SetActive(true);
					current.Reset();
					current.Paused = false;
				} else {
					StartTalk();
				}
			}
		} else {
			StartTalkButton.interactable = false;
			StartKeynoteButton.interactable = false;
			PauseButton.interactable = true;
			EndTalkButton.interactable = (current.CurrentMode == Timer.Mode.Talk || current.CurrentMode == Timer.Mode.Bonus);
			EndQuestionsButton.interactable = (current.CurrentMode == Timer.Mode.Questions || current.CurrentMode == Timer.Mode.Finished);


			if (current.CurrentMode == Timer.Mode.Talk || current.CurrentMode == Timer.Mode.Bonus) {
				if (Input.GetKeyDown(KeyCode.Return)) current.FinishTalk();
			
			} else if (current.CurrentMode == Timer.Mode.Questions) {
				if (Input.GetKeyDown(KeyCode.Return)) current.FinishQuestions();
			}

			if (Input.GetKeyDown(KeyCode.Space)) {
				current.Paused = !current.Paused;
			}
			if (Input.GetKeyDown(KeyCode.Backspace)) {
				current.gameObject.SetActive(false);
				current = null;
			}
		}
	}

	void PickCurrent(int option = -1) {
		if (current != null) {
			current.gameObject.SetActive(false);
		}
		if (option == -1) option = Random.Range(0, Timers.Length);
		current = Timers[option];
		current.gameObject.SetActive(true);
	}

	public void StartTalk() {
		PickCurrent(0);
		current.TalkSeconds = 5 * 60;
		current.BonusSeconds = 1 * 60;
		current.QuestionSeconds = 1 * 60;
		current.Reset();
		current.Paused = false;
	}

	public void StartKeynote() {
		PickCurrent(1);
		current.TalkSeconds = 10 * 60;
		current.BonusSeconds = 1 * 60;
		current.QuestionSeconds = 2 * 60;
		current.Reset();
		current.Paused = false;
	}
}
