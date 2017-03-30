using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTimer : MonoBehaviour {

	public Mesh[] DigitMeshes;
	public ParticleSystem MinutesHigh, MinutesLow, Colon, SecondsHigh, SecondsLow;

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

		int sl = seconds % DigitMeshes.Length;
		int sh = (seconds / DigitMeshes.Length) % DigitMeshes.Length;
		int ml = minutes % DigitMeshes.Length;
		int mh = (minutes / DigitMeshes.Length) % DigitMeshes.Length;


		var mhs = MinutesHigh.shape;
		mhs.mesh = DigitMeshes[mh];
		if (secondsLeft < 60 * 10) {
			if (MinutesHigh.isPlaying) MinutesHigh.Stop();
		} else {
			if (!MinutesHigh.isPlaying) MinutesHigh.Play();
		}
		var mls = MinutesLow.shape;
		mls.mesh = DigitMeshes[ml];
		if (secondsLeft < 60) {
			if (MinutesLow.isPlaying) MinutesLow.Stop();
			if (Colon.isPlaying) Colon.Stop();
		} else {
			if (!MinutesLow.isPlaying) MinutesLow.Play();
			if (!Colon.isPlaying) Colon.Play();
		}
		var shs = SecondsHigh.shape;
		shs.mesh = DigitMeshes[sh];
		if (secondsLeft < 10) {
			if (SecondsHigh.isPlaying) SecondsHigh.Stop();
		} else {
			if (!SecondsHigh.isPlaying) SecondsHigh.Play();
		}
		var sls = SecondsLow.shape;
		sls.mesh = DigitMeshes[sl];

		System.Action< Color > SetColor = delegate(Color c) {
			var mhm = MinutesHigh.main; mhm.startColor = c;
			var mlm = MinutesLow.main; mlm.startColor = c;
			var shm = SecondsHigh.main; shm.startColor = c;
			var slm = SecondsLow.main; slm.startColor = c;
			var cm = Colon.main; cm.startColor = c;
		};

		if (timer.CurrentMode == Timer.Mode.Talk) {
			var mhm = MinutesHigh.main;
			SetColor(new Color(1.0f, 1.0f, 1.0f, 1.0f));
		} else if (timer.CurrentMode == Timer.Mode.Bonus) {
			SetColor(new Color(1.0f, 0.5f, 0.5f, 1.0f));
		} else if (timer.CurrentMode == Timer.Mode.Questions) {
			SetColor(new Color(0.1f, 0.1f, 1.0f, 1.0f));
		} else if (timer.CurrentMode == Timer.Mode.Finished) {
			MinutesHigh.Stop();
			MinutesLow.Stop();
			Colon.Stop();
			SecondsHigh.Stop();
			SecondsLow.Stop();
		}
	}

	void OnReset() {
		MinutesHigh.Play();
		MinutesLow.Play();
		Colon.Play();
		SecondsHigh.Play();
		SecondsLow.Play();
		LateUpdate();
	}

	void OnTimeout() {
		
	}
}
