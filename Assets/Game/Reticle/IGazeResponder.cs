using System;

public interface IGazeResponder {
	void OnGazeEnter();
	void OnGazeExit();
	void OnGazeStay();
	void OnGazeTrigger();
	float timeToTrigger{ get;}
	bool isInteractionVisible{ get;}
	bool isEnabled{ get;}
}