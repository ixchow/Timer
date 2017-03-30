using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTimer : MonoBehaviour {

	public UnityEngine.UI.Text MinutesText;
	public UnityEngine.UI.Text ColonText;
	public UnityEngine.UI.Text SecondsText;
	public UnityEngine.UI.Text LabelText;

	Timer timer;

	void Start () {
		timer = GetComponent< Timer >();
		timer.onReset.AddListener(OnReset);
		timer.onTimeout.AddListener(OnTimeout);
	}

	void LateUpdate () {
		char m10, m1, s10, s1;
		timer.GetDigits(out m10, out m1, out s10, out s1);
		MinutesText.text = m10.ToString() + m1;
		SecondsText.text = s10.ToString() + s1;
		System.Action< Color > SetColor = delegate (Color c) {
			LabelText.color = c;
			MinutesText.color = c;
			if (ColonText) ColonText.color = c;
			SecondsText.color = c;
		};
		if (timer.CurrentMode == Timer.Mode.Talk) {
			LabelText.text = "Talk";
			SetColor(Color.white);
		} else if (timer.CurrentMode == Timer.Mode.Bonus) {
			LabelText.text = "! Overtime !";
			SetColor(Color.red);
		} else if (timer.CurrentMode == Timer.Mode.Questions) {
			LabelText.text = "Questions";
			SetColor(Color.yellow);
		} else if (timer.CurrentMode == Timer.Mode.Finished) {
			LabelText.text = "- It's Over -";
			SetColor(Color.blue);
		}
	}

	void OnReset() {
	}

	void OnTimeout() {
		
	}
}
