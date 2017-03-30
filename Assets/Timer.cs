using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
	[HideInInspector]
	public int TalkSeconds = 5 * 60;
	[HideInInspector]
	public int BonusSeconds = 1 * 60;
	[HideInInspector]
	public int QuestionSeconds = 1 * 60;


	public enum Mode { Talk, Bonus, Questions, Finished }
	[HideInInspector]
	public Mode CurrentMode;
	[HideInInspector]
	public float TimeLeft = 0.0f;
	[HideInInspector]
	public bool Paused = true;


	public void Reset() {
		CurrentMode = Mode.Talk;
		TimeLeft = TalkSeconds;
		Paused = true;
		onReset.Invoke();
	}

	public void FinishTalk() {
		if (CurrentMode == Mode.Talk) {
			TimeLeft += BonusSeconds;
			CurrentMode = Mode.Bonus;
		}
		if (CurrentMode == Mode.Bonus) {
			TimeLeft += QuestionSeconds;
			CurrentMode = Mode.Questions;
		}
	}

	public void FinishQuestions() {
		if (CurrentMode == Mode.Questions) {
			CurrentMode = Mode.Finished;
			TimeLeft = 0.0f;
		}
	}

	public UnityEngine.Events.UnityEvent onReset = new UnityEngine.Events.UnityEvent();
	public UnityEngine.Events.UnityEvent onTimeout = new UnityEngine.Events.UnityEvent();

	void Update() {
		if (!Paused) TimeLeft -= Time.deltaTime;
		if (TimeLeft < 0.0f) {
			if (CurrentMode == Mode.Talk) {
				onTimeout.Invoke();
				CurrentMode = Mode.Bonus;
				TimeLeft = BonusSeconds;
			} else if (CurrentMode == Mode.Bonus) {
				onTimeout.Invoke();
				CurrentMode = Mode.Questions;
				TimeLeft = QuestionSeconds;
			} else if (CurrentMode == Mode.Questions) {
				onTimeout.Invoke();
				CurrentMode = Mode.Finished;
				TimeLeft = 0.0f;
			} else if (CurrentMode == Mode.Finished) {
				TimeLeft = 0.0f;
			}
		}
	}

	public int GetSecondsLeft() {
		if (TimeLeft < 0) {
			return 0;
		}
		return Mathf.FloorToInt(TimeLeft);
	}

	public void GetDigits(out char minutesTens, out char minutesOnes, out char secondsTens, out char secondsOnes) {
		int secondsLeft = GetSecondsLeft();
		string minutes = string.Format("{0:D2}", secondsLeft / 60);
		string seconds = string.Format("{0:D2}", secondsLeft % 60);
		minutesTens = minutes[minutes.Length-2];
		minutesOnes = minutes[minutes.Length-1];
		secondsTens = seconds[seconds.Length-2];
		secondsOnes = seconds[seconds.Length-1];
	}
}
