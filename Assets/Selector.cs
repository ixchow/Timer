using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

	public Timer[] Timers;
	public Timer current = null;

	void Start () {
		Timers = FindObjectsOfType< Timer >();
		foreach (var t in Timers) {
			t.gameObject.SetActive(false);
		}
	}

	void Update() {
		if (current == null) {
			if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Space)) {
				current = Timers[Random.Range(0, Timers.Length-1)];
				if (Input.GetKeyDown(KeyCode.K)) {
					current.TalkSeconds = 10 * 60;
				} else {
					current.TalkSeconds = 5 * 60;
				}
				current.gameObject.SetActive(true);
				current.Reset();
			}
		} else {
			if (Input.GetKeyDown(KeyCode.Space)) {
				current.Paused = !current.Paused;
			}
			if (Input.GetKeyDown(KeyCode.Return)) {
				if (current.CurrentMode == Timer.Mode.Talk || current.CurrentMode == Timer.Mode.Bonus) {
					current.FinishTalk();
				} else if (current.CurrentMode == Timer.Mode.Questions) {
					current.FinishQuestions();
				}
			}
			if (Input.GetKeyDown(KeyCode.Backspace)) {
				current.gameObject.SetActive(false);
				current = null;
			}
		}
	}
}
