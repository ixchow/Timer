using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSounds : MonoBehaviour {

	public AudioSource TalkTimeout, BonusTimeout, QuestionsTimeout;

	void Start () {
		Timer timer = GetComponent< Timer >();
		timer.onTimeout.AddListener(delegate (){
			if (timer.CurrentMode == Timer.Mode.Talk && TalkTimeout) {
				TalkTimeout.PlayOneShot(TalkTimeout.clip);
			}
			if (timer.CurrentMode == Timer.Mode.Bonus && BonusTimeout) {
				BonusTimeout.PlayOneShot(BonusTimeout.clip);
			}
			if (timer.CurrentMode == Timer.Mode.Questions && QuestionsTimeout) {
				QuestionsTimeout.PlayOneShot(QuestionsTimeout.clip);
			}
		});
	}
}
