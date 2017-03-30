using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimer : MonoBehaviour {

	public GameObject[] MinutesLow;
	public GameObject[] MinutesHigh;
	public GameObject[] SecondsLow;
	public GameObject[] SecondsHigh;

	Timer timer;

	void Start () {
		timer = GetComponent< Timer >();
		timer.onReset.AddListener(OnReset);
		timer.onTimeout.AddListener(OnTimeout);
	}

	void LateUpdate () {
		int secondsLeft = timer.GetSecondsLeft();
		int seconds = secondsLeft % 60;
		int minutes = secondsLeft / 60;

		int sl = seconds % SecondsLow.Length;
		int sh = (seconds / SecondsLow.Length) % SecondsHigh.Length;
		int ml = minutes % MinutesLow.Length;
		int mh = (minutes / MinutesLow.Length) % MinutesHigh.Length;

		for (var i = 0; i < MinutesLow.Length; ++i) {
			MinutesLow[i].SetActive(i == ml);
		}
		for (var i = 0; i < MinutesHigh.Length; ++i) {
			MinutesHigh[i].SetActive(i == mh);
		}
		for (var i = 0; i < SecondsLow.Length; ++i) {
			SecondsLow[i].SetActive(i == sl);
		}
		for (var i = 0; i < SecondsHigh.Length; ++i) {
			SecondsHigh[i].SetActive(i == sh);
		}
	}

	void OnReset() {
	}

	void OnTimeout() {
		
	}
}
